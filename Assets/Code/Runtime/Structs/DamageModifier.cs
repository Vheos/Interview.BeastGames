using System;
using UnityEngine;

[Serializable]
public struct DamageModifier
{
	[field: SerializeField] public ArmorType ArmorType { get; private set; }
	[field: SerializeField, Range(0f, 1f)] public float Multiplier { get; private set; }
}