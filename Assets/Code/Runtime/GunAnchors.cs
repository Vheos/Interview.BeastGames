using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnchors : MonoBehaviour
{
	[SerializeField] private Transform grip;
	[SerializeField] private Transform handle;

	public Vector3 Grip
		=> grip.position;
	public Vector3 Handle
		=> handle.position;
}
