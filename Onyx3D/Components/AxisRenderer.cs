
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{ 
	public class AxisRenderer : MeshRenderer
	{

		public AxisRenderer()
		{

			Mesh myMesh = new Mesh();

			float smallthingsize = 0.05f;

			// X Axis
			MeshUtils.CreateLine(ref myMesh, Vector3.Zero, Vector3.UnitX, Vector3.UnitX);
			MeshUtils.CreateLine(ref myMesh, Vector3.Zero, Vector3.UnitY, Vector3.UnitY);
			MeshUtils.CreateLine(ref myMesh, Vector3.Zero, Vector3.UnitZ, Vector3.UnitZ);

			MeshUtils.CreateArrowCap(ref myMesh, Vector3.UnitX, Vector3.UnitY, Vector3.UnitX, 0.025f, 0.1f, Vector3.UnitX);
			MeshUtils.CreateArrowCap(ref myMesh, Vector3.UnitZ, Vector3.UnitY, Vector3.UnitZ, 0.025f, 0.1f, Vector3.UnitZ);
			MeshUtils.CreateArrowCap(ref myMesh, Vector3.UnitY, Vector3.UnitX, Vector3.UnitY, 0.025f, 0.1f, Vector3.UnitY);

			MeshUtils.CreateLine(ref myMesh, Vector3.UnitX * smallthingsize, Vector3.UnitY * smallthingsize, Vector3.One);
			MeshUtils.CreateLine(ref myMesh, Vector3.UnitX * smallthingsize, Vector3.UnitZ * smallthingsize, Vector3.One);
			MeshUtils.CreateLine(ref myMesh, Vector3.UnitY * smallthingsize, Vector3.UnitZ * smallthingsize, Vector3.One);

			MeshUtils.CreateLine(ref myMesh, Vector3.Zero, Vector3.UnitX * smallthingsize, Vector3.One);
			MeshUtils.CreateLine(ref myMesh, Vector3.Zero, Vector3.UnitZ * smallthingsize, Vector3.One);
			MeshUtils.CreateLine(ref myMesh, Vector3.Zero, Vector3.UnitY * smallthingsize, Vector3.One);

			Mesh = myMesh;
			Mesh.GenerateVAO();
		}

		public override void Render()
		{
			SetUpMaterial();
			SetUpMVP(Material.Shader.Program);

			GL.Disable(EnableCap.DepthTest);

			GL.BindVertexArray(Mesh.VertexArrayObject);
			GL.DrawArrays(PrimitiveType.Lines, 0, Mesh.Vertices.Count);
			GL.BindVertexArray(0);

			GL.Enable(EnableCap.DepthTest);

			GL.UseProgram(0);
		}
	}

}