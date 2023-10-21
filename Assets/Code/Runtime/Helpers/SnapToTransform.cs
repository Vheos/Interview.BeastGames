using UnityEngine;

public class SnapToTransform : MonoBehaviour
{
	// Inspector
	[Header(Headers.Values)]
	[SerializeField] private Transform target;

	// Public
	public Transform Target
	{
		get => target;
		set => target = value;
	}

	// Mono
	protected void LateUpdate()
	{
		if (Target != null)
			transform.SnapTo(Target);
	}
}
