using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	[SerializeField] private DestructibleAttributes attributes;
	[SerializeField] private OnGetShot.Event onGetShot;

	public void InvokeOnGetShot(Bullet bullet, Collision collision)
		=> onGetShot.Invoke(new(this, bullet, collision));
}
