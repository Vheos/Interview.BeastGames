using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] private GunAttributes attributes;
	[SerializeField] private GunAnchors anchors;
	[SerializeField] private Bullet bulletPrefab;

	public GunAttributes Attributes
		=> attributes;
	public GunAnchors Anchors
		=> anchors;

	public void ApplyGripOffset()
		=> transform.localPosition = -anchors.Grip.localPosition;

	public bool TryShoot()
	{
		Bullet.Spawn(bulletPrefab, this);
		return true;
	}

	private void Start()
	{
		ApplyGripOffset();
	}
}
