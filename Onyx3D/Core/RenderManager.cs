using System.Drawing;

using OpenTK.Graphics.OpenGL4;

namespace Onyx3D
{
	public class RenderManager
	{
		private SceneObject mRoot;

		public void Init()
		{
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable(EnableCap.Multisample);
			GL.Hint(HintTarget.MultisampleFilterHintNv, HintMode.Nicest);

			GL.Enable(EnableCap.LineSmooth);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.ClearColor(Color.SlateGray);
		}

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
