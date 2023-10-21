using UnityEngine;

public class CursorSettings : MonoBehaviour
{
	// Inspector
	[Header(Headers.Values)]
	[SerializeField] private CursorLockMode mode = CursorLockMode.Locked;
	[SerializeField] private bool visible = false;

	// Mono
	protected void Awake()
	{
		Cursor.lockState = mode;
		Cursor.visible = visible;
	}
}
