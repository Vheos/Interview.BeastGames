using DG.Tweening;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private TrailRenderer trailRenderer;
	[SerializeField, Range(0f, 1f)] private float fadeDuration;

	// Fields
	private Tween despawnTween;

	// Public
	public float FadeDuration
		=> fadeDuration;
	public bool IsDespawning
		 => despawnTween != null;
	public bool Despawn()
	{
		if (IsDespawning)
			return false;

		transform.parent = null;
		despawnTween = trailRenderer.DOWidthScale(FadeDuration, 0f);
		despawnTween.onComplete += DestroySelf;
		return true;
	}

	// Private
	private void DestroySelf()
		=> Destroy(gameObject);
}