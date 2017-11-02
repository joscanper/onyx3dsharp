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
		Sampler2D,
		Cubemap
	};

	public class MaterialProperty {
		public MaterialPropertyType Type;
		public object Data;
		public int DataIndex;

		public MaterialProperty(MaterialPropertyType type, object data, int index = -1)
		{
			Type = type;
			Data = data;
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
						GL.ActiveTexture(TextureUnit.Texture0 + mp.Value.DataIndex);
						GL.BindTexture(TextureTarget.Texture2D, (int)mp.Value.Data);
						GL.Uniform1(GL.GetUniformLocation(Shader.Program, mp.Key), mp.Value.DataIndex);
						break;
				}
			}
		}
	}
}
