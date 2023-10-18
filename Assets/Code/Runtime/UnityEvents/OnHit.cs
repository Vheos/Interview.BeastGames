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
		public readonly Collision Collision;

		public Layer Layer
			=> (Layer)Collision.collider.gameObject.layer;
		public Collider Collider
			=> Collision.collider;
		public ContactPoint ContactPoint
			=> Collision.GetContact(0);

		public Data(Bullet bullet, Collision collision)
		{
			Bullet = bullet;
			Collision = collision;
		}
	}
}