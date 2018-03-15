
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Onyx3D
{
	public class CircleRenderer : MeshRenderer
	{
		public void GenerateCircle(float radius, Vector3 color, Vector3 up, int segments = 100)
		{
			if (Mesh == null)
				Mesh = new Mesh();
			else
				Mesh.Clear();


			Vector3 fwd = Vector3.Cross(up, Vector3.UnitY);
			if (fwd == Vector3.Zero)
				fwd = Vector3.Cross(up, Vector3.UnitX);
			Vector3 right = Vector3.Cross(fwd, up);
			Matrix3 m = new Matrix3(right, up, fwd);
			
			float angleStep = (float)Math.PI * 2.0f / segments;
			for(int i = 0; i <= segments; ++i)
			{
				float x = (float)Math.Cos(angleStep * i) * radius;
				float y = (float)Math.Sin(angleStep * i) * radius;

				
				Mesh.Vertices.Add(new Vertex(new Vector3(x, 0, y) * m, color));
			}

			Mesh.GenerateVAO();
		}

		public override void Render()
		{
			SetUpMaterial();
			SetUpMVP(Material.Shader.Program);

			GL.BindVertexArray(Mesh.VertexArrayObject);
			GL.Disable(EnableCap.DepthTest);
			GL.DrawArrays(PrimitiveType.LineStrip, 0, Mesh.Vertices.Count);
			GL.Enable(EnableCap.DepthTest);
			GL.BindVertexArray(0);

			GL.UseProgram(0);
		}
	}
}
