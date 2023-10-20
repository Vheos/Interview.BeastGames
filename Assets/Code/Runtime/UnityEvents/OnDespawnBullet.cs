using System;
using UnityEngine.Events;


public static class OnDespawnBullet
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Bullet Bullet;

		public Data(Bullet bullet)
		{
			Bullet = bullet;
		}
	}
}