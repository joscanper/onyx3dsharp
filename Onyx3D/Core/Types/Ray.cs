
using OpenTK;

namespace Onyx3D
{
    public struct Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;

		public Ray(Vector3 origin, Vector3 dir)
		{
			Origin = origin;
			Direction = dir;
		}

		public Vector3 ClosestPointTo(Ray b)
		{
			Vector3 c = b.Origin - Origin;
			float aDotB = Vector3.Dot(Direction, b.Direction);
			float bDotC = Vector3.Dot(b.Direction, c);
			float aDotC = Vector3.Dot(Direction, c);
			float bDotB = Vector3.Dot(b.Direction, b.Direction);
			float aDotA = Vector3.Dot(Direction, Direction);

			float up = -aDotB * bDotC + aDotC * bDotB;
			float down = aDotA * bDotB - aDotB * aDotB;

			return Origin + Direction * up / down;
		}
	}
}
