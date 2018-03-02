
using OpenTK;

namespace Onyx3D
{
    public struct Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;

		public Vector3 ClosestPointTo(Vector3 p)
		{
			return Origin + Vector3.Dot(Direction, p) * Direction;
		}
	}
}
