using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Onyx3D;

namespace Onyx3DEditor
{
	class Onyx3DInstance
	{
		public RenderManager Render;
		public ContentManager Content;

		public void Init()
		{
			Render = new RenderManager();
			Render.Init();

			Content = new ContentManager();
			Content.Init();
		}
	}
}
