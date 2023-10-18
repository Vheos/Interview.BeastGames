using UnityEngine;

public class Gun : MonoBehaviour, IShooter
{
	public bool TryShoot()
	{
		return true;
	}
}
