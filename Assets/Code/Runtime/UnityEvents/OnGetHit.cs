using System;
using UnityEngine;
using UnityEngine.Events;


public static class OnGetHit
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Destructible Destructible;
		public readonly OnHit.Data HitData;

		public Bullet Bullet
			=> HitData.Bullet;

		public Data(Destructible destructible, OnHit.Data hitData)
		{
			Destructible = destructible;
			HitData = hitData;
		}
	}
}