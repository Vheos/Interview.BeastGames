using UnityEngine;
using ActionContext = UnityEngine.InputSystem.InputAction.CallbackContext;


internal class PlayerController : MonoBehaviour
{
	// Inspector
	[Header("Dependencies")]
	[SerializeField] private CharacterController characterController;
	[SerializeField] private Transform cameraAnchor;
	[SerializeField] private GunInventory gunInventory;
	[Header("Move")]
	[SerializeField] private float maxMoveSpeed = 5f;
	[SerializeField] private float moveAcceleration = 0.5f;
	[Header("Fall")]
	[SerializeField] private float maxFallSpeed = 50f;
	[SerializeField] private float fallAcceleration = 0.5f;
	[Header("Look")]
	[SerializeField] private Vector2 lookSpeed = new(+5f, -4f);
	[SerializeField] private float maxPitch = 60f;

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
	private float FallSpeed
	{
		get
		{
			float currentSpeed = Mathf.Abs(characterController.velocity.y);
			float acceleration = fallAcceleration;
			return Mathf.Min(currentSpeed + acceleration, maxFallSpeed);
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
			float offsetAngle = lookInput.x * lookSpeed.x * Time.deltaTime;
			return currentAngle + offsetAngle;
		}
	}
	private float LookPitch
	{
		get
		{
			float currentAngle = cameraAnchor.localEulerAngles.x;
			float offsetAngle = lookInput.y * lookSpeed.y * Time.deltaTime;
			return ClampAngle(currentAngle + offsetAngle, -maxPitch, +maxPitch);
		}
	}


	private void OnShootGun(ActionContext context)
	{
		gunInventory.TryShoot();
	}
	private void OnSwitchGun(ActionContext context)
	{
		gunInventory.TrySwitch();
	}

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
		actions.ShootGun.performed += OnShootGun;
		actions.SwitchGun.performed += OnSwitchGun;
		actions.Enable();
	}

	private void OnDisable()
	{
		actions.Move.performed -= SetMoveInput;
		actions.Move.canceled -= ResetMoveInput;
		actions.Look.performed -= AccumulateLookInput;
		actions.ShootGun.performed -= OnShootGun;
		actions.SwitchGun.performed -= OnSwitchGun;
		actions.Disable();
	}

	private void Update()
	{
		Look();
		Move();
	}
}