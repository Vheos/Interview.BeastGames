using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CommonBulletBehaviour), menuName = nameof(CommonBulletBehaviour))]
public class CommonBulletBehaviour : ScriptableObject
{
	public void RandomizeRotation(OnSpawnBullet.Data data)
		=> data.Bullet.transform.rotation = Random.rotation;

	public void Jiggle(OnChangeHealth.Data data)
	{
		data.Destructible.transform.DOKill(true);
		data.Destructible.transform.DOShakeScale(0.5f, 0.25f);
	}

}
