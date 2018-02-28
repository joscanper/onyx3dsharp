
using OpenTK;
using System;

namespace Onyx3D
{
	public struct Plane
	{
		public Vector3 Distance;
		public Vector3 Normal;

		public Plane(Vector3 distance, Vector3 n)
		{
			Distance = distance;
			Normal = n;
		}

		public bool IntersectsRay(Ray r, out float hitDist)
		{
			hitDist = 0;
			float denom = Vector3.Dot(Normal, r.Direction);
			if (denom > 1e-6)
			{
				Vector3 pl = (Distance * Normal) - r.Direction;
				hitDist = Vector3.Dot(pl, Normal) / denom;
				return (hitDist >= 0);
			}

			return false;
		}
	}
}
