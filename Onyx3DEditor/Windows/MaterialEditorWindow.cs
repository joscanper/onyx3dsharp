using System;
using System.Windows.Forms;

using OpenTK;
using Onyx3D;

namespace Onyx3DEditor
{
    public partial class MaterialEditorWindow : Form
    {
        public Action<OnyxProjectAsset> MaterialSaved;
		
		//private Onyx3DInstance myOnyxInstance;
		
		
        private SceneObject mObject;
		private Material mMaterial;
        private MeshRenderer mRenderer;
        private Shader mTestShader;
		

		private float mAngle = 0;

        public OnyxProjectAsset SelectedMaterial
        {
            get { return materialViewList1.SelectedMaterial; }
        }
		
        public MaterialEditorWindow()
        {
            InitializeComponent();
		}

		private void MaterialEditorWindow_Shown(object sender, System.EventArgs e)
		{
			if (!DesignMode)
			{
				onyx3DControl.Init();
				InitScene();
				InitUI();
			}
		}

		private void InitScene()
		{

			mObject = new SceneObject("BaseObject");
            mObject.Parent = onyx3DControl.Scene.Root;
			mObject.Transform.LocalPosition = new Vector3(0, 0.5f, 0);
			mRenderer = mObject.AddComponent<MeshRenderer>();
            mRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Sphere);
            Material m = onyx3DControl.OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
            SetMaterial(m);

			onyx3DControl.BakeReflection();

			SceneObject floor = new SceneObject("Floor");
			floor.Parent = onyx3DControl.Scene.Root;
			floor.Transform.LocalPosition = new Vector3(0, 0, 0);
			floor.Transform.LocalScale = new Vector3(4, 4, 4);
			MeshRenderer floorMesh = floor.AddComponent<MeshRenderer>();
			floorMesh.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Quad);
			floorMesh.Material = onyx3DControl.OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);

			onyx3DControl.Camera.Transform.Translate(
				onyx3DControl.Camera.Transform.Forward * 0.5f  + onyx3DControl.Camera.Transform.Up * 0.25f
				);

			//cubemapViewer1.Init(mReflectionProbe.Cubemap);
		}

		private SceneObject AddPrimitive(int meshType, string name)
        {
            SceneObject primitive = SceneObject.CreatePrimitive(meshType, name);
            primitive.Parent = onyx3DControl.Scene.Root;
            return primitive;
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
            materialViewList1.UpdateMaterialList(-1, false);
		}

		
		private void RebuildShader()
		{
			Logger.Instance.Clear();


            mTestShader = new Shader();
            mTestShader.InitProgram(textBoxVertexCode.Text, textBoxFragmentCode.Text);
			
			textBoxLog.Text = Logger.Instance.Content;
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


		// --------------------------------------------------------------------

		#region UI callbacks

		private void toolStripNewMaterialButton_Click(object sender, EventArgs e)
		{
			
			if (!ProjectLoader.AssertProjectExists())
				return;

			DefaultMaterial material = new DefaultMaterial();
			string matPath = SelectMaterialFile();
			if (matPath.Length == 0)
				return;

			ProjectManager.Instance.Content.AddMaterial(matPath, false, material);
			AssetLoader<Material>.Save(material, material.LinkedProjectAsset.Path);

			SetMaterial(material);
			materialViewList1.UpdateMaterialList(material.LinkedProjectAsset.Guid, false);
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
			mRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Cube);
			onyx3DControl.Refresh();
		}

		private void toolStripButtonSphere_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Sphere);
			onyx3DControl.Refresh();
		}
		
		private void toolStripButtonTorus_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Torus);
			onyx3DControl.Refresh();
		}

		private void toolStripButtonCylinder_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Cylinder);
			onyx3DControl.Refresh();
		}

		private void toolStripButtonTeapot_Click(object sender, EventArgs e)
		{
			mRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(BuiltInMesh.Teapot);
			onyx3DControl.Refresh();
		}

		private void materialProperties_Changed(object sender, EventArgs e)
		{
			onyx3DControl.Refresh();
			//UpdateMaterialList(mMaterial.LinkedProjectAsset.Guid);
			//materialViewList1.SelectedItem = ((OnyxProjectMaterialAsset)mMaterial.LinkedProjectAsset).Name;
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			mAngle+= 0.05f;
			mObject.Transform.LocalRotation = Quaternion.FromEulerAngles(0, mAngle, 0);
			onyx3DControl.Refresh();
		}

		#endregion

		private void toolStripButtonGrid_Click(object sender, EventArgs e)
		{
			onyx3DControl.DrawGrid = !onyx3DControl.DrawGrid;
		}

		private void materialViewList_SelectedChanged(object sender, EventArgs e)
		{
			int id = SelectedMaterial.Guid;
			SetMaterial(onyx3DControl.OnyxInstance.Resources.GetMaterial(id));
		}

		private void toolStripSaveMaterialButton_Click(object sender, EventArgs e)
		{
			AssetLoader<Material>.Save(mMaterial, mMaterial.LinkedProjectAsset.Path);
            MaterialSaved?.Invoke(SelectedMaterial);
            mMaterial.LinkedProjectAsset.Dirty = true;

            materialViewList1.UpdateMaterial(SelectedMaterial != null ? SelectedMaterial.Guid : -1);
        }

		private void toolStripDeleteMaterialButton_Click(object sender, EventArgs e)
		{
			if (SelectedMaterial != null)
			{
				if (MessageBox.Show("Are you sure?", "Delete Material", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					if (ProjectManager.Instance.Content.Materials.Remove(SelectedMaterial))
					{
						materialPropertiesControl.Clear();
						materialViewList1.UpdateMaterialList(0, false);
					}
				}
			}
				
		}
	}
}
