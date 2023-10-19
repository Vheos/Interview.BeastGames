using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBehaviour : MonoBehaviour
{
	[SerializeField, Range(1f, 100f)] private float maxDistance = 50f;

	public void Raycast(OnSpawnBullet.Data data)
	{
		Ray ray = new(data.Bullet.transform.position, data.Bullet.transform.forward);
		if (!Physics.Raycast(ray, out var hitInfo, maxDistance))
		{
			data.Bullet.Despawn();
			return;
		}

		data.Bullet.transform.position = hitInfo.point;
		if (hitInfo.collider.Layer() == Layer.Destructible
		&& hitInfo.collider.TryGetInSelfOrParents(out Destructible destructible))
			destructible.GetHitBy(data.Bullet);		
	}
}
