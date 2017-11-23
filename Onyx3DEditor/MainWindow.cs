using System;
using System.Windows.Forms;

using Onyx3D;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

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
			if (ProjectManager.Instance.CurrentProjectPath.Length == 0)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
				saveFileDialog1.Filter = "Onyx3d project files (*.o3dproj)|*.o3dproj";
				saveFileDialog1.FilterIndex = 2;
				saveFileDialog1.RestoreDirectory = true;

				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					ProjectManager.Instance.Save(saveFileDialog1.FileName);
				}
			}
			else
			{
				ProjectManager.Instance.Save();
			}

			
		}

		private void toolStripButtonMaterials_Click(object sender, EventArgs e)
		{
			new MaterialEditor().Show();
		}

		private void toolStripButtonTextures_Click(object sender, EventArgs e)
		{
			new TextureManager().Show();
		}


		private void toolStripButtonNewProject_Click(object sender, EventArgs e)
		{
			var confirmResult = MessageBox.Show("Are you sure to start a new project?", "New Project", MessageBoxButtons.YesNo);
			if (confirmResult == DialogResult.Yes)
			{
				ProjectManager.Instance.New();
			}
		}

		private void toolStripButtonOpenProject_Click(object sender, EventArgs e)
		{

			Stream myStream;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "Onyx3d project files (*.o3dproj)|*.o3dproj";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((myStream = openFileDialog1.OpenFile()) != null)
					{
						ProjectManager.Instance.Load(myStream);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		#endregion

	}
}
