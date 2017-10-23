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
        
        public void Render()
        {
            int program = Material.Shader.Program;
            GL.UseProgram(program);

            Matrix4 M = Transform.GetRotationMatrix();
            GL.UniformMatrix4(GL.GetUniformLocation(program, "M"), false, ref M);

            GL.BindVertexArray(Mesh.VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, Mesh.Vertices.Count);
            GL.BindVertexArray(0);
        }
    }
}
