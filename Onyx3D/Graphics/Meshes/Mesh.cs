using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Onyx3D
{

    public struct Vertex
    {

        public Vector3 Position;
        public Vector3 Color;
        public Vector3 Normal;
        //public Vector3 TexCoord;

        public Vertex(Vector3 pos) : this(pos, Vector3.One, Vector3.Zero){}

		public Vertex(Vector3 pos, Vector3 col) : this(pos, col, Vector3.Zero){}

		public Vertex(Vector3 pos, Vector3 col, Vector3 normal)
		{
			Position = pos;
			Color = col;
			Normal = normal;
		}
	}

    public class Mesh
    {
        public int VertexArrayObject;

        public List<Vertex> Vertices = new List<Vertex>();
        public List<int> Indices = new List<int>();
        
        public void GenerateVAO()
        {
            Vertex[] vertices = Vertices.ToArray();

            GL.GenVertexArrays(1, out VertexArrayObject);

            int vbo;
            GL.GenBuffers(1, out vbo);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            int sizeOfVertex = Marshal.SizeOf(vertices[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeOfVertex, vertices, BufferUsageHint.StaticDraw);

			//Position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeOfVertex, 0);
            GL.EnableVertexAttribArray(0);
			//Color
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position));
            GL.EnableVertexAttribArray(1);
			//Normal
			GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position) + Marshal.SizeOf(vertices[0].Color));
			GL.EnableVertexAttribArray(2);

			GL.BindVertexArray(0);
            GL.DeleteBuffer(vbo);
        }
    }

}