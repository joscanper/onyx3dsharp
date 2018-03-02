
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
	public class LineRenderer : MeshRenderer
	{
		public void GenerateLine(Vector3 from, Vector3 to, Vector3 col)
		{
			if (Mesh == null)
				Mesh = new Mesh();
			else
				Mesh.Clear();

			Mesh.Vertices.Add(new Vertex(from, col));
			Mesh.Vertices.Add(new Vertex(to, col));

			Mesh.GenerateVAO();
		}

		public override void Render()
		{
			SetUpMaterial();
			SetUpMVP(Material.Shader.Program);

			GL.BindVertexArray(Mesh.VertexArrayObject);
			GL.Disable(EnableCap.DepthTest);
			GL.DrawArrays(PrimitiveType.Lines, 0, Mesh.Vertices.Count);
			GL.Enable(EnableCap.DepthTest);
			GL.BindVertexArray(0);

			GL.UseProgram(0);
		}
	}
}
