using UnityEngine;

public class GunAnchors : MonoBehaviour
{
	[SerializeField] private Transform grip;
	[SerializeField] private Transform muzzle;

	public Vector3 GripLocal
		=> grip.localPosition;
	public Vector3 GripWorld
		=> grip.position;
	public Vector3 MuzzleLocal
		=> muzzle.localPosition;
	public Vector3 MuzzleWorld
		=> muzzle.position;
}
