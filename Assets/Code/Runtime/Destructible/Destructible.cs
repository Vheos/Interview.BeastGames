using DG.Tweening;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private DestructibleAttributes attributes;
	[SerializeField] private new Collider collider;
	[Header(Headers.Events)]
	[SerializeField] private OnGetHit.Event onGetHit;
	[SerializeField] private OnChangeHealth.Event onChangeHealth;
	[SerializeField] private OnDespawnDestructible.Event onDespawn;

	// Fields
	private float health;
	private float recentDamage;
	private Tween damageTween;
	private Tween despawnTween;

	// Public
	public float Health
	{
		get => health;
		set
		{
			if (value == health || IsDespawning)
				return;

			float previous = health;
			health = value;

			onChangeHealth.Invoke(new(this, previous, health));

			HandleDamage(previous - health);
		}
	}
	public DestructibleAttributes Attributes
		=> attributes;
	public bool IsDespawning
		=> despawnTween != null;
	public void GetHit(OnHit.Data hitData)
	{
		onGetHit.Invoke(new(this, hitData));
		if (hitData.Bullet.Attributes.DealDamageOnCollision || !hitData.IsCollision)
			Health -= hitData.Bullet.Gun.GetDamageDealtTo(this);
	}
	public void Despawn()
	{
		collider.enabled = false;
		transform.DOKill();
		despawnTween = transform.DOScale(Vector3.zero, 1f);
		despawnTween.onComplete += DestroySelf;
		onDespawn.Invoke(new(this));
	}

	// Private
	private void HandleDamage(float damage)
	{
		if (damage <= 0)
			return;

		if (health <= 0f)
			Despawn();
		else
			recentDamage += damage;
	}
	private void AnimateDamage(float damage)
	{
		float healthPrecentLost = damage / attributes.MaxHealth;
		float duration = 0.5f + healthPrecentLost;
		float strength = 2 * healthPrecentLost;
		damageTween?.Kill(true);
		damageTween = transform.DOShakeScale(duration, strength);
	}
	private void DestroySelf()
		=> Destroy(gameObject);

	// Mono
	protected void Awake()
	{
		health = attributes.MaxHealth;
	}
	protected void Update()
	{
		if (recentDamage > 0f && !IsDespawning)
		{
			AnimateDamage(recentDamage);
			recentDamage = 0f;
		}
	}
}