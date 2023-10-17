using System;
using UnityEngine.Events;


public class ShootGun
{
	[Serializable]
	public class Event : UnityEvent<Context>
	{ }

	[Serializable]
	public readonly struct Context
	{
		public readonly Gun Gun;

		public Context(Gun gun)
		{
			Gun = gun;
		}
	}
}