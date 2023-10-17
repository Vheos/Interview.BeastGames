using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToTransform : MonoBehaviour
{
	[SerializeField] private Transform Target;

	void Update()
	{
		transform.position = Target.position;
		transform.rotation = Target.rotation;
	}
}
