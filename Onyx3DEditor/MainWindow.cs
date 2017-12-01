using System;
using System.Drawing;
using System.Windows.Forms;

using Onyx3D;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Collections.Generic;

namespace Onyx3DEditor
{
	public partial class MainWindow : Form
	{
		bool canDraw = false;

		Onyx3DInstance myOnyxInstance;

		Scene myScene;
		Camera myCamera;
		GridRenderer myGridRenderer;
		SceneObject myTeapot;

		OnyxViewerNavigation mNavigation = new OnyxViewerNavigation();

		public MainWindow()
		{
			InitializeComponent();
			InitializeCanvas();

			mNavigation.Bind(renderCanvas);
		}

		private void InitScene()
		{
			myScene = new Scene();

			myOnyxInstance = new Onyx3DInstance();
			myOnyxInstance.Init();

			
			SceneObject grid = new SceneObject("Grid");
			myGridRenderer = grid.AddComponent<GridRenderer>();
			myGridRenderer.GenerateGridMesh(10, 10, 0.25f, 0.25f);
			myGridRenderer.Material = myOnyxInstance.Content.BuiltInMaterials.Unlit;
			grid.Parent = myScene.Root;

			SceneObject teapot = new SceneObject("Teapot");
			MeshRenderer teapotMesh = teapot.AddComponent<MeshRenderer>();
			teapotMesh.Mesh = myOnyxInstance.Content.BuiltInMeshes.Teapot;
			teapot.Transform.LocalPosition = new Vector3(0, 0.5f, 0);
			teapotMesh.Material = myOnyxInstance.Content.BuiltInMaterials.Default;
			teapot.Parent = myScene.Root;
			myTeapot = teapot;

			SceneObject teapot2 = new SceneObject("Teapot2");
			MeshRenderer teapot2Mesh = teapot2.AddComponent<MeshRenderer>();
			teapot2Mesh.Mesh = myOnyxInstance.Content.BuiltInMeshes.Teapot;
			teapot2Mesh.Material = myOnyxInstance.Content.BuiltInMaterials.Default;
			teapot2.Transform.LocalScale = new Vector3(0.5f, 0.5f, 0.5f);
			teapot2.Transform.LocalPosition = new Vector3(0, 1.75f, 0);
			teapot2.Parent = myScene.Root;

			Axis axis = new Axis("Axis", myOnyxInstance.Content);
			axis.Parent = teapot;

			mNavigation.CreateCamera();

			UpdateTreeView();
		}

		private void UpdateTreeView()
		{
			treeViewSceneHierarchy.Nodes.Clear();
			TreeNode root = new TreeNode("Scene Name");
			if (myScene.Root.ChildCount > 0)
				AddSceneObjectToTreeNode(root, myScene.Root, true);
			treeViewSceneHierarchy.Nodes.Add(root);
			treeViewSceneHierarchy.ExpandAll();
		}

		private void AddSceneObjectToTreeNode(TreeNode node, SceneObject sceneObject, bool skipAdd)
		{
			TreeNode objectNode = new TreeNode(sceneObject.Id);
			if (!skipAdd)
				node.Nodes.Add(objectNode);
			for (int i = 0; i < sceneObject.ChildCount; ++i)
				AddSceneObjectToTreeNode(skipAdd ? node : objectNode, sceneObject.GetChild(i), false);

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


			mNavigation.NavigationCamera.InitPerspective(1.5f, (float)renderCanvas.Width / (float)renderCanvas.Height);
			mNavigation.UpdateCamera();

			myOnyxInstance.RenderManager.Render(myScene, mNavigation.NavigationCamera, renderCanvas.Width, renderCanvas.Height);

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
