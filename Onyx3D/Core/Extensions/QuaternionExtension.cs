using System;
using OpenTK;

public static class QuaternionExtension
{

	public static Vector3 ToEulerAngles(this Quaternion q)
	{
		
		// Store the Euler angles in radians
		Vector3 eulers = new Vector3();
		eulers.X = (float)Math.Atan2(-2 * (q.Y * q.Z - q.W * q.X), q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z);
		eulers.Y = (float)Math.Asin(2 * (q.X * q.Z + q.W * q.Y));
		eulers.Z = (float)Math.Atan2(-2 * (q.X * q.Y - q.W * q.Z), q.W * q.W + q.X * q.X - q.Y * q.Y - q.Z * q.Z);
		return eulers;
	}

	public static Matrix4 ToMatrix4(this Quaternion q)
	{
		Matrix4 m = new Matrix4(
		1 - 2 * q.Y * q.Y - 2 * q.Z * q.Z,
		2 * q.X * q.Y - 2 * q.Z * q.W,
		2 * q.X * q.Z + 2 * q.Y * q.W,
		0,

		2 * q.X * q.Y + 2 * q.Z * q.W,
		1 - 2 * q.X*q.X - 2 * q.Z*q.Z,
		2 * q.Y * q.Z - 2 * q.X * q.W,
		0,

		2 * q.X * q.Z - 2 * q.Y * q.W,
		2 * q.Y * q.Z + 2 * q.X * q.W,
		1 - 2 * q.X*q.X - 2 * q.Y*q.Y,
		0,
		
		0,0,0,1);

		return m;
	}

	public static Vector3 ToDegEulerAngles(this Quaternion q)
	{
		return q.ToEulerAngles() * 180.0f / (float)Math.PI;
	}

	public static Quaternion FromDegEulerAngles(this Quaternion q, Vector3 v)
	{ 
		return Quaternion.FromEulerAngles(v * (float)Math.PI / 180.0f);
	}
}

