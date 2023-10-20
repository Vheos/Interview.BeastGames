using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
	[SerializeField, Range(0f, 10)] private float explosionRadius = 5f;
	[SerializeField, Range(1, 50)] private int maxHits = 5;
	[SerializeField] private ParticleSystem explosionParticlePrefab;

	private Collider[] colliderHits;
	private RaycastHit[] raycastHits;

	public void RandomizeRotation(OnSpawnBullet.Data data)
		=> data.Bullet.transform.rotation = Random.rotation;

	public void Explode(OnDespawnBullet.Data data)
	{
		Vector3 bulletPosition = data.Bullet.transform.position;
		int colliderHitCount = Physics.OverlapSphereNonAlloc(bulletPosition, explosionRadius, colliderHits);

		for (int i = 0; i < colliderHitCount; i++)
		{
			Vector3 colliderPosition = colliderHits[i].bounds.center;
			Ray ray = new(bulletPosition, colliderPosition - bulletPosition);
			int raycastHitCount = Physics.RaycastNonAlloc(ray, raycastHits, explosionRadius);

			for (int j = 0; j < raycastHitCount; j++)
				if (raycastHits[j].collider == colliderHits[i])
					data.Bullet.Hit(raycastHits[j]);
		}

		if (explosionParticlePrefab != null)
			Instantiate(explosionParticlePrefab, bulletPosition, Quaternion.identity);
	}

	private void Awake()
	{
		colliderHits = new Collider[maxHits];
		raycastHits = new RaycastHit[maxHits];
	}
}
