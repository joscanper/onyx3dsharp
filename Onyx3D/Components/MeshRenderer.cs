using System;
using System.Xml;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{

    public class MeshRenderer : Renderer
    {
		private Mesh mMesh;
        public Mesh Mesh
		{
			set
			{
				mMesh = value;
				UpdateBounds();
			}
			get { return mMesh; }
		}

        public Material Material;

		// --------------------------------------------------------------------

		public override void Render()
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

		// --------------------------------------------------------------------

		protected void SetUpMaterial()
		{
			GL.UseProgram(Material.Shader.Program);
			Material.ApplyProperties();
		}

		// --------------------------------------------------------------------

		protected void SetUpMVP(int program)
		{
			Matrix4 M = Transform.ModelMatrix;
			Matrix4 R = Transform.GetRotationMatrix();
            Matrix4 NM = Transform.NormalMatrix;
            
            GL.UniformMatrix4(GL.GetUniformLocation(program, "M"), false, ref M);
			GL.UniformMatrix4(GL.GetUniformLocation(program, "R"), false, ref R);
            GL.UniformMatrix4(GL.GetUniformLocation(program, "NM"), false, ref NM);
        }
		
		// --------------------------------------------------------------------

		protected override void UpdateBounds()
		{
			if (Mesh == null)
			{
				Bounds = new Bounds();
				return;
			}

			if (Transform == null)
			{
				Bounds = Mesh.Bounds;
				return;
			}

			
			Bounds bounds = new Bounds();
			Vector3 initPos = Transform.LocalToWorld(Vector3.Zero);
			bounds.SetMinMax(initPos, initPos);

			if (Mesh.Vertices.Count > 0)
			{ 
				foreach (Vertex v in Mesh.Vertices)
					bounds.Encapsulate(Transform.LocalToWorld(v.Position));
			}

			bounds.Center = initPos;
			
			Bounds = bounds;
		}

		// --------------------------------------------------------------------

			
		public override bool IntersectsRay(Ray ray, out RaycastHit hit)
		{
			hit = new RaycastHit();
			for (int i = 0; i < Mesh.Indices.Length; i += 3)
			{
				Vector3 a = Mesh.Vertices[Mesh.Indices[i]].Position;
				Vector3 b = Mesh.Vertices[Mesh.Indices[i + 1]].Position;
				Vector3 c = Mesh.Vertices[Mesh.Indices[i + 2]].Position;

				a = Transform.LocalToWorld(a);
				b = Transform.LocalToWorld(b);
				c = Transform.LocalToWorld(c);

				if (Physics.RaycastTriangle(ray, a, b, c, out hit))
				{
					hit.Object = SceneObject;
					return true;
				}
			}

			return false;
		}


		// --------------------------------------------------------------------
		// ------ Serialization ------
		// --------------------------------------------------------------------

		public override void ReadComponentXmlNode(XmlReader reader)
		{
			if (reader.Name.Equals("Mesh"))
				Mesh = Onyx3DEngine.Instance.Resources.GetMesh(reader.ReadElementContentAsInt());
			else if (reader.Name.Equals("Material"))
				Material = Onyx3DEngine.Instance.Resources.GetMaterial(reader.ReadElementContentAsInt());
		}

		// --------------------------------------------------------------------

		public override void WriteComponentXml(XmlWriter writer)
		{
			writer.WriteElementString("Mesh", Mesh.LinkedProjectAsset.Guid.ToString());
			writer.WriteElementString("Material", Material.LinkedProjectAsset.Guid.ToString());
		}

	}
}
