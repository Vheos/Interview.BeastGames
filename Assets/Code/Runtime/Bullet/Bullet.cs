using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletAttributes attributes;

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
	private void Move()
		=> transform.position += attributes.Speed * Time.deltaTime * transform.forward;
	private void CheckCollision()
	{
		// TODO
	}

	private void Start()
	{
		UpdateDestroyTime();
	}
	private void Update()
	{
		if (CheckDestroy())
			return;

		Move();
		CheckCollision();
	}
}