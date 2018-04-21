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
        private bool mCanDraw;

        public Onyx3DInstance OnyxInstance;
        public Scene Scene;
        public Camera Camera;

        private SceneObject mCamPivot;

        private GridRenderer mGridRenderer;

        public Onyx3DControl()
        {
            InitializeCanvas();
            InitializeComponent();
        }

        public void Init()
        {
            OnyxInstance = new Onyx3DInstance();
            OnyxInstance.Init();

            InitializeBasicScene();

            renderTimer.Enabled = true;
        }

        public void InitializeBasicScene()
        {
            
            Scene = new Scene();

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
            light.AddComponent<Light>();
            light.Parent = Scene.Root;
            light.Transform.LocalPosition = Vector3.One * 5;
            
            SceneObject test = new SceneObject("ReflectionProbe");
            test.Parent = Scene.Root;
            test.Transform.LocalPosition = new Vector3(0, 0, 0);
            ReflectionProbe mReflectionProbe = test.AddComponent<ReflectionProbe>();
            mReflectionProbe.Init(64);
            
            SceneObject sky = new SceneObject("test_sky");
            sky.Transform.LocalScale = new Vector3(-1, 1, 1);
            MeshRenderer skyR = sky.AddComponent<MeshRenderer>();
            skyR.Mesh = OnyxInstance.Resources.GetMesh(BuiltInMesh.Sphere);
            skyR.Material = OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Sky);
            Scene.Sky = skyR;
            
        }

        private void RenderScene()
        {
            renderCanvas.MakeCurrent();
            
            if (OnyxInstance!= null)
            { 
                Scene.ActiveCamera.Update();
                OnyxInstance.Renderer.Render(Scene, Scene.ActiveCamera, renderCanvas.Width, renderCanvas.Height);
                OnyxInstance.Renderer.Render(mGridRenderer, Scene.ActiveCamera);
            }

            renderCanvas.SwapBuffers();
        }

        private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!mCanDraw)
                return;

            RenderScene();
        }

        private void renderCanvas_Load(object sender, EventArgs e)
        {
            mCanDraw = (LicenseManager.UsageMode == LicenseUsageMode.Runtime);
            renderCanvas.Refresh();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!mCanDraw)
                return;

            mCamPivot.Transform.Rotate(new Vector3(0, 0.01f, 0));
            renderCanvas.Refresh();
        }
    }
}
