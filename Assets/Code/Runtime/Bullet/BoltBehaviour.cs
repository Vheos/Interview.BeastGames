using DG.Tweening;
using UnityEngine;

public class BoltBehaviour : MonoBehaviour
{
	[SerializeField, Range(1f, 100f)] private float maxDistance = 50f;

	private Transform hitAnchor;

	public void Raycast(OnSpawnBullet.Data data)
	{
		Ray ray = new(data.Bullet.transform.position, data.Bullet.transform.forward);

		data.Bullet.Trail.UnparentAndFade();
		data.Bullet.Trail.transform.DOMove(ray.GetPoint(maxDistance / 10f), data.Bullet.Trail.FadeDuration);

		if (!Physics.Raycast(ray, out var hitInfo, maxDistance))
		{
			data.Bullet.Despawn();
			return;
		}

		data.Bullet.transform.position = hitInfo.point;
		data.Bullet.Hit(hitInfo);
	}
	public void StickToTarget(OnHit.Data data)
	{
		hitAnchor = new GameObject().transform;
		hitAnchor.parent = data.Collider.transform;
		hitAnchor.SnapTo(data.Bullet.transform);

		SnapToTransform snap = data.Bullet.gameObject.AddComponent<SnapToTransform>();
		snap.Target = hitAnchor;
	}
	public void RemoveHitAnchor(OnDespawnBullet.Data data)
	{
		if (hitAnchor != null)
			Destroy(hitAnchor.gameObject);
	}
}
