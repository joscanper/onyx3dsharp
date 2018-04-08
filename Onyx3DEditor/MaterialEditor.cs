using System;
using System.Windows.Forms;

using OpenTK;
using Onyx3D;

namespace Onyx3DEditor
{
    public partial class MaterialEditor : Form
    {
        public Action<OnyxProjectMaterialAsset> MaterialSaved;

        private bool canDraw = false;

		private Onyx3DInstance myOnyxInstance;
		
		private Scene mScene;
        private SceneObject mObject;
		private Material mMaterial;
        private MeshRenderer mRenderer;
		private Camera mCamera;
		private GridRenderer mGridRenderer;
        private Shader mTestShader;

		private float mAngle = 0;

        public OnyxProjectMaterialAsset SelectedMaterial
        {
            get { return ProjectManager.Instance.Content.Materials[toolStripMaterialsComboBox.SelectedIndex]; }
        }
		
        public MaterialEditor()
        {
            InitializeComponent();
			InitializeCanvas();
		}


		private void InitScene()
		{
			myOnyxInstance = new Onyx3DInstance();
			myOnyxInstance.Init();

			//RebuildShader();

			mScene = new Scene();
			
			mCamera = new PerspectiveCamera("MainCamera", 1.5f, (float)renderCanvas.Width / (float)renderCanvas.Height);
			mCamera.Transform.LocalPosition = new Vector3(0, 0.85f, 2);
			mCamera.Transform.LocalRotation = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -0.45f);
			mCamera.Parent = mScene.Root;

			mObject = new SceneObject("BaseObject");
			mObject.Parent = mScene.Root;

			mRenderer = mObject.AddComponent<MeshRenderer>();
			mRenderer.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Teapot);
			
			SceneObject grid = new SceneObject("Grid");
			//grid.Parent = mScene.Root;

			mGridRenderer = grid.AddComponent<GridRenderer>();
			mGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f);
			mGridRenderer.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			
			SceneObject light = new SceneObject("Light");
			light.AddComponent<Light>();
			light.Parent = mScene.Root;
			light.Transform.LocalPosition = Vector3.One * 1;

			SetMaterial(myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default));
		}

		private void SetMaterial(Material mat)
		{
			mMaterial = mat;
			mRenderer.Material = mat;
			if (mat.LinkedProjectAsset.Guid != BuiltInMaterial.Default)
				materialPropertiesControl.Fill(mat);
		}

		private void InitUI()
		{
			textBoxVertexCode.Text = mMaterial.Shader.VertexCode;
			textBoxFragmentCode.Text = mMaterial.Shader.FragmentCode;

			UpdateMaterialList(0);
		}

		
		private void RebuildShader()
		{
			Logger.Instance.Clear();


            mTestShader = new Shader();
            mTestShader.InitProgram(textBoxVertexCode.Text, textBoxFragmentCode.Text);
			
			textBoxLog.Text = Logger.Instance.Content;
		}
		

		private void RenderScene()
		{

            renderCanvas.MakeCurrent();

			mCamera.Update();

            Shader originalShader = mMaterial.Shader;
            if (mTestShader != null)
                mMaterial.Shader = mTestShader;
            

			myOnyxInstance.Renderer.Render(mScene, mCamera, renderCanvas.Width, renderCanvas.Height);

            mMaterial.Shader = originalShader;


            if (toolStripButtonGrid.CheckState == CheckState.Checked)
				mGridRenderer.Render();

			renderCanvas.SwapBuffers();
		}

	
		private string SelectMaterialFile()
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = ProjectManager.Instance.CurrentProjectPath;
            saveFileDialog1.Filter = "Onyx3d material files (*.o3dmat)|*.o3dmat";
			saveFileDialog1.FilterIndex = 2;
			saveFileDialog1.RestoreDirectory = true;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					return saveFileDialog1.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not save the project: " + ex.StackTrace);
				}

			}

			return "";
		}

		private void UpdateMaterialList(int selected)
		{
			toolStripMaterialsComboBox.Items.Clear();
			int i = 0;
			foreach(OnyxProjectMaterialAsset m in ProjectManager.Instance.Content.Materials)
			{
				toolStripMaterialsComboBox.Items.Add(m.Name);
				if (m.Guid == selected)
					toolStripMaterialsComboBox.SelectedIndex = i;
				i++;
			}
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
			DefaultMaterial material = new DefaultMaterial();
			string matPath = SelectMaterialFile();
			if (matPath.Length == 0)
				return;

			ProjectManager.Instance.Content.AddMaterial(matPath, material);
            MaterialLoader.Save(material, material.LinkedProjectAsset.Path);

			SetMaterial(material);
			UpdateMaterialList(material.LinkedProjectAsset.Guid);
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
			//UpdateMaterialList(mMaterial.LinkedProjectAsset.Guid);
            toolStripMaterialsComboBox.SelectedItem = ((OnyxProjectMaterialAsset)mMaterial.LinkedProjectAsset).Name;
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

		private void toolStripMaterialsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int id = SelectedMaterial.Guid;
			SetMaterial(myOnyxInstance.Resources.GetMaterial(id));
		}

		private void toolStripSaveMaterialButton_Click(object sender, EventArgs e)
		{
			MaterialLoader.Save(mMaterial, mMaterial.LinkedProjectAsset.Path);
            MaterialSaved?.Invoke(SelectedMaterial);
        }

    }
}
