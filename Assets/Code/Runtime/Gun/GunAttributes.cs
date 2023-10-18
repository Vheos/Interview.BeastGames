using System;
using System.Collections.Generic;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(0f, 10f)] private float damage = 5f;
	[SerializeField] private DamageModifier[] damageModifiers;
	[SerializeField, Range(0f, 1f)] private float fallbackDamageModifier = 1f;

	public float Damage
		=> damage;
	public IReadOnlyList<DamageModifier> DamageModifiers
		=> damageModifiers;
	public float FallbackDamageModifier
		=> fallbackDamageModifier;

	public float GetDamageModifierFor(ArmorType armorType)
	{
		foreach (var damageModifier in damageModifiers)
			if (damageModifier.ArmorType == armorType)
				return damageModifier.Multiplier;

		return fallbackDamageModifier;
	}
	public float GetDamageDealtTo(ArmorType armorType)
		=> damage * GetDamageModifierFor(armorType);
}
