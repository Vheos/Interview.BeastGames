using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnchors : MonoBehaviour
{
    [field: SerializeField] public Transform Grip { get; private set; }
	[field: SerializeField] public Transform Handle { get; private set; }
}
