using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class DemoInput : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private PlayerController player;
	[SerializeField] private GunInventory gunInventory;
	[SerializeField] private Camera playerCamera;
	[SerializeField] private Camera gunCamera;
	[SerializeField] private TextMeshProUGUI weaponName;
	[SerializeField] private TextMeshProUGUI armorInfo;
	[SerializeField] private TextMeshProUGUI damageInfo;
	[SerializeField] private RectTransform[] panels;

	// Fields
	private float originalInventoryLocalX;
	private int inventoryPositionId;
	private bool isHudVisible = true;
	private Vector2 fovRange = new(30, 90);
	private float currentFov;

	// Delegates
	public void UpdateWeaponHUD(OnSwitchGun.Data data)
	{
		weaponName.text = data.To.name;
		var damages = Enum.GetValues(typeof(ArmorType)).Cast<ArmorType>().Select(t => $"{data.To.GetDamageModifierFor(t):P0} dmg");
		damageInfo.text = string.Join('\n', damages);
	}

	// Private
	private void UpdateFoV()
	{
		float mouseScroll = Mouse.current.scroll.y.value / 120f;
		if (mouseScroll != 0f)
		{
			float targetFov = mouseScroll > 0f ? fovRange.x : fovRange.y;
			currentFov = Mathf.Lerp(currentFov, targetFov, 0.125f);
		}

		playerCamera.fieldOfView = gunCamera.fieldOfView =
			Mathf.Lerp(playerCamera.fieldOfView, currentFov, 5f * Time.deltaTime);
	}
	private void ToggleHUD()
	{
		if (Keyboard.current.hKey.wasPressedThisFrame)
		{
			isHudVisible = !isHudVisible;
			foreach (var panel in panels)
				panel.DOScale(isHudVisible ? Vector3.one : Vector3.zero, 1f);
		}
	}
	private void MoveGun()
	{
		if (Keyboard.current.tabKey.wasPressedThisFrame)
		{
			inventoryPositionId = (inventoryPositionId + 2).PosMod(3) - 1;
			Vector3 targetPosition = gunInventory.transform.localPosition;
			targetPosition.x = originalInventoryLocalX * inventoryPositionId;
			gunInventory.transform.DOLocalMove(targetPosition, 1f);
		}
	}
	private void InvertY()
	{
		if (Keyboard.current.iKey.wasPressedThisFrame)
			player.InvertedY = !player.InvertedY;
	}
	private static void Quit()
	{
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
			Application.Quit();
	}

	// Mono
	protected void Awake()
	{
		originalInventoryLocalX = gunInventory.transform.localPosition.x;
		inventoryPositionId = originalInventoryLocalX.Sig();
		currentFov = playerCamera.fieldOfView;
		armorInfo.text = string.Join('\n', Enum.GetNames(typeof(ArmorType)).Select(t => $"vs {t}"));
		gunInventory.OnSwitchGun.AddListener(UpdateWeaponHUD);
	}
	protected void Update()
	{
		Quit();
		InvertY();
		MoveGun();
		ToggleHUD();
		UpdateFoV();
	}
}