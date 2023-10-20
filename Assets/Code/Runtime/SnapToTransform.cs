using UnityEngine;

public class SnapToTransform : MonoBehaviour
{
	[field: SerializeField] public Transform Target { get; set; }

	private void LateUpdate()
	{
		if (Target != null)
			transform.SnapTo(Target);
	}
}
