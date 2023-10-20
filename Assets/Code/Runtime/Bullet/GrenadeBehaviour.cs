using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
	[SerializeField, Range(0f, 10)] private float explosionRadius = 5f;
	[SerializeField, Range(1, 50)] private int maxHits = 5;

	private RaycastHit[] raycastHits;

	public void RandomizeRotation(OnSpawnBullet.Data data)
		=> data.Bullet.transform.rotation = Random.rotation;

	public void Explode(OnDespawnBullet.Data data)
	{
		Vector3 position = data.Bullet.transform.position;
		int hitCount = Physics.SphereCastNonAlloc(position, explosionRadius, Vector3.up, raycastHits, 0f);
		Debug.Log(hitCount);
		for (int i = 0; i < hitCount; i++)
			data.Bullet.Hit(raycastHits[i]);
	}

	private void Awake()
	{
		raycastHits = new RaycastHit[maxHits];
	}
}
