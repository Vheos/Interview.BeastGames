using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using ActionContext = UnityEngine.InputSystem.InputAction.CallbackContext;


internal class PlayerController : MonoBehaviour
{
	// Inspector
	[Header("Dependencies")]
	[SerializeField] private CharacterController characterController;
	[SerializeField] private Transform cameraAnchor;
	[Header("Move")]
	[SerializeField, Range(1f, 10f)] private float MaxMoveSpeed = 5f;
	[SerializeField, Range(0.1f, 1f)] private float MoveAcceleration = 0.5f;
	[Header("Fall")]
	[SerializeField, Range(10f, 100f)] private float MaxFallSpeed = 50f;
	[SerializeField, Range(0.1f, 1f)] private float FallAcceleration = 0.5f;
	[Header("Look")]
	[SerializeField] private Vector2 LookSpeed = new(-5f, -4f);
	[SerializeField, Range(60f, 90f)] private float MaxPitch = 60f;

	// Fields
	private FPSActions.PlayerActions actions;
	private Vector2 moveInput;
	private Vector2 lookInput;

	// Input
	private void SetMoveInput(ActionContext context)
		=> moveInput = context.ReadValue<Vector2>();
	private void ResetMoveInput(ActionContext context)
		=> moveInput = default;
	private void AccumulateLookInput(ActionContext context)
		=> lookInput += context.ReadValue<Vector2>();
	private void ResetLookInput()
		=> lookInput = default;

	// Move & Fall
	private void Move()
	{
		Vector3 motion = MoveSpeed * MoveDirection + FallSpeed * FallDirection;
		characterController.Move(motion * Time.deltaTime);
	}
	private float MoveSpeed
	{
		get
		{
			Vector3 horizontalVelocity = new(characterController.velocity.x, 0f, characterController.velocity.z);
			float currentSpeed = horizontalVelocity.magnitude;
			float acceleration = MoveAcceleration;
			return Mathf.Min(currentSpeed + acceleration, MaxMoveSpeed);
		}
	}
	private Vector3 MoveDirection
	{
		get
		{
			Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
			return transform.rotation * inputDirection;
		}
	}
	private float FallSpeed
	{
		get
		{
			float currentSpeed = Mathf.Abs(characterController.velocity.y);
			float acceleration = FallAcceleration;
			return Mathf.Min(currentSpeed + acceleration, MaxFallSpeed);
		}
	}
	private Vector3 FallDirection
		=> Vector3.down;

	// Look
	private void Look()
	{
		transform.localRotation = Quaternion.Euler(0f, LookYaw, 0f);
		cameraAnchor.localRotation = Quaternion.Euler(LookPitch, 0f, 0f);
		ResetLookInput();
	}
	private float LookYaw
	{
		get
		{
			float currentAngle = transform.localEulerAngles.y;
			float offsetAngle = lookInput.x * LookSpeed.x * Time.deltaTime;
			return currentAngle + offsetAngle;
		}
	}
	private float LookPitch
	{
		get
		{
			float currentAngle = cameraAnchor.localEulerAngles.x;
			float offsetAngle = lookInput.y * LookSpeed.y * Time.deltaTime;
			return ClampAngle(currentAngle + offsetAngle, -MaxPitch, +MaxPitch);
		}
	}


	private void OnFire(ActionContext context)
		=> Debug.Log($"{context.action.name}");
	private void OnSwitch(ActionContext context)
		=> Debug.Log($"{context.action.name}");

	private static float ClampAngle(float angle, float min, float max)
	{
		float c = 360f;
		if (angle <= -c / 2f)
			angle += c;
		if (angle >= c / 2f)
			angle += -c;

		return Mathf.Clamp(angle, min, max);
	}


	private void Awake()
	{
		actions = new FPSActions().Player;
	}

	private void OnEnable()
	{
		actions.Move.performed += SetMoveInput;
		actions.Move.canceled += ResetMoveInput;
		actions.Look.performed += AccumulateLookInput;
		actions.Fire.performed += OnFire;
		actions.Switch.performed += OnSwitch;
		actions.Enable();
	}

	private void OnDisable()
	{
		actions.Move.performed -= SetMoveInput;
		actions.Move.canceled -= ResetMoveInput;
		actions.Look.performed -= AccumulateLookInput;
		actions.Fire.performed -= OnFire;
		actions.Switch.performed -= OnSwitch;
		actions.Disable();
	}

	private void Update()
	{
		Look();
		Move();
	}
}