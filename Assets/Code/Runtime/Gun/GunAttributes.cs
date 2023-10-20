using System;
using System.Collections.Generic;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField, Range(0f, 10f)] private float damage = 5f;
	[SerializeField, Range(1, 10)] private int bulletCount = 1;
	[SerializeField, Range(0f, 15f)] private float bulletSpread = 5f;
	[SerializeField, Range(0f, 30f)] private float recoil = 5f;

	[SerializeField] private DamageModifier[] damageModifiers;
	[SerializeField, Range(0f, 1f)] private float fallbackDamageModifier = 1f;

	public Bullet BulletPrefab
		=> bulletPrefab;
	public float Damage
		=> damage;
	public int BulletCount
		=> bulletCount;
	public float BulletSpread
		=> bulletSpread;
	public float Recoil
		=> recoil;
	public IReadOnlyList<DamageModifier> DamageModifiers
		=> damageModifiers;
	public float FallbackDamageModifier
		=> fallbackDamageModifier;
}
