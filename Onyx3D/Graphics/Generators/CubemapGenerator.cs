
using OpenTK;
using System;

namespace Onyx3D
{
    public class CubemapGenerator
    {
      

        private Camera mCamera;
        private FrameBuffer[] mFrameBuffer;
        private Vector3[] mCamRotations;

        public CubemapGenerator(int size)
        {
            mCamera = new PerspectiveCamera("", (float)Math.PI / 2.0f, 1);
            mFrameBuffer = new FrameBuffer[6];
            for(int i=0;i<6;++i)
                mFrameBuffer[i] = new FrameBuffer(size, size);

            mCamRotations = new Vector3[6];
            mCamRotations[0] = new Vector3(0, 0, 0);                                                    
            mCamRotations[1] = new Vector3(0, (float)Math.PI / 2.0f, 0);                                
            mCamRotations[2] = new Vector3(0, (float)Math.PI, 0);                                       
            mCamRotations[3] = new Vector3(0, -(float)Math.PI / 2.0f, 0);                               
            mCamRotations[4] = new Vector3(0, 0, (float)Math.PI / 2.0f);								
            mCamRotations[5] = new Vector3(0,0, -(float)Math.PI / 2.0f);     
        }

        public Cubemap Generate(RenderManager renderMgr, Scene scene, Vector3 position)
        {
			Cubemap cubemap = new Cubemap();
            Generate(renderMgr, scene, position, ref cubemap);
            return cubemap;
        }

        public void Generate(RenderManager renderMgr, Scene scene, Vector3 position, ref Cubemap cubemap)
        {
            
            mCamera.Transform.LocalPosition = position;
            mCamera.Transform.LocalRotation = Quaternion.Identity;

            for (int i = 0; i < 6; i++)
            {
                mCamera.Transform.LocalRotation = Quaternion.FromEulerAngles(mCamRotations[i]);

				mFrameBuffer[i].Bind();
                renderMgr.Render(scene, mCamera, mFrameBuffer[i].Width, mFrameBuffer[i].Height);
                mFrameBuffer[i].Unbind();
			}

            cubemap.SetTexture(CubemapFace.Left, mFrameBuffer[1].Texture);
            cubemap.SetTexture(CubemapFace.Right, mFrameBuffer[3].Texture);
            cubemap.SetTexture(CubemapFace.Front, mFrameBuffer[2].Texture);
            cubemap.SetTexture(CubemapFace.Back, mFrameBuffer[0].Texture);
            cubemap.SetTexture(CubemapFace.Top, mFrameBuffer[5].Texture);
            cubemap.SetTexture(CubemapFace.Bottom, mFrameBuffer[4].Texture);

			cubemap.GenerateMipmaps();

			cubemap.TextureRight = mFrameBuffer[1].Texture;
            cubemap.TextureLeft = mFrameBuffer[3].Texture;
            cubemap.TextureBack = mFrameBuffer[2].Texture;
            cubemap.TextureFront = mFrameBuffer[0].Texture;
            cubemap.TextureTop = mFrameBuffer[5].Texture;
            cubemap.TextureBottom = mFrameBuffer[4].Texture;
            
        }
    }
}
