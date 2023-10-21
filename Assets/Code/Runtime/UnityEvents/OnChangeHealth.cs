using System;
using UnityEngine.Events;


public static class OnChangeHealth
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly Destructible Destructible;
		public readonly float From;
		public readonly float To;


		public Data(Destructible invoker, float from, float to)
		{
			Destructible = invoker;
			From = from;
			To = to;
		}
	}
}