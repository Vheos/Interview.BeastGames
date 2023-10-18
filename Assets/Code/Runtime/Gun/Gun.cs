using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private GunAttributes attributes;
	[SerializeField] private GunAnchors anchors;
	[SerializeField] private Bullet bulletPrefab;

	public void ApplyGripOffset()
		=> transform.localPosition = -anchors.GripLocal;

	public bool TryShoot()
	{
		Bullet bullet = Instantiate(bulletPrefab, anchors.MuzzleWorld, Quaternion.identity);

		return true;
	}

	private void Start()
	{
		ApplyGripOffset();
	}
}
