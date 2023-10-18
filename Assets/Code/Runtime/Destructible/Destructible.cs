using UnityEngine;

public class Destructible : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private DestructibleAttributes attributes;
	[Header(Headers.Events)]
	[SerializeField] private OnGetShot.Event onGetShot;
	[SerializeField] private OnChangeHealth.Event onChangeHealth;

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

	public void OnGetShot(Bullet bullet, Collision collision)
	{
		float damage = bullet.Gun.Attributes.GetDamageDealtTo(attributes.ArmorType);
		Health -= damage;

		onGetShot.Invoke(new(this, bullet, collision));
	}

	private void Awake()
	{
		health = attributes.MaxHealth;
	}
}
