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
	public Rigidbody RigidBody
		=> rigidBody;

	private int hits;
	public Gun Gun { get; private set; }
	public BulletTrail Trail
		=> trail;

	private float destroyTime;

	private static Quaternion RandomSpreadRotation(float maxAngle)
		=> Quaternion.Euler(Random.value * maxAngle, Random.value * maxAngle, 0f);

	public static Bullet Spawn(Bullet prefab, Gun gun)
	{
		Vector3 position = gun.GetNearMuzzlePoint(Camera.main);
		Quaternion rotation = gun.Anchors.Muzzle.rotation;
		Quaternion spread = RandomSpreadRotation(gun.Attributes.BulletSpread);
		Bullet bullet = Instantiate(prefab, position, rotation * spread);

		bullet.name = prefab.name;
		bullet.Gun = gun;
		bullet.SetDespawnTime();
		bullet.TryAddForce();

		bullet.OnSpawn.Invoke(new(bullet));
		return bullet;
	}
	public void Hit(Collision collision)
	{
		hits++;
		InvokeEvents(new(this, collision));
		CheckDespawnOnHit();
	}
	public void Hit(RaycastHit raycastHit)
	{
		hits++;
		InvokeEvents(new(this, raycastHit));
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
	private void InvokeEvents(OnHit.Data hitData)
	{
		OnHit.Invoke(hitData);
		if (hitData.Collider.TryGetDestructible(out var destructible))
			destructible.GetHit(hitData);
	}
	private void TryAddForce()
	{
		if (rigidBody != null)
			rigidBody.AddForce(transform.forward * attributes.Force);
	}

	private void FixedUpdate()
	{
		CheckDespawnOnTime();
	}
	private void OnCollisionEnter(Collision collision)
	{
		Hit(collision);
	}
}