using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GunInventory : MonoBehaviour
{
	public SwitchGun.Event OnSwitchGun;

	private List<Gun> guns;
	private Gun currentGun;

	public IReadOnlyList<Gun> Guns
		=> guns;

	public bool TryEquip(Gun newGun)
	{
		if (currentGun == newGun)
			return false;

		Gun previousGun = currentGun;
		if (previousGun != null)
			previousGun.gameObject.SetActive(false);

		currentGun = newGun;
		if (currentGun != null)
			currentGun.gameObject.SetActive(true);

		OnSwitchGun?.Invoke(new(previousGun, currentGun));
		return true;
	}

	public bool TrySwitch()
	{
		if (guns.Count < 2)
			return false;

		int currentIndex = guns.IndexOf(currentGun);
		int nextIndex = (currentIndex + 1) % guns.Count;
		Gun nextGun = guns[nextIndex];
		TryEquip(nextGun);
		return true;
	}

	private void Awake()
	{
		guns = new List<Gun>(GetComponentsInChildren<Gun>(true));
		TryEquip(guns.FirstOrDefault());
	}
}



