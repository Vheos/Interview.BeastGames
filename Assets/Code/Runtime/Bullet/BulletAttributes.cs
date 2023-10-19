using UnityEngine;

public class BulletAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(1f, 10f)] private float force = 5f;
	[SerializeField, Range(0f, 10)] private float lifetime = 5f;
	[SerializeField] private bool dealDamageOnHit = true;

	public float Force
		=> force * 1000f;
	public float Lifetime
		=> lifetime;
	public bool DealDamageOnHit
		=> dealDamageOnHit;
}