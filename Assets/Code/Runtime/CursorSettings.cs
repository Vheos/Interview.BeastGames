using UnityEngine;

public class CursorSettings : MonoBehaviour
{
	// Inspector
	[SerializeField] private CursorLockMode mode = CursorLockMode.Locked;
	[SerializeField] private bool visible = false;

	private void Start()
	{
		Cursor.lockState = mode;
		Cursor.visible = visible;
	}
}
