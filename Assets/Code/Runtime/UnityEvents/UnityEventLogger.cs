using UnityEngine;

[CreateAssetMenu(fileName = nameof(UnityEventLogger), menuName = nameof(UnityEventLogger))]
public class UnityEventLogger : ScriptableObject
{
	[SerializeField] private bool enableEventLogs = false;
	[SerializeField] private LogType logType = LogType.Log;

	private void Log(string eventName, Component invoker, string message = null)
	{
		if (!enableEventLogs)
			return;

		string text = $"{invoker.name}.{eventName}";
		if (!string.IsNullOrEmpty(message))
			text += $":   {message}";

		Debug.unityLogger.Log(logType, text);
	}
	private string WrapName(Object obj)
		=> obj != null ? obj.name : "null";

	public void Log(OnAddGun.Data data)
		=> Log(nameof(OnAddGun), data.Inventory, WrapName(data.Gun));
	public void Log(OnRemoveGun.Data data)
		=> Log(nameof(OnRemoveGun), data.Inventory, WrapName(data.Gun));
	public void Log(OnSwitchGun.Data data)
		=> Log(nameof(OnSwitchGun), data.Inventory, $"{WrapName(data.From)}   ->   {WrapName(data.To)}");

	public void Log(OnShoot.Data data)
		=> Log(nameof(OnShoot), data.Gun);

	public void Log(OnSpawnBullet.Data data)
		=> Log(nameof(OnSpawnBullet), data.Bullet);
	public void Log(OnHit.Data data)
		=> Log(nameof(OnHit), data.Bullet, $"{WrapName(data.Collider)}");
	public void Log(OnDespawnBullet.Data data)
		=> Log(nameof(OnDespawnBullet), data.Bullet);

	public void Log(OnGetShot.Data data)
		=> Log(nameof(OnGetShot), data.Destructible, $"{WrapName(data.Bullet)}");
	public void Log(OnChangeHealth.Data data)
		=> Log(nameof(OnChangeHealth), data.Destructible, $"{data.From:F1}   ->   {data.To:F1}");
}
