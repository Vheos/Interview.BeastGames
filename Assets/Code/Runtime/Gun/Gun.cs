using UnityEngine;

public class Gun : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private GunAttributes attributes;
	[SerializeField] private GunAnchors anchors;
	[SerializeField] private ParticleSystem muzzleParticle;
	[Header(Headers.Events)]
	[SerializeField] private OnShoot.Event OnShoot;

	public GunAttributes Attributes
		=> attributes;
	public GunInventory Inventory { get; set; }

	public bool TryShoot()
	{
		OnShoot.Invoke(new(this));

		for (int i = 0; i < attributes.BulletCount; i++)
			Bullet.Spawn(attributes.BulletPrefab, this);

		muzzleParticle.Play();

		transform.localRotation *= Quaternion.Euler(-attributes.Recoil, 0f, 0f);

		return true;
	}

	public float GetDamageModifierFor(ArmorType armorType)
	{
		foreach (var damageModifier in attributes.DamageModifiers)
			if (damageModifier.ArmorType == armorType)
				return damageModifier.Multiplier;

		return attributes.FallbackDamageModifier;
	}
	public float GetDamageDealtTo(ArmorType armorType)
		=> attributes.Damage * GetDamageModifierFor(armorType);
	public float GetDamageModifierFor(Destructible destructible)
		=> GetDamageModifierFor(destructible.Attributes.ArmorType);
	public float GetDamageDealtTo(Destructible destructible)
		=> GetDamageDealtTo(destructible.Attributes.ArmorType);
	public Vector3 GetNearMuzzlePoint(Camera playerCamera)
	{
		Vector3 worldPosition = anchors.Muzzle.transform.position;
		Vector3 screenPosition = playerCamera.WorldToScreenPoint(worldPosition);
		screenPosition.z = playerCamera.nearClipPlane * 2;
		return playerCamera.ScreenToWorldPoint(screenPosition);
	}

	private void Update()
	{
		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, 3f * Time.deltaTime);
	}
}
