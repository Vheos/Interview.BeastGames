using UnityEngine;

public class DestructibleAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(1f, 100f)] private float maxHealth = 50f;
	[SerializeField] private ArmorType armorType = default;

	public float MaxHealth
		=> maxHealth;
	public ArmorType ArmorType
		=> armorType;
}
