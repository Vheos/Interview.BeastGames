using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using ActionContext = UnityEngine.InputSystem.InputAction.CallbackContext;


internal class PlayerController : MonoBehaviour
{
	// Inspector
	[field: Header("Dependencies")]
	[field: SerializeField] public CharacterController CharacterController { get; private set; }
	[field: SerializeField] public Transform CameraAnchor { get; private set; }
	[field: Header("Move")]
	[field: SerializeField, Range(1f, 10f)] public float MaxMoveSpeed { get; private set; } = 5f;
	[field: SerializeField, Range(0.1f, 1f)] public float MoveAcceleration { get; private set; } = 0.5f;
	[field: Header("Fall")]
	[field: SerializeField] public float MaxFallSpeed { get; private set; } = 50f;
	[field: SerializeField] public float FallAcceleration { get; private set; } = 0.5f;
	[field: Header("Look")]
	[field: SerializeField] public Vector2 LookSpeed { get; private set; } = new(-5f, -4f);
	[field: SerializeField] public float MaxPitch { get; private set; } = 60f;

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
		CharacterController.Move(motion * Time.deltaTime);
	}
	private float MoveSpeed
	{
		get
		{
			Vector3 horizontalVelocity = new(CharacterController.velocity.x, 0f, CharacterController.velocity.z);
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
			float currentSpeed = Mathf.Abs(CharacterController.velocity.y);
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
		CameraAnchor.localRotation = Quaternion.Euler(LookPitch, 0f, 0f);
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
			float currentAngle = CameraAnchor.localEulerAngles.x;
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