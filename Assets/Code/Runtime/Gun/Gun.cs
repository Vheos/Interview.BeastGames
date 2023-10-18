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

	public void ApplyGripOffset()
		=> transform.localPosition = -anchors.Grip.localPosition;

	public bool TryShoot()
	{
		Bullet newBullet = Bullet.Spawn(bulletPrefab, this);

		OnShoot.Invoke(new(this, newBullet));
		return true;
	}

	private void Start()
	{
		ApplyGripOffset();
	}
}
