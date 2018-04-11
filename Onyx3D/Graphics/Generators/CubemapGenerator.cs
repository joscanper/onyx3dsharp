
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
            mCamRotations[0] = new Vector3(0, 0, 0);                                                    // front?
            mCamRotations[1] = new Vector3(0, (float)Math.PI / 2.0f, 0);                                // right?
            mCamRotations[2] = new Vector3(0, (float)Math.PI, 0);                                       // back?
            mCamRotations[3] = new Vector3(0, -(float)Math.PI / 2.0f, 0);                               // left?
            mCamRotations[4] = new Vector3((float)Math.PI / 2.0f, ((float)Math.PI / 2.0f) * 3, 0);      // top?
            mCamRotations[5] = new Vector3(-(float)Math.PI / 2.0f, ((float)Math.PI / 2.0f) * 3, 0);     // down?
        }

        public Cubemap Generate(RenderManager renderMgr, Scene scene, Vector3 position, float yaw = 0)
        {
			Cubemap cubemap = new Cubemap();
            Generate(renderMgr, scene, position, ref cubemap, yaw);
            return cubemap;
        }

        public void Generate(RenderManager renderMgr, Scene scene, Vector3 position, ref Cubemap cubemap, float yaw = 0)
        {
            mCamera.Transform.LocalPosition = position;
            Vector3 initRotation = new Vector3(0, yaw, 0);

            for (int i = 0; i < 6; i++)
            {
                mCamera.Transform.LocalRotation = Quaternion.FromEulerAngles(initRotation + mCamRotations[i]);

				mFrameBuffer[i].Bind();
                renderMgr.Render(scene, mCamera, mFrameBuffer[i].Width, mFrameBuffer[i].Height);
                mFrameBuffer[i].Unbind();

				cubemap.SetTexture(i, mFrameBuffer[i].Texture);

			}

			
            cubemap.TextureFront = mFrameBuffer[0].Texture;
            cubemap.TextureRight = mFrameBuffer[1].Texture;
            cubemap.TextureBack = mFrameBuffer[2].Texture;
            cubemap.TextureLeft = mFrameBuffer[3].Texture;
            cubemap.TextureUp = mFrameBuffer[4].Texture;
            cubemap.TextureDown = mFrameBuffer[5].Texture;
			
	
		}
    }
}
