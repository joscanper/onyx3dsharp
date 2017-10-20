using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	enum MaterialPropertyType
	{
		Int,
		Float,
		Vector2,
		Vector3,
		Vector4,
		Sampler2D,
		Cubemap
	};

	class MaterialProperty {
		MaterialPropertyType Type;
		object Data;
	}

	class Material
	{
		public Shader Shader;
		public Dictionary<string, MaterialProperty> Properties;
	}
}
