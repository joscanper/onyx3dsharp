
using OpenTK;
using System;

namespace Onyx3D
{
    public class CubemapGenerator : IDisposable
    {
      
        private Camera mCamera;
        private FrameBuffer mFrameBuffer;
        private Vector3[] mCamRotations;

        public CubemapGenerator(int size)
        {
            mCamera = new PerspectiveCamera("", (float)Math.PI / 2.0f, 1);
            
            mFrameBuffer = new FrameBuffer(size, size);

            mCamRotations = new Vector3[6];
            
            mCamRotations[0] = new Vector3(0, -(float)Math.PI / 2.0f, 0); // Right
            mCamRotations[1] = new Vector3(0, (float)Math.PI / 2.0f, 0);  // Left
            mCamRotations[2] = new Vector3(-(float)Math.PI / 2.0f, 0, 0); // Top
            mCamRotations[3] = new Vector3((float)Math.PI / 2.0f, 0, 0);  // Bottom
            mCamRotations[4] = new Vector3(0, 0, 0);                      // Back                                              
            mCamRotations[5] = new Vector3(0, (float)Math.PI, 0);         // Front                     
            
        }

		public void Dispose()
		{
            mFrameBuffer.Dispose();
            mCamera.Dispose();

            mFrameBuffer = null;
            mCamera = null;
        }

        public void Generate(RenderManager renderMgr, Scene scene, Vector3 position, ref Cubemap cubemap)
        {
            
            mCamera.Transform.LocalPosition = position;
            mCamera.Transform.LocalRotation = Quaternion.Identity;

            for (int i = 0; i < 6; i++)
            {
                mCamera.Transform.LocalRotation = Quaternion.FromEulerAngles(mCamRotations[i]);

				mFrameBuffer.Bind();
                renderMgr.RenderScene(scene, mCamera, mFrameBuffer.Width, mFrameBuffer.Height);
                mFrameBuffer.Unbind();

                cubemap.SetTexture((CubemapFace)i, mFrameBuffer.Texture);
            }
            
			cubemap.GenerateMipmaps();
           
        }
    }
}
