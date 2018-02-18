
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{


	public class BoxRenderer : MeshRenderer
	{

		public BoxRenderer()
		{
			Mesh = new Mesh();
			
			//TODO - Generate default box

			
		}

		public void GenerateDefaultBox()
		{
			Bounds defaultBox = new Bounds();
			defaultBox.Min.X = -0.5f;
			defaultBox.Max.X = 0.5f;
			defaultBox.Min.Y = -0.5f;
			defaultBox.Max.Y = 0.5f;
			defaultBox.Min.Z = -0.5f;
			defaultBox.Max.Z = 0.5f;
			GenerateBox(defaultBox);
		}

		public void GenerateBox(Bounds box)
		{
			Mesh = new Mesh();

            MeshUtils.CreateLine(ref Mesh, box.Min, box.Min + Vector3.UnitX, Vector3.UnitX);
            MeshUtils.CreateLine(ref Mesh, box.Min, box.Min + Vector3.UnitY, Vector3.UnitX);
            MeshUtils.CreateLine(ref Mesh, box.Min, box.Min + Vector3.UnitZ, Vector3.UnitX);

            MeshUtils.CreateLine(ref Mesh, box.Max, box.Max - Vector3.UnitX, Vector3.UnitY);
            MeshUtils.CreateLine(ref Mesh, box.Max, box.Max - Vector3.UnitY, Vector3.UnitY);
            MeshUtils.CreateLine(ref Mesh, box.Max, box.Max - Vector3.UnitZ, Vector3.UnitY);

            
			Vector3 frontTL = new Vector3(box.Min.X, box.Min.Y, box.Max.Z);
			Vector3 frontBL = new Vector3(box.Min.X, box.Max.Y, box.Max.Z);
			Vector3 frontTR = new Vector3(box.Max.X, box.Min.Y, box.Max.Z);
			Vector3 frontBR = new Vector3(box.Max.X, box.Max.Y, box.Max.Z);

			Vector3 backTL = new Vector3(box.Min.X, box.Min.Y, box.Min.Z);
			Vector3 backBL = new Vector3(box.Min.X, box.Max.Y, box.Min.Z);
			Vector3 backTR = new Vector3(box.Max.X, box.Min.Y, box.Min.Z);
			Vector3 backBR = new Vector3(box.Max.X, box.Max.Y, box.Min.Z);

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
            Matrix4 M = Transform.GetTranslationMatrix();
            Matrix4 R = Matrix4.Identity;
            GL.UniformMatrix4(GL.GetUniformLocation(Material.Shader.Program, "M"), false, ref M);
            GL.UniformMatrix4(GL.GetUniformLocation(Material.Shader.Program, "R"), false, ref R);

            //GL.Disable(EnableCap.DepthTest);

            GL.BindVertexArray(Mesh.VertexArrayObject);
			GL.DrawArrays(PrimitiveType.Lines, 0, Mesh.Vertices.Count);
			GL.BindVertexArray(0);

			//GL.Enable(EnableCap.DepthTest);

			GL.UseProgram(0);
		}
	}

}