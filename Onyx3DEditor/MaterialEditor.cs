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

        private SceneObject myObject;
		private Shader myShader;
        private MeshRenderer myRenderer;

		private Matrix4 mView = Matrix4.Identity;
		private Matrix4 mProjection;

        public MaterialEditor()
        {
            InitializeComponent();
			InitializeGL();
			
		}

        private void renderCanvas_Load(object sender, EventArgs e)
        {
			RebuildShader();

			myObject = new SceneObject("BaseObject");
			myObject.Transform.LocalPosition = new Vector3(0, 0, -2);
			myObject.Transform.LocalScale = new Vector3(2, 1, 1);

			myRenderer = myObject.AddComponent<MeshRenderer>();
            myRenderer.Mesh = new CubeMesh();
            myRenderer.Material = new Material();
            myRenderer.Material.Shader = myShader;
			
			textBoxVertexCode.Text = myShader.VertexCode;
			textBoxFragmentCode.Text = myShader.FragmentCode;

			canDraw = true;
        }

		private void RebuildShader()
		{
			Logger.Instance.Clear();
			if (myShader == null)
				myShader = new Shader("./Shaders/VertexShader.glsl", "./Shaders/FragmentShader.glsl");
			else
				myShader.InitProgram(textBoxVertexCode.Text, textBoxFragmentCode.Text);
			textBoxLog.Text = Logger.Instance.Content;
		}
     

        private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
			if (!canDraw)
				return;

			mProjection = Matrix4.CreatePerspectiveFieldOfView(1.3f, renderCanvas.Width / renderCanvas.Height, 0.1f, 1000f);

			GL.Enable(EnableCap.DepthTest);
			GL.ClearColor(Color.DarkBlue);
            GL.Viewport(0, 0, renderCanvas.Width, renderCanvas.Height);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            myRenderer.Render(mView, mProjection);
            
			GL.Flush();
			renderCanvas.SwapBuffers();
        }

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

		private void trackBarRotation_ValueChanged(object sender, EventArgs e)
		{
			myObject.Transform.LocalRotation = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), (float)(trackBarRotation.Value * Math.PI / 180.0f));
			renderCanvas.Refresh();
		}
	}
}
