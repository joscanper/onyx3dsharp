

using OpenTK;

namespace Onyx3D
{

	public class MeshUtils
	{
		public static void CreateLine(ref Mesh mesh, Vector3 point1, Vector3 point2, Vector3 color)
		{
			mesh.Vertices.Add(new Vertex(point1, color));
			mesh.Vertices.Add(new Vertex(point2, color));
		}

		public static void CreateArrow(ref Mesh mesh, Vector3 point, Vector3 up, Vector3 dir, float size1, float size2, Vector3 color)
		{
			dir.Normalize();
			up.Normalize();
			Vector3 right = Vector3.Cross(up, dir);

			//CreateLine(ref mesh, point, dir * size2, color);
			CreateLine(ref mesh, point + up * size1, point + dir * size2, color);
			CreateLine(ref mesh, point + up * size1, point + right * size1, color);
			CreateLine(ref mesh, point + up * size1, point + -right * size1, color);
			CreateLine(ref mesh, point + -up * size1, point + dir * size2, color);
			CreateLine(ref mesh, point + -up * size1, point + right * size1, color);
			CreateLine(ref mesh, point + -up * size1, point + -right * size1, color);
			CreateLine(ref mesh, point + right * size1, point + dir * size2, color);
			CreateLine(ref mesh, point + -right * size1, point + dir * size2, color);
		}

	}
}
