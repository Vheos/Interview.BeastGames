using System;
using UnityEngine.Events;


public static class OnAddRemoveGun
{
	[Serializable]
	public class Event : UnityEvent<Data>
	{ }

	[Serializable]
	public readonly struct Data
	{
		public readonly GunInventory Inventory;
		public readonly Gun Gun;

		public Data(GunInventory gunInventory, Gun gun)
		{
			Gun = gun;
			Inventory = gunInventory;
		}
	}
}