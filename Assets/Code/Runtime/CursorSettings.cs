using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSettings : MonoBehaviour
{
	[field: SerializeField] public CursorLockMode Mode { get; private set; } = CursorLockMode.Locked;
	[field: SerializeField] public bool Visible { get; private set; } = false;

	void Start()
    {
		Cursor.lockState = Mode;
		Cursor.visible = false;
    }
}
