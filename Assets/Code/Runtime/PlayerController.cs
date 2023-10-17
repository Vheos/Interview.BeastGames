using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using ActionContext = UnityEngine.InputSystem.InputAction.CallbackContext;


internal class PlayerController : MonoBehaviour
{
	[SerializeField, Range(0.1f, 1f)] private float MoveSpeed;
	[SerializeField, Range(0.1f, 1f)] private float MoveAcceleration;
	[SerializeField] private CharacterController characterController;
	[SerializeField] private Transform cameraAnchor;

	private float currentMoveSpeed;
	private float currentLookSpeed;

	private FPSActions.PlayerActions actions;
	private Vector2 moveInput;
	private Vector2 lookInput;

	private void SetMoveInput(ActionContext context)
		=> moveInput = context.ReadValue<Vector2>();
	private void ResetMoveInput(ActionContext context)
		=> moveInput = default;
	private void AddLookInput(ActionContext context)
		=> lookInput += context.ReadValue<Vector2>();


	private void Move()
	{
		if (moveInput == default)
			return;

		Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
		cameraAnchor.transform.rotation


		characterController.Move(direction);
	}
	private void Look()
	{
		if (lookInput == default)
			return;

		Debug.Log(lookInput);
		lookInput = default;
	}
	private void OnJump(ActionContext context)
		=> Debug.Log($"{context.action.name}");
	private void OnFire(ActionContext context)
		=> Debug.Log($"{context.action.name}");
	private void OnSwitch(ActionContext context)
		=> Debug.Log($"{context.action.name}");



	private void Awake()
	{
		actions = new FPSActions().Player;
	}

	private void OnEnable()
	{
		actions.Move.performed += SetMoveInput;
		actions.Move.canceled += ResetMoveInput;
		actions.Look.performed += AddLookInput;
		actions.Jump.performed += OnJump;
		actions.Fire.performed += OnFire;
		actions.Switch.performed += OnSwitch;
		actions.Enable();
	}

	private void OnDisable()
	{
		actions.Move.performed -= SetMoveInput;
		actions.Move.canceled -= ResetMoveInput;
		actions.Look.performed -= AddLookInput;
		actions.Jump.performed -= OnJump;
		actions.Fire.performed -= OnFire;
		actions.Switch.performed -= OnSwitch;
		actions.Disable();
	}

	private void FixedUpdate()
	{
		Look();
		Move();
	}
}