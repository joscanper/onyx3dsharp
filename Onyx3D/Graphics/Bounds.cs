using OpenTK;
using System;

namespace Onyx3D
{
	public struct Bounds
	{
        public Vector3 Min;
        public Vector3 Max;
		public Vector3 Center;

		public Vector3 Size
		{
			get { return new Vector3(Max.X - Min.X, Max.Y - Min.Y, Max.Z - Min.Z); }
		}

		public void SetMinMax(Vector3 min, Vector3 max)
		{
			Min = min;
			Max = max;
			Center = Max + Min / 2;
		}


		public void Encapsulate(Vector3 position)
		{
			if (position.X < Min.X) Min.X = position.X;
			if (position.Y < Min.Y) Min.Y = position.Y;
			if (position.Z < Min.Z) Min.Z = position.Z;

			if (position.X > Max.X) Max.X = position.X;
			if (position.Y > Max.Y) Max.Y = position.Y;
			if (position.Z > Max.Z) Max.Z = position.Z;

			Center = Max + Min / 2;
		}
		
        public bool IntersectsRay(Ray r)
        {
            // r.dir is unit direction vector of ray
            
            Vector3 dirfrac = new Vector3(1.0f / r.Direction.X, 1.0f / r.Direction.Y, 1.0f / r.Direction.Z);

            // lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
            // r.org is origin of ray
            double t1 = (Min.X - r.Origin.X) * dirfrac.X;
            double t2 = (Max.X - r.Origin.X) * dirfrac.X;

            double t3 = (Min.Y - r.Origin.Y) * dirfrac.Y;
            double t4 = (Max.Y - r.Origin.Y) * dirfrac.Y;

            double t5 = (Min.Z - r.Origin.Z) * dirfrac.Z;
            double t6 = (Max.Z - r.Origin.Z) * dirfrac.Z;

            double tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            double tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            double t;
            /*
            // if tmax < 0, ray (line) is intersecting AABB, but the whole AABB is behind us
            if (tmax < 0)
            {
                t = tmax;
                return false;
            }*/

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                t = tmax;
                return false;
            }

            t = tmin;
            return true;
        }
	}
}
