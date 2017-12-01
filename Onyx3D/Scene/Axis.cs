using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	public class Axis : SceneObject
	{
		public Axis(string id, ContentManager content, Scene scene =null) : base(id, scene)
		{
			AxisRenderer axisRenderer = this.AddComponent<AxisRenderer>();
			axisRenderer.Material = content.BuiltInMaterials.UnlitVertexColor;

		}
	}
}
