using System;
using System.Drawing;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using Onyx3D;

namespace Onyx3DEditor
{
    public partial class MaterialEditor : Form
    {
        private bool canDraw = false;

		private Onyx3DInstance myOnyxInstance;
		
		private Scene mScene;
        private SceneObject mObject;
		private Shader mShader;
        private MeshRenderer mRenderer;
		private Camera mCamera;
		private GridRenderer mGridRenderer;

		private float mAngle = 0;
		
        public MaterialEditor()
        {
            InitializeComponent();
			InitializeCanvas();
		}


		private void InitScene()
		{
			myOnyxInstance = new Onyx3DInstance();
			myOnyxInstance.Init();

			RebuildShader();

			mScene = new Scene();
			
			mCamera = new PerspectiveCamera("MainCamera", 1.5f, (float)renderCanvas.Width / (float)renderCanvas.Height);
			mCamera.Transform.LocalPosition = new Vector3(0, 0.85f, 2);
			mCamera.Transform.LocalRotation = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -0.45f);
			mCamera.Parent = mScene.Root;

			mObject = new SceneObject("BaseObject");
			mObject.Parent = mScene.Root;

			mRenderer = mObject.AddComponent<MeshRenderer>();
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Teapot);
			mRenderer.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			mShader = mRenderer.Material.Shader;

			SceneObject grid = new SceneObject("Grid");
			//grid.Parent = mScene.Root;

			mGridRenderer = grid.AddComponent<GridRenderer>();
			mGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f);
			mGridRenderer.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			
			SceneObject light = new SceneObject("Light");
			light.AddComponent<Light>();
			light.Parent = mScene.Root;
			light.Transform.LocalPosition = Vector3.One * 1;
			
		}

		private void InitUI()
		{
			textBoxVertexCode.Text = mShader.VertexCode;
			textBoxFragmentCode.Text = mShader.FragmentCode;

			materialPropertiesControl.Fill(mRenderer.Material);
		}

		private void RebuildShader()
		{
			Logger.Instance.Clear();

			if (mShader == null)
			{
				mShader = myOnyxInstance.Resources.GetShader(BuiltInShader.Default);
			}
			else
			{
				mShader = new Shader();
				mShader.InitProgram(textBoxVertexCode.Text, textBoxFragmentCode.Text);
				mRenderer.Material.Shader = mShader;
			}

			textBoxLog.Text = Logger.Instance.Content;
		}

		private void RenderScene()
		{
			mCamera.Update();

			myOnyxInstance.Renderer.Render(mScene, mCamera, renderCanvas.Width, renderCanvas.Height);

			if (toolStripButtonGrid.CheckState == CheckState.Checked)
				mGridRenderer.Render();

			renderCanvas.SwapBuffers();
		}

		// --------------------------------------------------------------------

		#region RenderCanvas callbacks

		private void renderCanvas_Load(object sender, EventArgs e)
        {

			InitScene();
			InitUI();
			canDraw = true;

		}

		private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
			if (!canDraw)
				return;

			RenderScene();
        }

		#endregion

		#region UI callbacks

		private void toolStripNewMaterialButton_Click(object sender, EventArgs e)
		{
			toolStripMaterialsComboBox.Items.Add("New Material");
			toolStripMaterialsComboBox.SelectedIndex = toolStripMaterialsComboBox.Items.Count - 1;
		}

		private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControlMain.TabIndex == 0)
			{
				RebuildShader();
			}
		}
		

		private void toolStripButtonCube_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Cube);
			renderCanvas.Refresh();
		}

		private void toolStripButtonSphere_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Sphere);
			renderCanvas.Refresh();
		}
		
		private void toolStripButtonTorus_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Torus);
			renderCanvas.Refresh();
		}

		private void toolStripButtonCylinder_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Cylinder);
			renderCanvas.Refresh();
		}

		private void toolStripButtonTeapot_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Teapot);
			renderCanvas.Refresh();
		}

		private void materialProperties_Changed(object sender, EventArgs e)
		{
			renderCanvas.Refresh();
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			mAngle+= 0.05f;
			mObject.Transform.LocalRotation = Quaternion.FromEulerAngles(mAngle, mAngle, mAngle);
			renderCanvas.Refresh();
		}

		#endregion

		private void toolStripButtonGrid_Click(object sender, EventArgs e)
		{
			//mDrawGrid = false;
		}
	}
}
