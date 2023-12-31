using UnityEngine;

public class DestructibleAttributes : MonoBehaviour
{
	// Inspector
	[Header(Headers.Prefabs)]
	[SerializeField] private HitParticle hitParticlePrefab;
	[Header(Headers.Values)]
	[SerializeField, Range(1f, 100f)] private float maxHealth = 50f;
	[SerializeField] private ArmorType armorType = default;

	// Public
	public float MaxHealth
		=> maxHealth;
	public ArmorType ArmorType
		=> armorType;
	public HitParticle HitParticlePrefab
		=> hitParticlePrefab;
}