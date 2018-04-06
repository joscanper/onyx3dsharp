using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace Onyx3D
{
	public enum MaterialPropertyType
	{
		Int,
		Float,
		Vector2,
		Vector3,
		Vector4,
		Color,
		Sampler2D,
		Cubemap
	};

	public class MaterialProperty {
		public MaterialPropertyType Type;
		public object Data;

		public MaterialProperty(MaterialPropertyType type, object data)
		{
			Type = type;
			Data = data;
		}
	}

	public class TextureMaterialProperty : MaterialProperty
	{
		public Texture Texture;
		public int DataIndex;

		public TextureMaterialProperty(MaterialPropertyType type, Texture t, int index) : base(type, t.Id)
		{
			Texture = t;
			DataIndex = index;
		}
	}

	public class Material : GameAsset, IXmlSerializable
	{
		public Shader Shader;
		public Dictionary<string, MaterialProperty> Properties = new Dictionary<string, MaterialProperty>();

		public void ApplyProperties()
		{
			foreach (KeyValuePair<string, MaterialProperty> mp in Properties)
			{
				switch (mp.Value.Type)
				{
					case MaterialPropertyType.Sampler2D:
						TextureMaterialProperty tmp = (TextureMaterialProperty)mp.Value;
						GL.ActiveTexture(TextureUnit.Texture0 + tmp.DataIndex);
						GL.BindTexture(TextureTarget.Texture2D, (int)mp.Value.Data);
						GL.Uniform1(GL.GetUniformLocation(Shader.Program, mp.Key), tmp.DataIndex);
						break;
					case MaterialPropertyType.Float:
						int loc = GL.GetUniformLocation(Shader.Program, mp.Key);
						GL.Uniform1(loc, (float)mp.Value.Data);
						break;
					case MaterialPropertyType.Vector2:
						Vector2 v2 = (Vector2)mp.Value.Data;
						GL.Uniform2(GL.GetUniformLocation(Shader.Program, mp.Key), v2);
						break;
					case MaterialPropertyType.Vector3:
						Vector3 v3 = (Vector3)mp.Value.Data;
						GL.Uniform3(GL.GetUniformLocation(Shader.Program, mp.Key), v3);
						break;
					case MaterialPropertyType.Vector4:
					case MaterialPropertyType.Color:
						Vector4 v4 = (Vector4)mp.Value.Data;
						GL.Uniform4(GL.GetUniformLocation(Shader.Program, mp.Key), v4);
						break;

				}
			}
		}

		// ------ Serialization ------

		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if (reader.Name == "Shader")
						{
							Shader = Onyx3DEngine.Instance.Resources.GetShader(reader.ReadElementContentAsInt());
						}
						if (reader.Name == "Property")
						{
							string id = reader.GetAttribute("id");
							string type = reader.GetAttribute("type");
							string value = reader.GetAttribute("value");
							if (type == "float")
								Properties.Add(id, new MaterialProperty(MaterialPropertyType.Float, (float)Convert.ToDecimal(value)));
							else if (type == "float2")
								Properties.Add(id, new MaterialProperty(MaterialPropertyType.Vector2, XmlUtils.StringToVector2(value)));
							else if (type == "float3")
								Properties.Add(id, new MaterialProperty(MaterialPropertyType.Vector3, XmlUtils.StringToVector3(value)));
							else if (type == "float4")
								Properties.Add(id, new MaterialProperty(MaterialPropertyType.Vector4, XmlUtils.StringToVector4(value)));
							else if (type == "color")
								Properties.Add(id, new MaterialProperty(MaterialPropertyType.Color, XmlUtils.StringToVector4(value)));
							else if (type == "sampler2d")
								Properties.Add(id, new TextureMaterialProperty(MaterialPropertyType.Sampler2D, Onyx3DEngine.Instance.Resources.GetTexture(BuiltInTexture.Checker), 0));
							//else if (type == "samplerCube")
								//Properties.Add(id, new TextureMaterialProperty(MaterialPropertyType.Sampler2D, Onyx3DEngine.Instance.Resources.GetTexture(BuiltInTexture.Checker), 0));
							// TODO - More things
						}
						break;
				}
			}
			
			if (Shader == null)
				Shader = Onyx3DEngine.Instance.Resources.GetShader(BuiltInShader.Default);
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Material");

			
			writer.WriteElementString("Shader", Shader.LinkedProjectAsset.Guid.ToString());

			foreach (KeyValuePair<string, MaterialProperty> prop in Properties)
			{
				writer.WriteStartElement("Property");
				writer.WriteAttributeString("id", prop.Key);
				switch (prop.Value.Type)
				{
					case MaterialPropertyType.Color:
						writer.WriteAttributeString("type", "color");
						writer.WriteAttributeString("value", XmlUtils.Vector4ToString((Vector4)prop.Value.Data));
						break;
					case MaterialPropertyType.Vector4:
						writer.WriteAttributeString("type", "float4");
						writer.WriteAttributeString("value", XmlUtils.Vector4ToString((Vector4)prop.Value.Data));
						break;
					case MaterialPropertyType.Vector3:
						writer.WriteAttributeString("type", "float3");
						writer.WriteAttributeString("value", XmlUtils.Vector3ToString((Vector3)prop.Value.Data));
						break;
					case MaterialPropertyType.Vector2:
						writer.WriteAttributeString("type", "float2");
						writer.WriteAttributeString("value", XmlUtils.Vector2ToString((Vector2)prop.Value.Data));
						break;
					case MaterialPropertyType.Float:
						writer.WriteAttributeString("type", "float");
						writer.WriteAttributeString("value", ((float)prop.Value.Data).ToString());
						break;
					case MaterialPropertyType.Sampler2D:
						writer.WriteAttributeString("type", "sampler2d");
						writer.WriteAttributeString("value", ((int)prop.Value.Data).ToString());
						break;
					// TODO - More things
				}


				writer.WriteEndElement();
			}
			


			writer.WriteEndElement();
		}
	}
}
