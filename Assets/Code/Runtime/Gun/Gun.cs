using UnityEngine;

public class Gun : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private GunAttributes attributes;
	[SerializeField] private ParticleSystem muzzleParticle;
	[SerializeField] private Crosshair crosshair;
	[Header(Headers.Events)]
	[SerializeField] private OnShoot.Event OnShoot;

	// Public
	public GunAttributes Attributes
		=> attributes;
	public GunInventory Inventory
	{ get; set; }
	public bool TryShoot()
	{
		OnShoot.Invoke(new(this));

		for (int i = 0; i < attributes.BulletCount; i++)
			Bullet.Spawn(attributes.BulletPrefab, this);

		muzzleParticle.Play();
		ApplyRecoil();

		return true;
	}
	public Vector3 GetNearMuzzlePoint(Camera camera)
		=> muzzleParticle.transform.position.RetainScreenPositionAtDistance(camera.nearClipPlane * 2, camera);
	public float GetDamageModifierFor(ArmorType armorType)
	{
		foreach (var damageModifier in attributes.DamageModifiers)
			if (damageModifier.ArmorType == armorType)
				return damageModifier.Multiplier;

		return attributes.FallbackDamageModifier;
	}
	public float GetDamageDealtTo(ArmorType armorType)
		=> attributes.BaseDamage * GetDamageModifierFor(armorType);
	public float GetDamageModifierFor(Destructible destructible)
		=> GetDamageModifierFor(destructible.Attributes.ArmorType);
	public float GetDamageDealtTo(Destructible destructible)
		=> GetDamageDealtTo(destructible.Attributes.ArmorType);

	// Private
	private void ApplyRecoil()
		=> Inventory.transform.localRotation *= Quaternion.Euler(-attributes.Recoil, 0f, 0f);
	private void RecoverFromRecoil()
		=> Inventory.transform.localRotation = Quaternion.Lerp(Inventory.transform.localRotation, Quaternion.identity, 3f * Time.deltaTime);

	// Mono
	protected void OnEnable()
	{
		crosshair.UpdatePosition(this, Camera.main, true);
	}
	protected void Update()
	{
		RecoverFromRecoil();
		crosshair.UpdatePosition(this, Camera.main);
	}
}