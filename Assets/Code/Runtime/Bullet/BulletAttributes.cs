using UnityEngine;

public class BulletAttributes : MonoBehaviour
{
	// Inspector
	[Header(Headers.Prefabs)]
	[SerializeField] private HitParticle hitParticlePrefab;
	[Header(Headers.Values)]
	[SerializeField, Range(0f, 10f)] private float force = 5f;
	[SerializeField, Range(0f, 10f)] private float maxTime = 3f;
	[SerializeField, Range(0f, 100)] private int maxHits = 1;
	[SerializeField] private bool dealDamageOnCollision = true;
	[SerializeField] private bool spawnHitDecal = true;

	public float Force
		=> force * 1000f;
	public float MaxTime
		=> maxTime;
	public int MaxHits
		=> maxHits;
	public bool DealDamageOnCollision
		=> dealDamageOnCollision;
	public HitParticle HitParticlePrefab
		=> hitParticlePrefab;
	public bool SpawnHitDecal
		=> spawnHitDecal;
}