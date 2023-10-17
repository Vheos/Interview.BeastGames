using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IShooter
{

	public bool TryShoot()
	{
		return true;
	}
}
