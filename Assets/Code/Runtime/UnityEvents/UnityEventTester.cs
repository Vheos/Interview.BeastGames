using UnityEngine;

public class UnityEventTester : MonoBehaviour
{
	private const string Prefix = "EVENT: ";

	private string NameOrNull(Object obj)
		=> obj != null ? obj.name : "null";

	public void NotifyOnAddRemoveGun(OnAddRemoveGun.Data data)
		=> Debug.Log($"{Prefix}{nameof(OnAddRemoveGun)}\n" +
			$"\t{nameof(data.Gun)}: {NameOrNull(data.Gun)}");

	public void NotifyOnSwitchGun(OnSwitchGun.Data data)
		=> Debug.Log($"{Prefix}{nameof(OnSwitchGun)}\n" +
			$"\t{nameof(data.From)}: {NameOrNull(data.From)}\n" +
			$"\t{nameof(data.To)}: {NameOrNull(data.To)}");

	public void NotifyOnShootGun(OnShoot.Data data)
		=> Debug.Log($"{Prefix}{nameof(OnShoot)}\n" +
			$"\t{nameof(data.Gun)}: {NameOrNull(data.Gun)}\n" +
			$"\t{nameof(data.Bullet)}: {NameOrNull(data.Bullet)}");

	public void NotifyOnHit(OnHit.Data data)
		=> Debug.Log($"{Prefix}{nameof(OnHit)}\n" +
			$"\t{nameof(data.Bullet)}: {NameOrNull(data.Bullet)}\n" +
			$"\t{nameof(data.Collider)}: {NameOrNull(data.Collider)}");

	public void NotifyOnGetShot(OnGetShot.Data data)
		=> Debug.Log($"{Prefix}{nameof(OnGetShot)}\n" +
			$"\t{nameof(data.Destructible)}: {NameOrNull(data.Destructible)}\n" +
			$"\t{nameof(data.Bullet)}: {NameOrNull(data.Bullet)}");
}
