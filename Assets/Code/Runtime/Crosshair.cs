using UnityEngine;

public class Crosshair : MonoBehaviour
{
	[Header(Headers.Dependencies)]
	[SerializeField] private SpriteRenderer spriteRenderer;
	[Header(Headers.Values)]
	[SerializeField, Range(1f, 10f)] private float speed = 10f;
	[SerializeField, Range(1f, 10f)] private float distance = 5f;
	[SerializeField] private Color noTargetColor = Color.white.NewA(0.25f);
	[SerializeField] private Color zeroDamageColor = Color.red.NewA(0.5f);
	[SerializeField] private Color fullDamageColor = Color.green;
	[SerializeField] private LayerMask raycastMask;


	private void Reset()
	{
		raycastMask = LayerMask.GetMask(nameof(Layer.Default), nameof(Layer.Destructible));
	}
	private const float maxDistance = 100f;

	public void UpdatePosition(Gun gun, Camera camera, bool instantly = false)
	{
		Ray ray = new(gun.GetNearMuzzlePoint(camera), transform.forward);
		bool hitSomething = Physics.Raycast(ray, out var raycastHit, maxDistance, raycastMask);

		Vector3 point = hitSomething
			? raycastHit.point
			: ray.GetPoint(maxDistance);

		Color color = hitSomething && raycastHit.collider.TryGetDestructible(out var destructible)
			? Color.Lerp(zeroDamageColor, fullDamageColor, gun.GetDamageModifierFor(destructible))
			: noTargetColor;

		Vector3 worldPoint = point.RetainScreenPositionAtDistance(distance, camera);

		float lerpSpeed = instantly ? 1f : speed * Time.deltaTime;
		transform.position = Vector3.Lerp(transform.position, worldPoint, lerpSpeed);
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, color, lerpSpeed);
	}
}
