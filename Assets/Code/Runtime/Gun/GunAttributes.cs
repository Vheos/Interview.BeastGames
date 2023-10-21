using System;
using System.Collections.Generic;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
	// Inspector
	[Header(Headers.Prefabs)]
	[SerializeField] private Bullet bulletPrefab;
	[Header("Bullet")]
	[SerializeField, Range(1, 10)] private int bulletCount = 1;
	[SerializeField, Range(0f, 15f)] private float bulletSpread = 5f;
	[SerializeField, Range(0f, 30f)] private float recoil = 5f;
	[Header("Damage")]
	[SerializeField, Range(0f, 10f)] private float baseDamage = 5f;
	[SerializeField] private DamageModifier[] damageModifiers;
	[SerializeField, Range(0f, 1f)] private float fallbackDamageModifier = 1f;

	// Public
	public Bullet BulletPrefab
		=> bulletPrefab;
	public float BaseDamage
		=> baseDamage;
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