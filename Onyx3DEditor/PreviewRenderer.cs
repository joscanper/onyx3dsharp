using System;
using System.Drawing;

using Onyx3D;
using OpenTK;

namespace Onyx3DEditor
{
	public class PreviewRenderer : IDisposable
	{
		
		public Onyx3DInstance OnyxInstance;
		public Scene Scene;
		public Camera Camera;
		public bool DrawGrid;
		
		private SceneObject mCamPivot;
        private FrameBuffer mFrameBuffer;
        private GridRenderer mGridRenderer;
        private ReflectionProbe mReflectionProbe;
        
        // --------------------------------------------------------------------

        public void Init(int w, int h, IntPtr handle)
		{
			
			OnyxInstance = new Onyx3DInstance();
			mFrameBuffer = new FrameBuffer(w, h);

			InitializeBasicScene();
		}

        // --------------------------------------------------------------------

        public virtual void InitializeBasicScene()
		{

			Scene = new Scene(OnyxInstance);

			Camera = new PerspectiveCamera("MainCamera", 1.5f, (float)mFrameBuffer.Width / (float)mFrameBuffer.Height);
			Camera.Transform.LocalPosition = new Vector3(0, 0.65f, 1.25f);
			Camera.Transform.LocalRotation = OpenTK.Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -0.45f);

			Scene.ActiveCamera = Camera;

			mCamPivot = new SceneObject("camPivot");
			mCamPivot.Parent = Scene.Root;
			Camera.Parent = mCamPivot;

			SceneObject grid = new SceneObject("Grid");
			mGridRenderer = grid.AddComponent<GridRenderer>();
			mGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f, new Vector3(0.8f, 0.8f, 0.8f));
			mGridRenderer.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);

			SceneObject light = new SceneObject("Light");
			Light lightC = light.AddComponent<Light>();
			light.Parent = Scene.Root;
			light.Transform.LocalPosition = new Vector3(1, 2, 1);
			lightC.Intensity = 5;

			SceneObject test = new SceneObject("ReflectionProbe");
			test.Parent = Scene.Root;
			test.Transform.LocalPosition = new Vector3(0, 0, 0);
			mReflectionProbe = test.AddComponent<ReflectionProbe>();
			mReflectionProbe.Init(64);
			
			mReflectionProbe.Bake(OnyxInstance.Renderer);
		}

        // --------------------------------------------------------------------

        public void BakeReflection()
		{
			mReflectionProbe.Bake(OnyxInstance.Renderer);
		}

        // --------------------------------------------------------------------

        public void Render()
		{
			//OnyxInstance.Context.MakeCurrent();
			if (OnyxInstance != null)
			{
				mFrameBuffer.Bind();
				OnyxInstance.Renderer.Render(Scene, Scene.ActiveCamera, mFrameBuffer.Width, mFrameBuffer.Height);
				if (DrawGrid)
					OnyxInstance.Renderer.Render(mGridRenderer, Scene.ActiveCamera);
				mFrameBuffer.Unbind();
			}	
		}

        // --------------------------------------------------------------------

        public Bitmap AsBitmap()
		{
			return mFrameBuffer.Texture.AsBitmap();
		}

        // --------------------------------------------------------------------

        public void Dispose()
		{
			mFrameBuffer.Dispose();
			Scene.Dispose();
			Camera.Dispose();

			mFrameBuffer = null;
			Scene = null;
			Camera = null;
		}
	}
}
