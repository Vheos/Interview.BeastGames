using System;
using UnityEngine.Events;


public static class OnAddGun
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly GunInventory Inventory;
		public readonly Gun Gun;

		public Data(GunInventory invoker, Gun gun)
		{
			Gun = gun;
			Inventory = invoker;
		}
	}
}