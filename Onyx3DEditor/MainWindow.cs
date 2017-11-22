using System;
using System.Windows.Forms;

using Onyx3D;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3DEditor
{
	public partial class MainWindow : Form
	{
		bool canDraw = false;

		Onyx3DInstance myOnyxInstance;

		Camera myCamera;
		GridRenderer myGridRenderer;

		public MainWindow()
		{
			InitializeComponent();
			InitializeCanvas();
		}
		
		private void InitScene()
		{
			
			myOnyxInstance = new Onyx3DInstance();
			myOnyxInstance.Init();

			myCamera = new Camera("MainCamera");
			myCamera.Transform.LocalPosition = new Vector3(0, 1, 3);

			myGridRenderer = new SceneObject("Grid").AddComponent<GridRenderer>();
			myGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f);
			myGridRenderer.Material = myOnyxInstance.Content.BuiltInMaterials.Unlit;
		}

		#region RenderCanvas callbacks

		private void renderCanvas_Load(object sender, EventArgs e)
		{
			
			InitScene();
			canDraw = true;
		}

		private void renderCanvas_Paint(object sender, PaintEventArgs e)
		{
			if (!canDraw)
				return;
			
			myCamera.InitPerspective(1.5f, renderCanvas.Width / renderCanvas.Height);
			myCamera.UpdateUBO();
			myCamera.BindUBO(myGridRenderer.Material.Shader);


			GL.Viewport(0, 0, renderCanvas.Width, renderCanvas.Height);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			myGridRenderer.Render(myCamera);

			GL.Flush();
			renderCanvas.SwapBuffers();
		}

		#endregion

		#region UI callbacks

		private void toolStripButtonSaveProject_Click(object sender, EventArgs e)
		{
			ProjectManager.Instance.Save();
		}

		private void toolStripButtonMaterials_Click(object sender, EventArgs e)
		{
			new MaterialEditor().Show();
		}

		private void toolStripButtonTextures_Click(object sender, EventArgs e)
		{
			new TextureManager().Show();
		}

		#endregion

	}
}
