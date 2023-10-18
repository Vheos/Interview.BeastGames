using UnityEngine;

public class CursorSettings : MonoBehaviour
{
	[SerializeField] private CursorLockMode mode = CursorLockMode.Locked;
	[SerializeField] private bool visible = false;

	private void Start()
	{
		Cursor.lockState = mode;
		Cursor.visible = visible;
	}
}
