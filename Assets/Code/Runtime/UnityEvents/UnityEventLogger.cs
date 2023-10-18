using UnityEngine;

[CreateAssetMenu(fileName = nameof(UnityEventLogger), menuName = nameof(UnityEventLogger))]
public class UnityEventLogger : ScriptableObject
{
	private string NameOrNull(Object obj)
		=> obj != null ? obj.name : "null";

	public void Log(OnAddRemoveGun.Data data)
		=> Debug.Log($"{data.Inventory.name}.{nameof(OnAddRemoveGun)}:   {NameOrNull(data.Gun)}");
	public void Log(OnSwitchGun.Data data)
		=> Debug.Log($"{data.Inventory.name}.{nameof(OnSwitchGun)}:   {NameOrNull(data.From)}   ->   {NameOrNull(data.To)}");
	public void Log(OnShoot.Data data)
		=> Debug.Log($"{data.Gun.name}.{nameof(OnShoot)}:   {NameOrNull(data.Bullet)}");
	public void Log(OnHit.Data data)
		=> Debug.Log($"{data.Bullet.name}.{nameof(OnHit)}:   {NameOrNull(data.Collider)}");
	public void Log(OnGetShot.Data data)
		=> Debug.Log($"{data.Destructible.name}.{nameof(OnGetShot)}:   {NameOrNull(data.Bullet)}");
	public void Log(OnChangeHealth.Data data)
		=> Debug.Log($"{data.Destructible.name}.{nameof(OnSwitchGun)}:   {data.From:F1}   ->   {data.To:F1}");
}
