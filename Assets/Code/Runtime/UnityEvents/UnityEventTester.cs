using UnityEngine;

public class UnityEventTester : MonoBehaviour
{
	public void NotifyOnSwitchGun(SwitchGun.Context context)
		=> Debug.Log($"{nameof(SwitchGun)}\n" +
			$"\t{nameof(context.From)}: {(context.From != null ? context.From.name : "null")}\n" +
			$"\t{nameof(context.To)}: {(context.To != null ? context.To.name : "null")}");

	public void NotifyOnShootGun(ShootGun.Context context)
		=> Debug.Log($"{nameof(ShootGun)}\n" +
			$"\t{nameof(context.Gun)}: {(context.Gun != null ? context.Gun.name : "null")}\n");
}
