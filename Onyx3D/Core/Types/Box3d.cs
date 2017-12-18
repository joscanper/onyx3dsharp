using OpenTK;

namespace Onyx3D
{
	public struct Box3d
	{
		public float MinX;
		public float MaxX;
		public float MinY;
		public float MaxY;
		public float MinZ;
		public float MaxZ;
		public Vector3 Center;

		public Vector3 Size
		{
			get { return new Vector3(MaxX - MinX, MaxY - MinY, MaxZ - MinZ); }
		}
	}
}
