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

		private Material mMaterial;
        private SceneObject myObject;
		private Shader myShader;
        private MeshRenderer myRenderer;
		private Camera myCamera;

		private float mAngle = 0;
		
        public MaterialEditor()
        {
            InitializeComponent();
			InitializeCanvas();
		}

		private void InitGL()
		{
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);

			GL.Enable(EnableCap.Multisample);
			GL.Hint(HintTarget.MultisampleFilterHintNv, HintMode.Nicest);

			GL.Enable(EnableCap.LineSmooth);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			GL.ClearColor(Color.DarkBlue);
		}

		private void InitScene()
		{
			// TODO - Move this to Core
			ContentManager.Instance.Init();

			RebuildShader();

			myCamera = new Camera("MainCamera");
			myCamera.Transform.LocalPosition = new Vector3(0, 0.85f, 2);
			myCamera.Transform.LocalRotation = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -0.45f);

			myObject = new SceneObject("BaseObject");
			//myObject.Transform.LocalRotation = Quaternion.FromEulerAngles(0, 0, -90);

			mMaterial = ContentManager.Instance.DefaultMaterial; // TODO = Copy this shit instead

			myRenderer = myObject.AddComponent<MeshRenderer>();
			myRenderer.Mesh = PrimitiveMeshes.Teapot;
			myRenderer.Material = mMaterial;
		}

		private void InitUI()
		{
			textBoxVertexCode.Text = myShader.VertexCode;
			textBoxFragmentCode.Text = myShader.FragmentCode;

			materialPropertiesControl.Fill(mMaterial);
		}

		private void RebuildShader()
		{
			Logger.Instance.Clear();

			if (myShader == null)
			{
				myShader = ContentManager.Instance.DefaultShader;
			}
			else
			{
				myShader = new Shader();
				myShader.InitProgram(textBoxVertexCode.Text, textBoxFragmentCode.Text);
				myRenderer.Material.Shader = myShader;
			}

			textBoxLog.Text = Logger.Instance.Content;
		}

		#region RenderCanvas callbacks

		private void renderCanvas_Load(object sender, EventArgs e)
        {
			InitGL();
			InitScene();
			InitUI();
			canDraw = true;
		}

		private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
			if (!canDraw)
				return;

			myCamera.InitPerspective(1.5f, renderCanvas.Width / renderCanvas.Height);
			myCamera.UpdateUBO();
			myCamera.BindUBO(myRenderer.Material.Shader);
			
			
            GL.Viewport(0, 0, renderCanvas.Width, renderCanvas.Height);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            myRenderer.Render(myCamera);
            
			GL.Flush();
			renderCanvas.SwapBuffers();
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
			myRenderer.Mesh = PrimitiveMeshes.Cube;
			renderCanvas.Refresh();
		}

		private void toolStripButtonSphere_Click(object sender, EventArgs e)
		{
			myRenderer.Mesh = PrimitiveMeshes.Sphere;
			renderCanvas.Refresh();
		}
		
		private void toolStripButtonTorus_Click(object sender, EventArgs e)
		{
			myRenderer.Mesh = PrimitiveMeshes.Torus;
			renderCanvas.Refresh();
		}

		private void toolStripButtonCylinder_Click(object sender, EventArgs e)
		{
			myRenderer.Mesh = PrimitiveMeshes.Cylinder;
			renderCanvas.Refresh();
		}

		private void toolStripButtonTeapot_Click(object sender, EventArgs e)
		{
			myRenderer.Mesh = PrimitiveMeshes.Teapot;
			renderCanvas.Refresh();
		}

		private void materialProperties_Changed(object sender, EventArgs e)
		{
			renderCanvas.Refresh();
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			mAngle+= 0.05f;
			myObject.Transform.LocalRotation = Quaternion.FromEulerAngles(mAngle, mAngle, mAngle);
			renderCanvas.Refresh();
		}

		#endregion

	}
}
