using UnityEngine;

public class CursorSettings : MonoBehaviour
{
	// Inspector
	[SerializeField] private CursorLockMode mode = CursorLockMode.Locked;
	[SerializeField] private bool visible = false;

	private void Awake()
	{
		Cursor.lockState = mode;
		Cursor.visible = visible;
	}
}
