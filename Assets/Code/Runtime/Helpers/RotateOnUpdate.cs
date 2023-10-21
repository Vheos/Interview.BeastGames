using UnityEngine;

public class RotateOnUpdate : MonoBehaviour
{
	// Inspector
	[Header(Headers.Values)]
	[SerializeField] private Vector3 angles;

	// Mono
	protected void Update()
	{
		if (angles != Vector3.zero)
			transform.eulerAngles += angles * Time.deltaTime;
	}
}
