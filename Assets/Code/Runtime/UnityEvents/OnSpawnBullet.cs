using System;
using UnityEngine.Events;


public static class OnSpawnBullet
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Bullet Bullet;

		public Data(Bullet invoker)
		{
			Bullet = invoker;
		}
	}
}