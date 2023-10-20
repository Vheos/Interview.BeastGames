using UnityEngine;

public class DestructibleAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(1f, 100f)] private float maxHealth = 50f;
	[SerializeField] private ArmorType armorType = default;
	[SerializeField] private HitParticle hitParticlePrefab;

	public float MaxHealth
		=> maxHealth;
	public ArmorType ArmorType
		=> armorType;
	public HitParticle HitParticlePrefab
		=> hitParticlePrefab;
}
