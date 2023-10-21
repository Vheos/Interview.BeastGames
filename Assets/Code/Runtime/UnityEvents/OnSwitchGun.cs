using System;
using UnityEngine.Events;


public static class OnSwitchGun
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly GunInventory Inventory;
		public readonly Gun From;
		public readonly Gun To;

		public Data(GunInventory invoker, Gun from, Gun to)
		{
			Inventory = invoker;
			From = from;
			To = to;
		}
	}
}