using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
    public class MeshRenderer : Component
    {
        public Mesh Mesh;
        public Material Material;
        
        public virtual void Render(Camera cam)
        {
			SetUpMaterial();
			SetUpMVP(Material.Shader.Program, cam);
           
			GL.BindVertexArray(Mesh.VertexArrayObject);
			if (Mesh.Indices != null)
				GL.DrawElements(PrimitiveType.Triangles, Mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
			else
				GL.DrawArrays(PrimitiveType.Triangles, 0, Mesh.Vertices.Count);
            GL.BindVertexArray(0);

			GL.UseProgram(0);
		}

		protected void SetUpMaterial()
		{
			GL.UseProgram(Material.Shader.Program);
			Material.ApplyProperties();
		}

		protected void SetUpMVP(int program, Camera cam)
		{

			Matrix4 M = Transform.GetModelMatrix();
			Matrix4 V = cam.ViewMatrix;
			Matrix4 P = cam.ProjectionMatrix;
			//Matrix4 MVP = P * V * M;


			GL.UniformMatrix4(GL.GetUniformLocation(program, "V"), false, ref V);
			GL.UniformMatrix4(GL.GetUniformLocation(program, "P"), false, ref P);
			GL.UniformMatrix4(GL.GetUniformLocation(program, "M"), false, ref M);
			//GL.UniformMatrix4(GL.GetUniformLocation(program, "MVP"), false, ref MVP);

		}
	}
}
