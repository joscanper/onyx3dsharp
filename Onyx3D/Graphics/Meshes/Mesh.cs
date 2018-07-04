using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace Onyx3D
{

    public struct Vertex
    {

        public Vector3 Position;
        public Vector3 Color;
        public Vector3 Normal;
        public Vector2 TexCoord;
        public Vector3 Tangent;
		public Vector3 Bitangent;

		public Vertex(Vector3 pos) : this(pos, Vector3.One, Vector3.Zero, Vector2.Zero) {}

		public Vertex(Vector3 pos, Vector3 col) : this(pos, col, Vector3.Zero, Vector2.Zero) {}

		public Vertex(Vector3 pos, Vector3 col, Vector3 normal) : this(pos, col, normal, Vector2.Zero) { }

		public Vertex(Vector3 pos, Vector3 col, Vector3 normal, Vector2 texcoord)
		{
			Position = pos;
			Color = col;
			Normal = normal;
			TexCoord = texcoord;
			Tangent = normal;
			Bitangent = normal;
        }
	}

	[Serializable]
	public class Mesh : GameAsset, IXmlSerializable
	{
		
        private int mVertexArrayObject;
		public int VertexArrayObject
		{
			get { return mVertexArrayObject; }
		}
		//private int mElemen

        public List<Vertex> Vertices = new List<Vertex>();
        public int[] Indices;
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
            //Tangent
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position) + Marshal.SizeOf(vertices[0].Color) + Marshal.SizeOf(vertices[0].Normal) + Marshal.SizeOf(vertices[0].TexCoord));
            GL.EnableVertexAttribArray(4);
			//Bitangent
			GL.VertexAttribPointer(5, 3, VertexAttribPointerType.Float, false, sizeOfVertex, Marshal.SizeOf(vertices[0].Position) + Marshal.SizeOf(vertices[0].Color) + Marshal.SizeOf(vertices[0].Normal) + Marshal.SizeOf(vertices[0].TexCoord) + Marshal.SizeOf(vertices[0].Tangent));
			GL.EnableVertexAttribArray(5);

			int ebo = 0;
			if (Indices != null)
			{
				int[] indices = Indices.ToArray();
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

		// TODO - Delete Vertex Array

		public void AddFace(Vertex v1, Vertex v2, Vertex v3, Vertex v4)
		{
            Vertices.Add(v1);
            Vertices.Add(v2);
            Vertices.Add(v3);
            Vertices.Add(v2);
            Vertices.Add(v4);
            Vertices.Add(v3);
        }

        public void AddFace(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vector3 n)
		{
			if (n == Vector3.Zero)
				n = Vector3.Cross((v1.Position - v2.Position), (v2.Position - v3.Position));
			
			v1.Normal = n;
			v2.Normal = n;
			v3.Normal = n;
			v4.Normal = n;

            AddFace(v1, v2, v3, v4);
		}


		private Bounds GenerateAABB()
		{
            Bounds bbox = new Bounds();

			foreach (Vertex v in Vertices)
				bbox.Encapsulate(v.Position);
			
			return bbox;
		}


		// ------ Serialization ------


		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			Vertices.Clear();
			List<int> indices = new List<int>();
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if (reader.Name == "Vertex")
						{
							Vertex v = new Vertex();
							v.Position = XmlUtils.StringToVector3(reader.GetAttribute("position"));
							v.Normal = XmlUtils.StringToVector3(reader.GetAttribute("normal"));
							v.Tangent = XmlUtils.StringToVector3(reader.GetAttribute("tangent"));
							v.Color = XmlUtils.StringToVector3(reader.GetAttribute("color"));
							v.TexCoord = XmlUtils.StringToVector2(reader.GetAttribute("texcoord"));
							Vertices.Add(v);
						}
						if (reader.Name == "Index")
						{
							int index = Convert.ToInt32(reader.GetAttribute("i"));
							indices.Add(index);
						}
						break;
				}
			}
			Indices = indices.ToArray();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Mesh");

			writer.WriteStartElement("Vertices");

			foreach (Vertex v in Vertices)
			{
				writer.WriteStartElement("Vertex");

				writer.WriteAttributeString("position", XmlUtils.Vector3ToString(v.Position));
				writer.WriteAttributeString("normal", XmlUtils.Vector3ToString(v.Normal));
				writer.WriteAttributeString("texcoord", XmlUtils.Vector2ToString(v.TexCoord));
				writer.WriteAttributeString("color", XmlUtils.Vector3ToString(v.Color));
				writer.WriteAttributeString("tangent", XmlUtils.Vector3ToString(v.Tangent));

				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			writer.WriteStartElement("Indices");

			foreach (int i in Indices)
			{
				writer.WriteStartElement("Index");
				writer.WriteAttributeString("i", i.ToString());
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}

}