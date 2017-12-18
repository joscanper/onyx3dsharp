using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	public class Scene
	{
	
		public SceneObject Root;
		public Camera ActiveCamera;

		public Scene()
		{
			Root = new SceneObject("", this);
		}
	}
}
