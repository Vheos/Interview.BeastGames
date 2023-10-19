using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
	[SerializeField, Range(0f, 10)] private float explosionRadius = 5f;
	[SerializeField, Range(1, 50)] private int maxHits = 5;

	private Collider[] hits;

	public void RandomizeRotation(OnSpawnBullet.Data data)
		=> data.Bullet.transform.rotation = Random.rotation;

	public void HandleExplosion(OnDespawnBullet.Data data)
	{
		Vector3 position = data.Bullet.transform.position;
		int layerMask = Layer.Destructible.ToLayerMask();
		Physics.OverlapSphereNonAlloc(position, explosionRadius, hits, layerMask);

		foreach (var hit in hits)
			if (hit != null && hit.TryGetInSelfOrParents(out Destructible destructible))
				destructible.GetHitBy(data.Bullet);
	}

	private void Awake()
	{
		hits = new Collider[maxHits];
	}
}
