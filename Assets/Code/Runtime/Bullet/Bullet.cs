using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletAttributes attributes;
	[SerializeField] private Rigidbody rigidBody;
	[SerializeField] private OnHit.Event OnHit;

	public BulletAttributes Attributes
		=> attributes;

	public Gun Gun { get; private set; }
	private float destroyTime;

	public static Bullet Spawn(Bullet prefab, Gun gun)
	{
		Transform muzzle = gun.Anchors.Muzzle;
		Bullet bullet = Instantiate(prefab, muzzle.position, muzzle.rotation);
		bullet.Gun = gun;

		return bullet;
	}
	private void UpdateDestroyTime()
		=> destroyTime = Time.time + attributes.Lifetime;
	private bool CheckDestroy()
	{
		if (Time.time < destroyTime)
			return false;

		Destroy(gameObject);
		return true;
	}

	private void Start()
	{
		UpdateDestroyTime();
		rigidBody.AddForce(attributes.Speed * 100f * transform.forward);
	}
	private void Update()
	{
		if (CheckDestroy())
			return;
	}

	private void OnCollisionEnter(Collision collision)
	{
		OnHit.Invoke(new(this, collision));

		if ((Layer)collision.gameObject.layer == Layer.Destructible
		&& collision.collider.TryGetInParents(out Destructible destructible))
		{
			destructible.InvokeOnGetShot(this, collision);
		}
	}
}