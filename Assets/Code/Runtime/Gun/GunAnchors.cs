using UnityEngine;

public class GunAnchors : MonoBehaviour
{
	// Inspector
	[SerializeField] private Transform grip;
	[SerializeField] private Transform muzzle;

	public Transform Grip
		=> grip;
	public Transform Muzzle
		=> muzzle;
}
