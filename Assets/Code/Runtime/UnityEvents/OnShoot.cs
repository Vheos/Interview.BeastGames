using System;
using UnityEngine.Events;


public static class OnShoot
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Gun Gun;
		public readonly Bullet Bullet;

		public Data(Gun gun, Bullet bullet)
		{
			Gun = gun;
			Bullet = bullet;
		}
	}
}