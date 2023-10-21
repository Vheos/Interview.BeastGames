using UnityEngine;

public class HitParticle : MonoBehaviour
{
	// Inspector
	[Header(Headers.Dependencies)]
	[SerializeField] private new ParticleSystem particleSystem;
	[SerializeField] private ParticleSystem decal;

	// Public
	public bool IsDecalVisible
	{
		get => decal != null && decal.gameObject.activeSelf;
		set
		{
			if (decal == null)
				return;

			decal.gameObject.SetActive(value);
		}
	}
}