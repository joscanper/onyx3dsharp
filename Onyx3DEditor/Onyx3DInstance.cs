using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Onyx3D;

namespace Onyx3DEditor
{
	class Onyx3DInstance
	{
		public RenderManager RenderManager;
		public ResourcesManager Resources;

		public void Init()
		{
			RenderManager = new RenderManager();
			RenderManager.Init();

			Resources = new ResourcesManager();
			Resources.Init();
		}
	}
}
