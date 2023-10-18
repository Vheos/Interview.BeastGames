using UnityEngine;

public class UnityEventTester : MonoBehaviour
{
	private const string Prefix = "EVENT: ";

	private string NameOrNull(Object obj)
		=> obj != null ? obj.name : "null";

	public void NotifyOnSwitchGun(SwitchGun.Context context)
		=> Debug.Log($"{Prefix}{nameof(SwitchGun)}\n" +
			$"\t{nameof(context.From)}: {NameOrNull(context.From)}\n" +
			$"\t{nameof(context.To)}: {NameOrNull(context.To)}");

	public void NotifyOnShootGun(ShootGun.Context context)
		=> Debug.Log($"{Prefix}{nameof(ShootGun)}\n" +
			$"\t{nameof(context.Gun)}: {NameOrNull(context.Gun)}\n");

	public void NotifyOnAddGun(AddGun.Context context)
	=> Debug.Log($"{Prefix}{nameof(AddGun)}\n" +
		$"\t{nameof(context.Gun)}: {NameOrNull(context.Gun)}\n");

	public void NotifyOnRemoveGun(RemoveGun.Context context)
	=> Debug.Log($"{Prefix}{nameof(RemoveGun)}\n" +
		$"\t{nameof(context.Gun)}: {NameOrNull(context.Gun)}\n");
}
