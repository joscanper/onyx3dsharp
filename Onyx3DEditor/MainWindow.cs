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
		OnyxProjectSceneAsset mSceneAsset;
		Scene myScene;
		GridRenderer myGridRenderer;
		//SceneObject myTeapot;
		SceneObject mSelectedSceneObject;
        
		Ray myClickRay;
		
		


		OnyxViewerNavigation mNavigation = new OnyxViewerNavigation();

		public MainWindow()
		{
			InitializeComponent();
			InitializeCanvas();

			mNavigation.Bind(renderCanvas);
		}

		private void InitScene()
		{
			

			myOnyxInstance = Onyx3DEngine.Instance;
			myOnyxInstance.Init();

			mSceneAsset = ProjectManager.Instance.Content.GetInitScene();
			if (mSceneAsset == null)
			{
				myScene = new Scene();
			}
			else
			{
				myScene = SceneLoader.Load(mSceneAsset.Path);
			}

			/*
			SceneObject teapot = new SceneObject("Teapot");
			MeshRenderer teapotMesh = teapot.AddComponent<MeshRenderer>();
			teapotMesh.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Teapot);
			teapot.Transform.LocalPosition = new Vector3(0, 0.5f, 0);
			teapotMesh.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			teapot.Parent = myScene.Root;
            

            
            SceneObject teapot2 = new SceneObject("Teapot2");
			MeshRenderer teapot2Mesh = teapot2.AddComponent<MeshRenderer>();
			teapot2Mesh.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Teapot);
			teapot2Mesh.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			teapot2.Transform.LocalScale = new Vector3(0.5f, 0.5f, 0.5f);
			teapot2.Transform.LocalPosition = new Vector3(2, 0, 2);
            teapot2.Transform.LocalRotation = Quaternion.FromEulerAngles(new Vector3(0, 90, 0));
            teapot2.Parent = myScene.Root;

			myTeapot = teapot2;
			*/


			// Editor objects --------------------------------------

			SceneObject grid = new SceneObject("Grid");
			myGridRenderer = grid.AddComponent<GridRenderer>();
			myGridRenderer.GenerateGridMesh(100, 100, 0.25f, 0.25f);
			myGridRenderer.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Unlit);
			myGridRenderer.Material.Properties["color"].Data = new Vector4(1, 1, 1, 0.1f);
			
			//myBox = teapot.AddComponent<BoxRenderer>();
			//myBox.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.UnlitVertexColor);

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
			SceneTreeNode objectNode = new SceneTreeNode(sceneObject);
			if (!skipAdd)
				node.Nodes.Add(objectNode);
			for (int i = 0; i < sceneObject.ChildCount; ++i)
				AddSceneObjectToTreeNode(skipAdd ? node : objectNode, sceneObject.GetChild(i), false);

		}

		private void SelectObject(SceneObject node)
		{
			mSelectedSceneObject = node;
			renderCanvas.Refresh();
			
			if (mSelectedSceneObject != null)
			{
				splitContainer2.Panel2Collapsed = false;
				selectedObjectInspector.Fill(mSelectedSceneObject);
			}
			else
			{
				splitContainer2.Panel2Collapsed = true;
				selectedObjectInspector.Clear();
			}

		}


		private void OnSceneSelected(Scene s)
		{
			myScene = s;
			UpdateTreeView();
			renderCanvas.Refresh();
		}

		private void SaveScene()
		{
			//ProjectManager.Instance.Content.Scenes.Add(myScene);

			if (mSceneAsset == null)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
				saveFileDialog1.Filter = "Onyx3d scene files (*.o3dscene)|*.o3dscene";
				saveFileDialog1.FilterIndex = 2;
				saveFileDialog1.RestoreDirectory = true;

				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					SceneLoader.Save(myScene, saveFileDialog1.FileName);
					try
					{
						mSceneAsset = new OnyxProjectSceneAsset(saveFileDialog1.FileName);
						ProjectManager.Instance.Content.Scenes.Add(mSceneAsset);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error saving the scene : " + ex.Message);
					}


				}
			}
			else
			{
				SceneLoader.Save(myScene, mSceneAsset.Path);
			}
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

			mNavigation.UpdateCamera();

			myOnyxInstance.Renderer.Render(myScene, mNavigation.NavigationCamera, renderCanvas.Width, renderCanvas.Height);
            myOnyxInstance.Renderer.Render(myGridRenderer, mNavigation.NavigationCamera);
			
			myOnyxInstance.Gizmos.DrawLine(myClickRay.Origin, myClickRay.Origin + myClickRay.Direction * 10, Vector3.One);

			if (mSelectedSceneObject != null)
			{
				myOnyxInstance.Gizmos.DrawBox(mSelectedSceneObject.GetComponent<MeshRenderer>().Bounds, Vector3.Zero);
				myOnyxInstance.Gizmos.DrawAxis(mSelectedSceneObject.Transform.Position);
			}

			myOnyxInstance.Gizmos.Render(mNavigation.NavigationCamera);
			
			renderCanvas.SwapBuffers();
		}

		#endregion

		#region UI callbacks
		
		private void renderCanvas_Click(object sender, EventArgs e)
		{
			MouseEventArgs mouseEvent = e as MouseEventArgs;
			if (mouseEvent.Button == MouseButtons.Left)
			{
				myClickRay = mNavigation.NavigationCamera.ScreenPointToRay(mouseEvent.X, mouseEvent.Y, renderCanvas.Width, renderCanvas.Height);
				
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(myClickRay, out hit, myScene))
                {
                    SelectObject(hit.Object);
                }
                else
                {
                    SelectObject(null);
                }

				renderCanvas.Refresh();
			}
		}


		private void MainWindow_Activated(object sender, EventArgs e)
		{
			if (renderCanvas.Context != null)
				renderCanvas.MakeCurrent();
		}

		private void toolStripButtonSaveProject_Click(object sender, EventArgs e)
		{
			SaveScene();
				

			if (ProjectManager.Instance.CurrentProjectPath.Length == 0)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog();
				saveFileDialog1.Filter = "Onyx3d project files (*.o3dproj)|*.o3dproj";
				saveFileDialog1.FilterIndex = 2;
				saveFileDialog1.RestoreDirectory = true;

				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					try
					{
						ProjectManager.Instance.Save(saveFileDialog1.FileName);
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error: Could not save the project: " + ex.StackTrace);
					}
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
				//try
				//{
					ProjectManager.Instance.Load(openFileDialog1.FileName);
					InitScene();
				//}
				//catch (Exception ex)
				//{
				//	MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				//}
			}
		}

		private void treeViewSceneHierarchy_NodeSelected(object sender, TreeViewEventArgs e)
		{
			if (e.Node.GetType() != typeof(SceneTreeNode))
			{
				SelectObject(null);
				return;
			}

			SceneTreeNode sceneTreeeNode = (SceneTreeNode)e.Node;
			if (sceneTreeeNode != null)
			{
				SelectObject(sceneTreeeNode.BoundSceneObject);
			}
		}

		private void toolStripButtonChangeScene_Click(object sender, EventArgs e)
		{
			SceneSelector ss = new SceneSelector();
			ss.OnSceneSelected += OnSceneSelected;
			ss.Show();
		}

		private void toolStripButtonOpenScene_Click(object sender, EventArgs e)
		{

			Stream myStream;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "Onyx3d project files (*.o3dscene)|*.o3dscene";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((myStream = openFileDialog1.OpenFile()) != null)
					{
						myScene = SceneLoader.Load(openFileDialog1.FileName);
						UpdateTreeView();
						renderCanvas.Refresh();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
			
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //myTeapot.Transform.Rotate(new Vector3(0, 0.1f, 0));
            renderCanvas.Refresh();
        }

		#endregion

		private void toolStripCreateCube_Click(object sender, EventArgs e)
		{
			SceneObject cube = new SceneObject("Cube");
			MeshRenderer mesh = cube.AddComponent<MeshRenderer>();
			mesh.Mesh = myOnyxInstance.Resources.GetMesh(BuiltInMesh.Cube);
			cube.Transform.LocalPosition = new Vector3(0, 0.0f, 0);
			mesh.Material = myOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
			cube.Parent = myScene.Root;
			UpdateTreeView();
		}
	}
}
