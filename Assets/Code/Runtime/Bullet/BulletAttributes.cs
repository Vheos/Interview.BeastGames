using UnityEngine;

public class BulletAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(0f, 10f)] private float force = 5f;
	[SerializeField, Range(0f, 15f)] private float spread = 5f;
	[SerializeField, Range(0f, 10f)] private float maxTime = 3f;
	[SerializeField, Range(0f, 100)] private int maxHits = 1;
	[SerializeField] private bool dealDamageOnCollision = true;

	public float Force
		=> force * 1000f;
	public float Spread
		=> spread;
	public float MaxTime
		=> maxTime;
	public int MaxHits
		=> maxHits;
	public bool DealDamageOnCollision
		=> dealDamageOnCollision;
}