using System;

using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
	public class FrameBuffer : IDisposable
	{
		
		uint mFrameBufferId = 0;
		private uint mDepthId;

		public Texture Texture { get; private set; }

		public int Width
		{
			get { return Texture.Width; }
		}

		public int Height
		{
			get { return Texture.Height; }
		}

		public FrameBuffer(int w, int h)
		{
			GL.GenFramebuffers(1, out mFrameBufferId);
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, mFrameBufferId);

			Texture = new Texture(w, h);
			
			GL.GenRenderbuffers(1, out mDepthId);
			GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, mDepthId);
			GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, w, h);

			GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, mDepthId);
			GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, Texture.Id, 0);

			// Set the list of draw buffers.
			DrawBuffersEnum DrawBuffers = DrawBuffersEnum.ColorAttachment0;
			GL.DrawBuffers(1,ref DrawBuffers); // "1" is the size of DrawBuffe

			FramebufferErrorCode result = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
			if (result != FramebufferErrorCode.FramebufferComplete)
				Logger.Instance.Append("Framebuffer failed status : " + result.ToString());

			Unbind();
		}


		public void Bind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, mFrameBufferId);
			GL.Viewport(0, 0, Width, Height);
		}

		public void Unbind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
		}

		public void Dispose()
		{
			GL.DeleteFramebuffer(mFrameBufferId);
			GL.DeleteRenderbuffer(mDepthId);

			Texture.Dispose();
			Texture = null;
		}
	}
}
