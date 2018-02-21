using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{

    public class MeshRenderer : Component
    {
        public Mesh Mesh;
        public Material Material;
        public Bounds Bounds {
            get
            {
                Bounds b = new Bounds();
				b.SetMinMax(Transform.Position, Transform.Position);
				foreach (Vertex v in Mesh.Vertices)
					b.Encapsulate((new Vector4(v.Position.X, v.Position.Y, v.Position.Z, 1) * Transform.ModelMatrix).Xyz);
                
                return b;
            }
        }

		public virtual void Render()
        {
            SetUpMaterial();
			SetUpMVP(Material.Shader.Program);
           
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

		protected void SetUpMVP(int program)
		{

			Matrix4 M = Transform.ModelMatrix;
			Matrix4 R = Transform.GetRotationMatrix();
			//Matrix4 V = cam.ViewMatrix;
			//Matrix4 P = cam.ProjectionMatrix;
			//Matrix4 MVP = P * V * M;


			//GL.UniformMatrix4(GL.GetUniformLocation(program, "V"), false, ref V);
			//GL.UniformMatrix4(GL.GetUniformLocation(program, "P"), false, ref P);
			GL.UniformMatrix4(GL.GetUniformLocation(program, "M"), false, ref M);
			GL.UniformMatrix4(GL.GetUniformLocation(program, "R"), false, ref R);
			//GL.UniformMatrix4(GL.GetUniformLocation(program, "MVP"), false, ref MVP);

		}

		// ------ Serialization ------

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			if (reader.Name.Equals("Mesh"))
				Mesh = Onyx3DEngine.Instance.Resources.GetMesh(reader.ReadElementContentAsInt());
			else if (reader.Name.Equals("Material"))
				Material = Onyx3DEngine.Instance.Resources.GetMaterial(reader.ReadElementContentAsInt());
		}

		public override void WriteComponentXml(XmlWriter writer)
		{
			writer.WriteElementString("Mesh", Mesh.LinkedProjectAsset.Guid.ToString());
			writer.WriteElementString("Material", Material.LinkedProjectAsset.Guid.ToString());
		}

	}
}
