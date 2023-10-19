using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private BulletAttributes attributes;
	[SerializeField] private Rigidbody rigidBody;
	[SerializeField] private TrailRenderer trailRenderer;
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
	private float destroyTime;

	static private Quaternion RandomSpreadRotation(float maxAngle)
		=> Quaternion.Euler(Random.value * maxAngle, Random.value * maxAngle, Random.value * maxAngle);

	public static Bullet Spawn(Bullet prefab, Gun gun)
	{
		Vector3 position = gun.GetNearMuzzlePoint(Camera.main);
		Quaternion rotation = gun.Anchors.Muzzle.rotation;
		Quaternion spread = RandomSpreadRotation(prefab.attributes.Spread);
		Bullet bullet = Instantiate(prefab, position, rotation * spread);

		bullet.name = prefab.name;
		bullet.Gun = gun;
		bullet.SetDespawnTime();
		bullet.TryAddForce();

		bullet.OnSpawn.Invoke(new(bullet));
		return bullet;
	}
	public void Despawn()
	{
		if (trailRenderer != null)
			trailRenderer.transform.parent = null;

		OnDespawn.Invoke(new(this));
		Destroy(gameObject);
	}
	private void SetDespawnTime()
		=> destroyTime = Time.time + attributes.MaxTime;
	private bool CheckDespawnTime()
	{
		if (Time.time < destroyTime)
			return false;

		Despawn();
		return true;
	}
	private void TryAddForce()
	{
		if (rigidBody != null)
			rigidBody.AddForce(transform.forward * attributes.Force);
	}

	private void FixedUpdate()
	{
		CheckDespawnTime();
	}
	private void OnCollisionEnter(Collision collision)
	{
		hits++;
		OnHit.Invoke(new(this, collision));

		if ((Layer)collision.gameObject.layer == Layer.Destructible
		&& collision.collider.TryGetInSelfOrParents(out Destructible destructible))
		{
			destructible.GetHitOnCollision(this, collision);
			if (attributes.DealDamageOnCollision)
				destructible.TakeDamageFrom(Gun);
		}

		if (hits >= attributes.MaxHits)
			Despawn();
	}
}