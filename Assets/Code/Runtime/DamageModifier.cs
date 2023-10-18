using System;
using UnityEngine;

[Serializable]
public struct DamageModifier
{
	[field: SerializeField] public ArmorType Armor { get; private set; }
	[field: SerializeField, Range(0f, 2f)] public float Multiplier { get; private set; }
}