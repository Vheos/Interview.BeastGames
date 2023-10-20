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

		public Data(Gun gun)
		{
			Gun = gun;
		}
	}
}