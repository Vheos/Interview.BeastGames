using UnityEngine;

public class Gun : MonoBehaviour, IShooter
{

	public void ApplyGripOffset()
		=> transform.localPosition = -anchors.GripLocal;

	public bool TryShoot()
	{
		return true;
	}

	private void Start()
	{
		ApplyGripOffset();
	}
}
