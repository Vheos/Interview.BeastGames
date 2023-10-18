using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
	public SwitchGun.Event OnSwitchGun;
	public ShootGun.Event OnShootGun;

	private List<Gun> guns;
	private Gun currentGun;

	public IReadOnlyList<Gun> Guns
		=> guns;
	public Gun CurrentGun
	{
		get => currentGun;
		set
		{
			if (currentGun == value)
				return;

			Gun previousGun = currentGun;
			if (previousGun != null)
				previousGun.gameObject.SetActive(false);

			currentGun = value;
			if (currentGun != null)
				currentGun.gameObject.SetActive(true);

			OnSwitchGun?.Invoke(new(previousGun, currentGun));
		}
	}

	public bool TrySwitch()
	{
		if (guns.Count < 2)
			return false;

		int currentIndex = guns.IndexOf(currentGun);
		int nextIndex = (currentIndex + 1) % guns.Count;
		CurrentGun = guns[nextIndex];
		return true;
	}
	public bool TryShoot()
	{
		if (currentGun == null)
			return false;

		if (currentGun.TryShoot())
			OnShootGun.Invoke(new(currentGun));

		return true;
	}

	private void Awake()
	{
		guns = new List<Gun>(GetComponentsInChildren<Gun>(true));
		guns.ForEach(gun => gun.gameObject.SetActive(false));
		CurrentGun = guns.FirstOrDefault();
	}
}



