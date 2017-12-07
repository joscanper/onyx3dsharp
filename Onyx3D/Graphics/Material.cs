using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics.OpenGL;

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

	public class Material
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
	}
}
