using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunInventory : MonoBehaviour
{
	// Inspector
	[Header(Headers.Events)]
	[SerializeField] private OnAddGun.Event OnAddGun;
	[SerializeField] private OnRemoveGun.Event OnRemoveGun;
	[SerializeField] private OnSwitchGun.Event OnSwitchGun;

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

			OnSwitchGun?.Invoke(new(this, previousGun, currentGun));
		}
	}

	public void Add(Gun gun)
	{
		gun.transform.parent = transform;
		gun.Inventory = this;
		gun.gameObject.SetActive(false);
		guns.Add(gun);
		OnAddGun.Invoke(new(this, gun));
	}
	public bool TryRemove(Gun gun)
	{
		if (!guns.Remove(gun) || gun == currentGun)
			return false;

		gun.transform.parent = null;
		gun.Inventory = null;
		OnRemoveGun.Invoke(new(this, gun));
		return true;
	}
	public bool TrySwitchToNext()
	{
		if (guns.Count < 2)
			return false;

		int currentIndex = guns.IndexOf(currentGun);
		int nextIndex = (currentIndex + 1) % guns.Count;
		CurrentGun = guns[nextIndex];
		return true;
	}
	public bool TryShoot()
		=> currentGun != null && currentGun.TryShoot();

	private void Awake()
	{
		guns = new List<Gun>();

		foreach (var childGun in GetComponentsInChildren<Gun>(true))
			Add(childGun);

		CurrentGun = guns.FirstOrDefault();
	}
}



