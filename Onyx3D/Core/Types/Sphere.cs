using System;
using OpenTK;


namespace Onyx3D
{

	public struct Sphere
	{
		public Vector3 Position;
		public float Radius;

		public void Set(Vector3 pos, float radius)
		{
			Position = pos;
			Radius = radius;
		}

		public bool IntersectsRay(Ray r)
		{
			
			Vector3 dirToCenter = Position - r.Origin;
			float rayPoint = Vector3.Dot(dirToCenter, r.Direction);
			if (rayPoint < 0) return false;

			float dSqr = Vector3.Dot(dirToCenter, dirToCenter) - (rayPoint * rayPoint);
			return dSqr < (Radius * Radius);
		}
	}
}
