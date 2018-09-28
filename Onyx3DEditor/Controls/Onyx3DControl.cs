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

namespace Onyx3DEditor
{
    public partial class Onyx3DControl : UserControl
    {
        
        public Onyx3DInstance OnyxInstance;
        public Scene Scene;
        public Camera Camera;
		public bool DrawGrid;

        // --------------------------------------------------------------------

        private bool mCanDraw;
		private SceneObject mCamPivot;
        private GridRenderer mGridRenderer;
		private ReflectionProbe mReflectionProbe;

        // --------------------------------------------------------------------

        public GLControl RenderCanvas { get { return renderCanvas; } }

        // --------------------------------------------------------------------

        public Onyx3DControl()
        {
            InitializeCanvas();
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        public void Init()
        {
            OnyxInstance = new Onyx3DInstance(renderCanvas.Context, renderCanvas.WindowInfo);
            InitializeBasicScene();
        }

        // --------------------------------------------------------------------

        public void InitializeBasicScene()
        {
            
            Scene = new Scene(OnyxInstance);
			
			Camera = new PerspectiveCamera("MainCamera", 1.5f, (float)renderCanvas.Width / (float)renderCanvas.Height);
            Camera.Transform.LocalPosition = new Vector3(0, 0.65f, 1.25f);
            Camera.Transform.LocalRotation = OpenTK.Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -0.45f);
            
            Scene.ActiveCamera = Camera;

            mCamPivot = new SceneObject("camPivot");
            mCamPivot.Parent = Scene.Root;
            Camera.Parent = mCamPivot;

            SceneObject grid = new SceneObject("Grid");
            mGridRenderer = grid.AddComponent<GridRenderer>();
            mGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f, new Vector3(0.8f,0.8f,0.8f));
            mGridRenderer.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);

            SceneObject light = new SceneObject("Light");
			Light lightC = light.AddComponent<Light>();
            light.Parent = Scene.Root;
            light.Transform.LocalPosition = new Vector3(1,2,1);
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

        private void RenderScene()
        {

            if (OnyxInstance!= null)
            { 
                Scene.ActiveCamera.Update();
                OnyxInstance.Renderer.Render(Scene, Scene.ActiveCamera, renderCanvas.Width, renderCanvas.Height);
				if (DrawGrid)
					OnyxInstance.Renderer.Render(mGridRenderer, Scene.ActiveCamera);
            }

            renderCanvas.SwapBuffers();
        }

        // --------------------------------------------------------------------

        private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!mCanDraw)
                return;

			//mReflectionProbe.Bake(OnyxInstance.Renderer);


			RenderScene();
        }

        // --------------------------------------------------------------------

        private void renderCanvas_Load(object sender, EventArgs e)
        {
            mCanDraw = (LicenseManager.UsageMode == LicenseUsageMode.Runtime);
            renderCanvas.Refresh();
        }
		
    }
}
