using System;
using UnityEngine;

public class Destructible : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private DestructibleAttributes attributes;
	[Header(Headers.Events)]
	[SerializeField] private OnGetShot.Event onGetShot;
	[SerializeField] private OnChangeHealth.Event onChangeHealth;

	public DestructibleAttributes Attributes
		=> attributes;

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

			if (health <= 0f)
				Die();
		}
	}

	private void Die()
	{
		Destroy(gameObject);
	}

	public void GetShot(Bullet bullet, Collision collision)
	{
		onGetShot.Invoke(new(this, bullet, collision));
		if (bullet.Attributes.DealDamageOnHit)
			TakeDamageFrom(bullet.Gun);
	}

	public void TakeDamageFrom(Gun gun)
		=> Health -= gun.GetDamageDealtTo(this);

	private void Awake()
	{
		health = attributes.MaxHealth;
	}


}
