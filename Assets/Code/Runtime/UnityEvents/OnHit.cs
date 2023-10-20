using System;
using UnityEngine;
using UnityEngine.Events;


public static class OnHit
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Bullet Bullet;
		private readonly Collision collision;
		private readonly RaycastHit? raycastHit;

		public bool IsCollision
			=> collision != null;
		public bool IsRaycast
			=> raycastHit != null;
		public Collider Collider
			=> collision != null ? collision.collider 
			: raycastHit?.collider;
		public Vector3 Point
			=> collision != null ? collision.GetContact(0).point 
			: raycastHit != null ? raycastHit.Value.point
			: default;
		public Vector3 Normal
			=> collision != null ? collision.GetContact(0).normal
			: raycastHit != null ? raycastHit.Value.normal
			: default;

		public Data(Bullet bullet, Collision collision)
		{
			Bullet = bullet;
			this.collision = collision;
			raycastHit = null;
		}

		public Data(Bullet bullet, RaycastHit raycastHit)
		{
			Bullet = bullet;
			collision = null;
			this.raycastHit = raycastHit;
		}
	}
}
