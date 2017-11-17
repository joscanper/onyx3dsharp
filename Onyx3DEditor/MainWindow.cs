using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Onyx3D;
using OpenTK.Graphics.OpenGL;

namespace Onyx3DEditor
{
	public partial class MainWindow : Form
	{
		bool canDraw = false;
		Camera myCamera;
		GridRenderer myGridRenderer;

		public MainWindow()
		{
			InitializeComponent();
			InitializeCanvas();
		}
		
		private void InitScene()
		{
			myCamera = new Camera("MainCamera");

			myGridRenderer = new SceneObject("Grid").AddComponent<GridRenderer>();
			myGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f);
		}

		#region RenderCanvas callbacks

		private void renderCanvas_Load(object sender, EventArgs e)
		{
			RenderManager.Instance.Init();
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
