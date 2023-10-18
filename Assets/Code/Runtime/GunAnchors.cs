using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnchors : MonoBehaviour
{
	[SerializeField] private Transform grip;
	[SerializeField] private Transform muzzle;

	public Vector3 Grip
		=> grip.position;
	public Vector3 Muzzle
		=> muzzle.position;
}
