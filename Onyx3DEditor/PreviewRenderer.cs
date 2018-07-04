using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace Onyx3DEditor
{
	public class PreviewRenderer : IDisposable
	{
		private IWindowInfo mWindowInfo;
		private GraphicsContext mContext;

		private FrameBuffer mFrameBuffer;
		public Onyx3DInstance OnyxInstance;
		public Scene Scene;
		public Camera Camera;
		public bool DrawGrid;
		
		private SceneObject mCamPivot;
		private GridRenderer mGridRenderer;
		private ReflectionProbe mReflectionProbe;
		
		public PreviewRenderer()
		{
			
		}

		public void Init(int w, int h, IntPtr handle)
		{

			mWindowInfo = Utilities.CreateWindowsWindowInfo(handle);
			mContext = new GraphicsContext(new GraphicsMode(new ColorFormat(), 32, 32, 8), mWindowInfo);
			mContext.MakeCurrent(mWindowInfo);
			((IGraphicsContextInternal)mContext).LoadAll();

			OnyxInstance = new Onyx3DInstance();
			mFrameBuffer = new FrameBuffer(w, h);

			InitializeBasicScene();
		}

		public virtual void InitializeBasicScene()
		{

			Scene = new Scene();

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

		public void BakeReflection()
		{
			mReflectionProbe.Bake(OnyxInstance.Renderer);
		}

		public void Render()
		{

			mContext.MakeCurrent(mWindowInfo);
			if (OnyxInstance != null)
			{
				mFrameBuffer.Bind();
				OnyxInstance.Renderer.Render(Scene, Scene.ActiveCamera, mFrameBuffer.Width, mFrameBuffer.Height);
				if (DrawGrid)
					OnyxInstance.Renderer.Render(mGridRenderer, Scene.ActiveCamera);
				mFrameBuffer.Unbind();
			}	
		}

		public Bitmap AsBitmap()
		{
			Bitmap bitmap = mFrameBuffer.Texture.AsBitmap();
			bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return bitmap;
		}

		public void Dispose()
		{
			mFrameBuffer.Dispose();
			Scene.Dispose();
			Camera.Dispose();
			mWindowInfo.Dispose();
			mContext.Dispose();

			mFrameBuffer = null;
			Scene = null;
			Camera = null;
			mWindowInfo = null;
			mContext = null;
		}
	}
}
