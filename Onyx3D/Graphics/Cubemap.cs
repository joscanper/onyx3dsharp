using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace Onyx3D
{
    public class Cubemap
    {
		private int mId;

		public int Id { get { return mId; } }

		public Cubemap()
		{
			GL.GenTextures(1, out mId);
			GL.BindTexture(TextureTarget.TextureCubeMap, mId);

			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
			GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);
		}

		public void SetTexture(int index, Texture t)
		{
			IntPtr data = Marshal.AllocHGlobal(32 * t.Width * t.Height);

			GL.GetTextureImage(t.Id, 0, PixelFormat.Rgba, PixelType.UnsignedByte, 32 * t.Width * t.Height, data);
			GL.BindTexture(TextureTarget.TextureCubeMap, mId);
			GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + index, 0, PixelInternalFormat.Rgba, t.Width, t.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);

			//TODO - Free memory here?
		}

        public Texture TextureFront { set; get; }
        public Texture TextureLeft { set; get; }
        public Texture TextureRight { set; get; }
        public Texture TextureBack { set; get; }
        public Texture TextureUp { set; get; }
        public Texture TextureDown { set; get; }

    }

    

}
