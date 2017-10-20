using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL4;

namespace Onyx3D
{
	public class RenderManager : Singleton<RenderManager>
	{
		public void Render()
		{
			
			GL.Viewport(0, 0, 800, 600);

			// Clear the render canvas with the current color
			GL.Clear(ClearBufferMask.ColorBufferBit);

			//if (canDraw)
			//{
				// Draw a triangle
			//	GL.DrawArrays(PrimitiveType.Triangles, 0, nVertices);
			//}

			GL.Flush();
		}
	}
}
