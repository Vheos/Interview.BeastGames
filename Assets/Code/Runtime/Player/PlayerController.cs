using UnityEngine;
using ActionContext = UnityEngine.InputSystem.InputAction.CallbackContext;


internal class PlayerController : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private CharacterController characterController;
	[SerializeField] private Transform cameraAnchor;
	[SerializeField] private GunInventory gunInventory;
	[Header("Move")]
	[SerializeField, Range(1f, 10f)] private float maxMoveSpeed = 5f;
	[SerializeField, Range(0.1f, 1f)] private float moveAcceleration = 0.5f;
	[Header("Jump")]
	[SerializeField, Range(2f, 20f)] private float jumpHeight = 10f;
	[Header("Fall")]
	[SerializeField, Range(10f, 100f)] private float maxFallSpeed = 50f;
	[SerializeField, Range(0f, 1f)] private float fallAcceleration = 0.5f;
	[Header("Look")]
	[SerializeField] private Vector2 lookSpeed = new(+5f, -4f);
	[SerializeField, Range(60f, 90f)] private float maxPitch = 60f;

	// Fields
	private FPSActions.PlayerActions actions;
	private Vector2 moveInput;
	private Vector2 lookInput;
	private bool jumpInput;

	// Public
	public bool InvertedY
	{
		get => lookSpeed.y < 0f;
		set => lookSpeed.y = Mathf.Abs(lookSpeed.y) * (value ? -1f : +1f);
	}

	// Private
	private float MoveSpeed
	{
		get
		{
			Vector3 horizontalVelocity = new(characterController.velocity.x, 0f, characterController.velocity.z);
			float currentSpeed = horizontalVelocity.magnitude;
			float acceleration = moveAcceleration;
			return Mathf.Min(currentSpeed + acceleration, maxMoveSpeed);
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
	private float JumpSpeed
	{ get; set; }
	private Vector3 JumpDirection
		=> Vector3.up;
	private float FallSpeed
	{ get; set; }
	private Vector3 FallDirection
		=> Vector3.down;
	private float LookYaw
	{
		get
		{
			float currentAngle = transform.localEulerAngles.y;
			float offsetAngle = lookInput.x * lookSpeed.x / 100f;
			return currentAngle + offsetAngle;
		}
	}
	private float LookPitch
	{
		get
		{
			float currentAngle = cameraAnchor.localEulerAngles.x;
			float offsetAngle = lookInput.y * lookSpeed.y / 100f;
			return Helpers.ClampAngle(currentAngle + offsetAngle, -maxPitch, +maxPitch);
		}
	}
	private void UpdateJumpSpeed()
	{
		if (characterController.isGrounded)
			JumpSpeed = jumpInput ? jumpHeight : 0f;

		ResetJumpInput();
	}
	private void UpdateFallSpeed()
	{
		if (characterController.isGrounded)
			FallSpeed = 0f;

		FallSpeed = Mathf.Min(FallSpeed + fallAcceleration, maxFallSpeed);
	}
	private void Move()
	{
		Vector3 motion = MoveSpeed * MoveDirection + JumpSpeed * JumpDirection + FallSpeed * FallDirection;
		characterController.Move(motion * Time.deltaTime);
	}
	private void Look()
	{
		transform.localRotation = Quaternion.Euler(0f, LookYaw, 0f);
		cameraAnchor.localRotation = Quaternion.Euler(LookPitch, 0f, 0f);
		ResetLookInput();
	}
	private void OnShootGun(ActionContext context)
		=> gunInventory.TryShoot();
	private void OnSwitchGun(ActionContext context)
		=> gunInventory.TrySwitchToNext();


	// Input reading
	private void SetMoveInput(ActionContext context)
		=> moveInput = context.ReadValue<Vector2>();
	private void ResetMoveInput(ActionContext _)
		=> moveInput = default;
	private void SetJumpInput(ActionContext _)
		=> jumpInput = true;
	private void ResetJumpInput()
		=> jumpInput = false;
	private void AccumulateLookInput(ActionContext context)
		=> lookInput += context.ReadValue<Vector2>();
	private void ResetLookInput()
		=> lookInput = default;

	// Mono
	protected void Awake()
	{
		actions = new FPSActions().Player;
	}
	protected void OnEnable()
	{
		actions.Move.performed += SetMoveInput;
		actions.Move.canceled += ResetMoveInput;
		actions.Jump.performed += SetJumpInput;
		actions.Look.performed += AccumulateLookInput;
		actions.ShootGun.performed += OnShootGun;
		actions.SwitchGun.performed += OnSwitchGun;
		actions.Enable();
	}
	protected void OnDisable()
	{
		actions.Move.performed -= SetMoveInput;
		actions.Move.canceled -= ResetMoveInput;
		actions.Jump.performed -= SetJumpInput;
		actions.Look.performed -= AccumulateLookInput;
		actions.ShootGun.performed -= OnShootGun;
		actions.SwitchGun.performed -= OnSwitchGun;
		actions.Disable();
	}
	protected void Update()
	{
		Look();
		UpdateJumpSpeed();
		UpdateFallSpeed();
		Move();
	}
}