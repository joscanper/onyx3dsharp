using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	public class Axis : SceneObject
	{
		public Axis(string id, ResourcesManager content, Scene scene =null) : base(id, scene)
		{
			AxisRenderer axisRenderer = this.AddComponent<AxisRenderer>();
			axisRenderer.Material = Onyx3DEngine.Instance.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);

		}
	}
}
