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
        public Vector2 TexCoord;

        public Vertex(Vector3 pos) : this(pos, Vector3.One, Vector3.Zero, Vector2.Zero){}

		public Vertex(Vector3 pos, Vector3 col) : this(pos, col, Vector3.Zero, Vector2.Zero) {}

		public Vertex(Vector3 pos, Vector3 col, Vector3 normal) : this(pos, col, normal, Vector2.Zero) { }

		public Vertex(Vector3 pos, Vector3 col, Vector3 normal, Vector2 texcoord)
		{
			Position = pos;
			Color = col;
			Normal = normal;
			TexCoord = texcoord;
		}
	}

	[Serializable]
	public class Mesh : GameAsset
	{
		
        private int mVertexArrayObject;
		public int VertexArrayObject
		{
			get { return mVertexArrayObject; }
		}
		//private int mElemen

        public List<Vertex> Vertices = new List<Vertex>();
        public uint[] Indices;
        public Bounds Bounds { get; private set; }

        public void GenerateVAO()
		{
			Vertex[] vertices = Vertices.ToArray();


			GL.GenVertexArrays(1, out mVertexArrayObject);

			int vbo;
			GL.GenBuffers(1, out vbo);

			GL.BindVertexArray(mVertexArrayObject);
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

			int sizeOfVertex = Marshal.SizeOf(vertices[0]);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * sizeOfVertex), vertices, BufferUsageHint.StaticDraw);

			//Position
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeOfVertex, IntPtr.Zero);
			GL.EnableVertexAttribArray(0);
			//Color
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position));
			GL.EnableVertexAttribArray(1);
			//Normal
			GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position) + Marshal.SizeOf(vertices[0].Color));
			GL.EnableVertexAttribArray(2);
			//TexCoord
			GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position) + Marshal.SizeOf(vertices[0].Color) + Marshal.SizeOf(vertices[0].Normal));
			GL.EnableVertexAttribArray(3);


			int ebo = 0;
			if (Indices != null)
			{
				uint[] indices = Indices.ToArray();
				GL.GenBuffers(1, out ebo);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
				GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indices.Count() * sizeof(uint)), indices, BufferUsageHint.StaticDraw);
			}

			
			GL.BindVertexArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.DeleteBuffer(vbo);
			if (Indices != null)
			{
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
				GL.DeleteBuffer(ebo);
			}

            Bounds = GenerateAABB();
        }


		public void Clear()
		{
			Vertices.Clear();
			Indices = null;

			if (mVertexArrayObject>0)
				GL.DeleteVertexArray(mVertexArrayObject);
		}

		// TODO Delete Vertex Array

		protected void AddFace(Vertex v1, Vertex v2, Vertex v3, Vertex v4)
		{
			AddFace(v1, v2, v3, v4, Vector3.Zero);
		}

		protected void AddFace(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vector3 n)
		{
			if (n == Vector3.Zero)
				n = Vector3.Cross((v1.Position - v2.Position), (v2.Position - v3.Position));
			
			v1.Normal = n;
			v2.Normal = n;
			v3.Normal = n;
			v4.Normal = n;

			Vertices.Add(v1);
			Vertices.Add(v2);
			Vertices.Add(v3);
			Vertices.Add(v2);
			Vertices.Add(v4);
			Vertices.Add(v3);

		}


		private Bounds GenerateAABB()
		{
            Bounds bbox = new Bounds();

			foreach (Vertex v in Vertices)
			{
				Vector3 vp = v.Position;
				if (vp.X < bbox.Min.X)
                    bbox.Min.X = vp.X;
				else if (vp.X > bbox.Max.X)
                    bbox.Max.X = vp.X;

				if (vp.Y < bbox.Min.Y)
					bbox.Min.Y = vp.Y;
				else if (vp.Y > bbox.Max.Y)
					bbox.Max.Y = vp.Y;

				if (vp.Z < bbox.Min.Z)
					bbox.Min.Z = vp.Z;
				else if (vp.Z > bbox.Max.Z)
					bbox.Max.Z = vp.Z;
			}

			return bbox;
		}
	}

}