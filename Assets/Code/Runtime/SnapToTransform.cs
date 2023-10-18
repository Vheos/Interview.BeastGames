using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToTransform : MonoBehaviour
{
	[field: SerializeField] public Transform Target { get; private set; }

	public void SnapPosition()
		=> transform.position = Target.position;

	public void SnapRotation()
		=> transform.rotation = Target.rotation;

	void LateUpdate()
	{
		SnapPosition();
		SnapRotation();
	}
}
