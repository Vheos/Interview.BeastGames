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
}
