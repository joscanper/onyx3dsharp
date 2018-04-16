using System;
using OpenTK;

public static class Vector3Extension
{

	public static float SqrDistance(this Vector3 v1, Vector3 v2)
	{
		Vector3 dir = v1 - v2;
		return dir.LengthSquared;
	}
	
}

