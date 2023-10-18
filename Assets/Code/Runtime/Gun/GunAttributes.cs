using System;
using System.Collections.Generic;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(0f, 10f)] private float damage = 5f;
	[SerializeField] private DamageModifier[] overrideDamageModifiers;
	[SerializeField, Range(0f, 2f)] private float fallbackDamageModifier = 1f;

	public float Damage
		=> damage;
	public IReadOnlyList<DamageModifier> OverrideDamageModifiers
		=> overrideDamageModifiers;
	public float FallbackDamageModifier
		=> fallbackDamageModifier;
}
