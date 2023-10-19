using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private BulletAttributes attributes;
	[SerializeField] private Rigidbody rigidBody;
	[Header(Headers.Events)]
	[SerializeField] private OnSpawnBullet.Event OnSpawn;
	[SerializeField] private OnHit.Event OnHit;
	[SerializeField] private OnDespawnBullet.Event OnDespawn;

	public BulletAttributes Attributes
		=> attributes;
	public Rigidbody RigidBody
		=> rigidBody;

	public Gun Gun { get; private set; }
	private float destroyTime;

	public static Bullet Spawn(Bullet prefab, Gun gun)
	{
		Transform muzzle = gun.Anchors.Muzzle;
		Bullet bullet = Instantiate(prefab, muzzle.position, muzzle.rotation);
		bullet.name = prefab.name;
		bullet.Gun = gun;
		bullet.SetDespawnTime();
		bullet.AddForce();

		bullet.OnSpawn.Invoke(new(bullet));
		return bullet;
	}
	public void Despawn()
	{
		OnDespawn.Invoke(new(this));
		Destroy(gameObject);
	}

	private void SetDespawnTime()
		=> destroyTime = Time.time + attributes.Lifetime;
	private void AddForce()
		=> rigidBody.AddForce(transform.forward * attributes.Force);
	private bool CheckDespawn()
	{
		if (Time.time < destroyTime)
			return false;

		Despawn();
		return true;
	}

	private void Update()
	{
		if (CheckDespawn())
			return;
	}
	private void OnCollisionEnter(Collision collision)
	{
		OnHit.Invoke(new(this, collision));

		if ((Layer)collision.gameObject.layer == Layer.Destructible
		&& collision.collider.TryGetInParents(out Destructible destructible))
			destructible.GetShot(this, collision);
	}
}