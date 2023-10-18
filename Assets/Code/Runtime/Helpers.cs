using UnityEngine;

public static class Helpers
{
	public static float ClampAngle(float angle, float min, float max)
	{
		float c = 360f;
		if (angle <= -c / 2f)
			angle += c;
		if (angle >= c / 2f)
			angle += -c;

		return Mathf.Clamp(angle, min, max);
	}

	public static Vector3 Mul(this Vector3 a, Vector3 b)
		=> new(a.x * b.x, a.y * b.y, a.z * b.z);
	public static Vector3 Div(this Vector3 a, Vector3 b)
		=> new(a.x / b.x, a.y / b.y, a.z / b.z);
}