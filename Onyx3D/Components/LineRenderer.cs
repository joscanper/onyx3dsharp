
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
	public class LineRenderer : MeshRenderer
	{
		public void Set(Vector3 from, Vector3 to, Vector3 col)
		{
			Mesh = new Mesh();

			Mesh.Vertices.Add(new Vertex(from, col));
			Mesh.Vertices.Add(new Vertex(to, col));

			Mesh.GenerateVAO();
		}

		public override void Render()
		{
			SetUpMaterial();
			SetUpMVP(Material.Shader.Program);

			GL.BindVertexArray(Mesh.VertexArrayObject);
			GL.DrawArrays(PrimitiveType.Lines, 0, Mesh.Vertices.Count);
			GL.BindVertexArray(0);

			GL.UseProgram(0);
		}
	}
}
