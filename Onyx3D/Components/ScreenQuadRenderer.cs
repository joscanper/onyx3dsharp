
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
	public class ScreenQuadRenderer : MeshRenderer
	{
		public void GenerateQuad(float w, float h)
		{
			if (Mesh == null)
				Mesh = new Mesh();
			else
				Mesh.Clear();

			Vertex v1 = new Vertex(new Vector3(0, 0, 0), Vector3.One, Vector3.UnitZ, new Vector2(0, 0));
			Vertex v2 = new Vertex(new Vector3(w, 0, 0), Vector3.One, Vector3.UnitZ, new Vector2(1, 0));
			Vertex v3 = new Vertex(new Vector3(w ,h, 0), Vector3.One, Vector3.UnitZ, new Vector2(1, 1));
			Vertex v4 = new Vertex(new Vector3(0, h, 0), Vector3.One, Vector3.UnitZ, new Vector2(0, 1));

			Mesh.Vertices.Add(v1);
			Mesh.Vertices.Add(v2);
			Mesh.Vertices.Add(v3);
			Mesh.Vertices.Add(v4);

			Mesh.Indices = new int[]
			{
				0,
				1,
				2,
				0,
				2,
				3
			};


			Mesh.GenerateVAO();
		}

	}
}

