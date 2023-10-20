using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private BulletAttributes attributes;
	[SerializeField] private Rigidbody rigidBody;
	[SerializeField] private BulletTrail trail;
	[Header(Headers.Events)]
	[SerializeField] private OnSpawnBullet.Event OnSpawn;
	[SerializeField] private OnHit.Event OnHit;
	[SerializeField] private OnDespawnBullet.Event OnDespawn;

	public BulletAttributes Attributes
		=> attributes;

	private int hits;
	public Gun Gun { get; private set; }
	public BulletTrail Trail
		=> trail;

	private float destroyTime;

	private static Quaternion RandomSpreadRotation(float maxAngle)
		=> Quaternion.Euler(Random.Range(-maxAngle, +maxAngle) / 2f, Random.Range(-maxAngle, +maxAngle) / 2f, 0f);

	public static Bullet Spawn(Bullet prefab, Gun gun)
	{
		Vector3 position = gun.GetNearMuzzlePoint(Camera.main);
		Quaternion rotation = gun.transform.rotation;
		Quaternion spread = RandomSpreadRotation(gun.Attributes.BulletSpread);
		Bullet bullet = Instantiate(prefab, position, rotation * spread);

		bullet.name = prefab.name;
		bullet.Gun = gun;
		bullet.SetDespawnTime();
		bullet.AddForce();

		bullet.OnSpawn.Invoke(new(bullet));
		return bullet;
	}
	public void Hit(Collision collision)
	{
		hits++;
		OnHit.Data hitData = new(this, collision);
		InvokeEventsAndSpawnParticles(hitData);
		CheckDespawnOnHit();
	}
	public void Hit(RaycastHit raycastHit)
	{
		hits++;
		OnHit.Data hitData = new(this, raycastHit);
		InvokeEventsAndSpawnParticles(hitData);
		CheckDespawnOnHit();
	}
	public void Despawn()
	{
		Trail.UnparentAndFade();
		OnDespawn.Invoke(new(this));
		Destroy(gameObject);
	}

	private void SetDespawnTime()
		=> destroyTime = Time.time + attributes.MaxTime;
	private bool CheckDespawnOnTime()
	{
		if (Time.time < destroyTime)
			return false;

		Despawn();
		return true;
	}
	private void CheckDespawnOnHit()
	{
		if (hits >= attributes.MaxHits)
			Despawn();
	}
	private void InvokeEventsAndSpawnParticles(OnHit.Data hitData)
	{
		OnHit.Invoke(hitData);
		if (hitData.Collider.TryGetDestructible(out var destructible))
			destructible.GetHit(hitData);

		if (attributes.HitParticlePrefab != null)
		{
			HitParticle hitParticlePrefab = destructible != null && destructible.Attributes.HitParticlePrefab != null
				? destructible.Attributes.HitParticlePrefab : attributes.HitParticlePrefab;
			Vector3 position = hitData.Point;
			Quaternion rotation = Quaternion.LookRotation(hitData.Normal);

			HitParticle hitParticle = Instantiate(hitParticlePrefab, position, rotation);
			hitParticle.IsDecalVisible = attributes.SpawnHitDecal;
		}
	}
	private void AddForce()
		=> rigidBody.AddForce(transform.forward * attributes.Force);

	private void FixedUpdate()
	{
		CheckDespawnOnTime();
	}
	private void OnCollisionEnter(Collision collision)
	{
		Hit(collision);
	}
}