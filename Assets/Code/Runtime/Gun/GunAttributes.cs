using System;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
	[SerializeField, Range(0f, 10f)] private float damage = 5f;
	[SerializeField] private DamageModifier[] overrideDamageModifiers;
	[SerializeField, Range(0f, 2f)] private float fallbackDamageModifier = 1f;
}
