using UnityEngine;

public class SnapToTransform : MonoBehaviour
{
	[field: SerializeField] public Transform Target { get; private set; }

	public void SnapPosition()
		=> transform.position = Target.position;

	public void SnapRotation()
		=> transform.rotation = Target.rotation;

	private void LateUpdate()
	{
		SnapPosition();
		SnapRotation();
	}
}
