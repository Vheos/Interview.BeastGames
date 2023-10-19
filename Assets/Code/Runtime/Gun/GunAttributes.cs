using System;
using System.Collections.Generic;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(0f, 10f)] private float damage = 5f;
	[SerializeField, Range(1, 10)] private int bulletCount = 1;
	[SerializeField] private DamageModifier[] damageModifiers;
	[SerializeField, Range(0f, 1f)] private float fallbackDamageModifier = 1f;

	public float Damage
		=> damage;
	public int BulletCount
		=> bulletCount;
	public IReadOnlyList<DamageModifier> DamageModifiers
		=> damageModifiers;
	public float FallbackDamageModifier
		=> fallbackDamageModifier;
}
