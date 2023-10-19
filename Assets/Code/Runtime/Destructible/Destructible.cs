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

	private Tween takeDamageTween;
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

			TryAnimateTakeDamage(previous - health);

			if (health <= 0f)
				Despawn();
		}
	}

	private void TryAnimateTakeDamage(float damage)
	{
		if (damage <= 0f)
			return;

		float healthPrecentLost = damage / attributes.MaxHealth;
		float duration = healthPrecentLost.MapFrom01(0.5f, 1.5f);
		float strength = healthPrecentLost.MapFrom01(0f, 1f);
		takeDamageTween?.Kill(true);
		takeDamageTween = transform.DOShakeScale(duration, strength);
	}

	private void Despawn()
	{
		onDespawn.Invoke(new(this));
		Destroy(gameObject);
	}

	public void GetHitOnCollision(Bullet bullet, Collision collision)
		=> onGetHit.Invoke(new(this, bullet, collision));

	public void GetHitBy(Bullet bullet)
	{
		GetHitOnCollision(bullet, null);
		TakeDamageFrom(bullet.Gun);
	}

	public void TakeDamageFrom(Gun gun)
		=> Health -= gun.GetDamageDealtTo(this);

	private void Awake()
	{
		health = attributes.MaxHealth;
	}
}
