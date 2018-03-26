using System;
using OpenTK;

public static class QuaterionExtension
{

	public static Vector3 ToEulerAngles(this Quaternion q)
	{
		// Store the Euler angles in radians
		Vector3 pitchYawRoll = new Vector3();

		double sqw = q.W * q.W;
		double sqx = q.X * q.X;
		double sqy = q.Y * q.Y;
		double sqz = q.Z * q.Z;

		// If quaternion is normalised the unit is one, otherwise it is the correction factor
		double unit = sqx + sqy + sqz + sqw;
		double test = q.X * q.Y + q.Z * q.W;

		if (test > 0.4999f * unit)                              // 0.4999f OR 0.5f - EPSILON
		{
			// Singularity at north pole
			pitchYawRoll.Y = 2f * (float)Math.Atan2(q.X, q.W);  // Yaw
			pitchYawRoll.X = (float)Math.PI * 0.5f;                         // Pitch
			pitchYawRoll.Z = 0f;                                // Roll
			return pitchYawRoll;
		}
		else if (test < -0.4999f * unit)                        // -0.4999f OR -0.5f + EPSILON
		{
			// Singularity at south pole
			pitchYawRoll.Y = -2f * (float)Math.Atan2(q.X, q.W); // Yaw
			pitchYawRoll.X = - (float)Math.PI * 0.5f;           // Pitch
			pitchYawRoll.Z = 0f;                                // Roll
			return pitchYawRoll;
		}
		else
		{
			pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);       // Yaw
			pitchYawRoll.X = (float)Math.Asin(2f * test / unit);                                             // Pitch
			pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);      // Roll
		}

		return pitchYawRoll;
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

