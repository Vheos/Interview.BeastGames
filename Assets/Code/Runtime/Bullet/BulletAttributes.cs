using UnityEngine;

public class BulletAttributes : MonoBehaviour
{
	// Inspector
	[SerializeField, Range(1f, 100f)] private float speed = 50f;
	[SerializeField, Range(0f, 10)] private float lifetime = 5f;

	public float Speed
		=> speed;
	public float Lifetime
		=> lifetime;
}