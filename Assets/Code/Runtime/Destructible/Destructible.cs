using DG.Tweening;
using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private DestructibleAttributes attributes;

	[Header(Headers.Events)]
	[SerializeField] private OnGetHit.Event onGetHit;
	[SerializeField] private OnChangeHealth.Event onChangeHealth;
	[SerializeField] private OnDespawnDestructible.Event onDespawn;

	public DestructibleAttributes Attributes
		=> attributes;

	private Tween damageTween;
	private float health;
	public float Health
	{
		get => health;
		set
		{
			if (value == health)
				return;

			float previous = health;
			health = value;

			onChangeHealth.Invoke(new(this, previous, health));

			HandleDamage(previous - health);
		}
	}

	private void HandleDamage(float damage)
	{
		if (damage <= 0)
			return;

		if (health <= 0f)
			Despawn();
		else
			AnimateDamage(damage);
	}
	private void AnimateDamage(float damage)
	{
		float healthPrecentLost = damage / attributes.MaxHealth;
		float duration = 0.5f + healthPrecentLost;
		float strength = 0.05f + healthPrecentLost;
		damageTween?.Kill(true);
		damageTween = transform.DOShakeScale(duration, strength);
	}

	public void GetHit(OnHit.Data hitData)
	{
		onGetHit.Invoke(new(this, hitData));
		if (hitData.Bullet.Attributes.DealDamageOnCollision || !hitData.IsCollision)
			Health -= hitData.Bullet.Gun.GetDamageDealtTo(this);
	}
	public void Despawn()
	{
		onDespawn.Invoke(new(this));
		Destroy(gameObject);
	}


	private void Awake()
	{
		health = attributes.MaxHealth;
	}
}
