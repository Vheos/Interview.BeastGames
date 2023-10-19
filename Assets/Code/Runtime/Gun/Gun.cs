using UnityEngine;

public class Gun : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private GunAttributes attributes;
	[SerializeField] private GunAnchors anchors;
	[SerializeField] private Bullet bulletPrefab;
	[Header(Headers.Events)]
	[SerializeField] private OnShoot.Event OnShoot;

	public GunAttributes Attributes
		=> attributes;
	public GunAnchors Anchors
		=> anchors;

	private void ApplyGripOffset()
		=> transform.localPosition = -anchors.Grip.localPosition;

	public bool TryShoot()
	{
		OnShoot.Invoke(new(this));

		for (int i = 0; i < attributes.BulletCount; i++)
			Bullet.Spawn(bulletPrefab, this);

		return true;
	}

	private void Awake()
	{
		ApplyGripOffset();
	}
}
