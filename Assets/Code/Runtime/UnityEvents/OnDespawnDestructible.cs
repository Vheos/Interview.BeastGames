using System;
using UnityEngine.Events;


public static class OnDespawnDestructible
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Destructible Destructible;

		public Data(Destructible invoker)
		{
			Destructible = invoker;
		}
	}
}