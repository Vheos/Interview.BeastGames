using UnityEngine;

public class SnapToTransform : MonoBehaviour
{
	[field: SerializeField] public Transform Target { get; set; }

	private void LateUpdate()
		=> transform.SnapTo(Target);
}
