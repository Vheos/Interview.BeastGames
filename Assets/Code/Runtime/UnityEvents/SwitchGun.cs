using System;
using UnityEngine.Events;


public class SwitchGun
{
	[Serializable]
	public class Event : UnityEvent<Context>
	{ }

	[Serializable]
	public readonly struct Context
	{
		public readonly Gun From;
		public readonly Gun To;

		public Context(Gun from, Gun to)
		{
			From = from;
			To = to;
		}
	}
}