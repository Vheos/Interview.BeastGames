using System;
using UnityEngine;
using UnityEngine.Events;


public static class OnGetShot
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Destructible Destructible;
		public readonly Bullet Bullet;

		private readonly Collision collision;

		public Layer Layer
			=> (Layer)collision.collider.gameObject.layer;
		public Collider Collider
			=> collision.collider;
		public ContactPoint ContactPoint
			=> collision.GetContact(0);

		public Data(Destructible destructible, Bullet bullet, Collision collision)
		{
			Destructible = destructible;
			Bullet = bullet;
			this.collision = collision;
		}
	}
}