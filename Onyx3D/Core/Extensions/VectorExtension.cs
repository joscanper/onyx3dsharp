using System;
using System.Drawing;
using OpenTK;

public static class Vector3Extension
{

	public static float SqrDistance(this Vector3 v1, Vector3 v2)
	{
		Vector3 dir = v1 - v2;
		return dir.LengthSquared;
	}
	

	public static float CalculateAngleTo(this Vector3 v1, Vector3 v2, Vector3 normal)
	{
		v1.Normalize();
		v2.Normalize();
		float angle = (float)Math.Acos(Vector3.Dot(v1, v2));
		Vector3 cross = Vector3.Cross(v1, v2);
		if (Vector3.Dot(normal, cross) < 0)
		{ // Or > 0
			angle = -angle;
		}
		return angle;
	}

	public static Color ToColor(this Vector3 v)
	{
		return Color.FromArgb((int)(v.X * 255.0f), (int)(v.Y * 255.0f), (int)(v.Z * 255.0f));
	}
}

public static class Vector4Extension
{

	public static float SqrDistance(this Vector4 v1, Vector4 v2)
	{
		Vector4 dir = v1 - v2;
		return dir.LengthSquared;
	}

	public static Color ToColor(this Vector4 v)
	{
		return Color.FromArgb((int)(v.W * 255.0f), (int)(v.X * 255.0f), (int)(v.Y * 255.0f), (int)(v.Z * 255.0f));
	}

}
