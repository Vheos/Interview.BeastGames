using DG.Tweening;
using UnityEngine;

public static class Helpers
{
	public static float ClampAngle(this float angle, float min, float max)
	{
		float c = 360f;
		if (angle <= -c / 2f)
			angle += c;
		if (angle >= c / 2f)
			angle += -c;

		return Mathf.Clamp(angle, min, max);
	}
	public static float MapFrom01(this float @this, float c, float d)
		=> @this * (d - c) + c;
	public static float Map(this float @this, float a, float b, float c, float d) => (@this - a) * (d - c) / (b - a) + c;
	public static bool TryGetInSelfOrParents<T>(this Component @this, out T @out) where T : Component
	{
		@out = @this.GetComponentInParent<T>(true);
		return @out != null;
	}
	public static int ToLayerMask(this Layer layer)
		=> LayerMask.GetMask(layer.ToString());
	public static Ray Ray(this Transform transform)
	=> new(transform.position, transform.forward);
	public static Layer Layer(this Component component)
		=> (Layer)component.gameObject.layer;
	public static bool TryGetDestructible(this Collider collider, out Destructible destructible)
	{
		if (collider.Layer() != global::Layer.Destructible)
		{
			destructible = null;
			return false;
		}

		return collider.TryGetInSelfOrParents(out destructible);
	}


	public static Tween DOWidthScale(this TrailRenderer trailRenderer, float duration, float scale)
		=> DOTween.To(() => trailRenderer.widthMultiplier, x => trailRenderer.widthMultiplier = x, scale, duration);

	public static void SnapTo(this Transform a, Transform b)
		=> a.SetPositionAndRotation(b.position, b.rotation);

	public static Color NewA(this Color color, float a)
		=> new(color.r, color.g, color.b, a);
	public static Vector3 NewZ(this Vector3 vector, float z)
		=> new(vector.x, vector.y, z);
	public static Vector3 RetainScreenPositionAtDistance(this Vector3 point, float distance, Camera camera)
		=> point.WorldToScreen(camera).NewZ(distance).ScreenToWorld(camera);
	public static Vector3 WorldToScreen(this Vector3 worldPosition, Camera camera)
		=> camera.WorldToScreenPoint(worldPosition);
	public static Vector3 ScreenToWorld(this Vector3 screenPosition, Camera camera)
		=> camera.ScreenToWorldPoint(screenPosition);

}
