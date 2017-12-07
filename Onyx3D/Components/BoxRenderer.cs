
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{


	public class BoxRenderer : MeshRenderer
	{

		public BoxRenderer()
		{
			Mesh = new Mesh();

		
			Vector3 frontTL = new Vector3(-0.5f, -0.5f, 0.5f);
			Vector3 frontBL = new Vector3(-0.5f, 0.5f, 0.5f);
			Vector3 frontTR = new Vector3(0.5f, -0.5f, 0.5f);
			Vector3 frontBR = new Vector3(0.5f, 0.5f, 0.5f);

			Vector3 backTL = new Vector3(-0.5f, -0.5f, -0.5f);
			Vector3 backBL = new Vector3(-0.5f, 0.5f, -0.5f);
			Vector3 backTR = new Vector3(0.5f, -0.5f, -0.5f);
			Vector3 backBR = new Vector3(0.5f, 0.5f, -0.5f);

			// Front rect
			MeshUtils.CreateLine(ref Mesh, frontTL, frontTR, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, frontTR, frontBR, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, frontBR, frontBL, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, frontBL, frontTL, Vector3.One);

			MeshUtils.CreateLine(ref Mesh, frontTL, backTL, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, frontTR, backTR, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, frontBR, backBR, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, frontBL, backBL, Vector3.One);

			MeshUtils.CreateLine(ref Mesh, backTL, backTR, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, backTR, backBR, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, backBR, backBL, Vector3.One);
			MeshUtils.CreateLine(ref Mesh, backBL, backTL, Vector3.One);

			Mesh.GenerateVAO();
		}


		public override void Render()
		{
			SetUpMaterial();
			SetUpMVP(Material.Shader.Program);

			//GL.Disable(EnableCap.DepthTest);

			GL.BindVertexArray(Mesh.VertexArrayObject);
			GL.DrawArrays(PrimitiveType.Lines, 0, Mesh.Vertices.Count);
			GL.BindVertexArray(0);

			//GL.Enable(EnableCap.DepthTest);

			GL.UseProgram(0);
		}
	}

}