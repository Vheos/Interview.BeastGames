using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private DestructibleAttributes attributes;
	[Header(Headers.Events)]
	[SerializeField] private OnGetShot.Event onGetShot;

	public void InvokeOnGetShot(Bullet bullet, Collision collision)
		=> onGetShot.Invoke(new(this, bullet, collision));
}
