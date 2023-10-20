using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private TrailRenderer trailRenderer;
	[SerializeField, Range(0f, 1f)] private float fadeDuration;

	Tween fadeTween;

	public float FadeDuration
		=> fadeDuration;
	public bool IsFading
		 => fadeTween != null;

	public bool UnparentAndFade()
	{
		if (IsFading)
			return false;

		transform.parent = null;
		fadeTween = trailRenderer.DOWidthScale(FadeDuration, 0f);
		fadeTween.onComplete += Despawn;
		return true;
	}

	private void Despawn()
		=> Destroy(gameObject);
}
