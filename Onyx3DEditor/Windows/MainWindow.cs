using Onyx3D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	public partial class MainWindow : SingletonForm<MainWindow>
	{
		private bool mCanDraw = false;
		private Onyx3DInstance mOnyxInstance;
		private GridRenderer mGridRenderer;
		private SceneObject mSelectedSceneObject;
		private EntityProxy mSelectedEntity;
		private ObjectHandler mObjectHandler;
		private OnyxViewerNavigation mNavigation = new OnyxViewerNavigation();

		// --------------------------------------------------------------------

		private bool EditingEntity { get { return mSelectedEntity != null; } }

		// --------------------------------------------------------------------

		public MainWindow()
		{
			InitializeComponent();
			InitializeCanvas();

			Selection.OnSelectionChanged += OnSelectionChanged;
			SceneManagement.OnSceneChanged += OnSceneChanged;

			sceneHierarchy.OnEntityEditingChange += OnEntityEditingChange;

			mNavigation.Bind(renderCanvas);

			KeyPreview = true;
		}

		// --------------------------------------------------------------------

		private void InitializeEditor()
		{
			Onyx3DEngine.InitMain(renderCanvas.Context, renderCanvas.WindowInfo);
			mOnyxInstance = Onyx3DEngine.Instance;

			mNavigation.CreateCamera();

			mObjectHandler = new ObjectHandler(mOnyxInstance, renderCanvas, mNavigation.Camera);
			mObjectHandler.OnTransformModified += OnTransformModifiedFromObjectHandler;

			selectedObjectInspector.InspectorChanged += OnInspectorChanged;

			SceneObject grid = new SceneObject("Grid");
			mGridRenderer = grid.AddComponent<GridRenderer>();
			mGridRenderer.GenerateGridMesh(100, 100, 0.25f, 0.25f, Vector3.One);
			mGridRenderer.Material = mOnyxInstance.Resources.GetMaterial(BuiltInMaterial.Unlit);
			mGridRenderer.Material.Properties["color"].Data = new Vector4(1, 1, 1, 0.1f);

			UpdateFormTitle();
		}

		// --------------------------------------------------------------------

		private void UpdateFormTitle()
		{
			StringBuilder name = new StringBuilder();
			name.Append("Onyx3DEditor - ");
			name.Append(ProjectManager.Instance.CurrentProjectPath.Length > 0 ? ProjectManager.Instance.CurrentProjectPath : "[Unsaved Project]");

			Text = name.ToString();
		}

		// --------------------------------------------------------------------

		private void OnSelectionChanged(List<SceneObject> selected)
		{
			mSelectedSceneObject = Selection.ActiveObject;
			renderCanvas.Refresh();

			if (mSelectedSceneObject != null)
			{
				selectedObjectInspector.Fill(mSelectedSceneObject);
			}
			else
			{
				selectedObjectInspector.Fill(SceneManagement.ActiveScene);
			}

			mObjectHandler.HandleObject(mSelectedSceneObject);
			RenderScene();
		}

		// --------------------------------------------------------------------

		private void OnInspectorChanged(object sender, EventArgs args)
		{
			RenderScene();
		}

		// --------------------------------------------------------------------

		private void OnTransformModifiedFromObjectHandler()
		{
			selectedObjectInspector.Fill(mSelectedSceneObject);
		}

		// --------------------------------------------------------------------

		private void OnSceneChanged(Scene s)
		{
			sceneHierarchy.SetScene(s);
			renderCanvas.Refresh();
			Selection.ActiveObject = null;
		}

		// --------------------------------------------------------------------

		private void HighlightSelected()
		{
			if (mSelectedSceneObject != null)
			{
				if (EditingEntity)
				{
					mSelectedEntity.CalculateBounds();
				}

				mObjectHandler.Update();

				foreach (SceneObject obj in Selection.Selected)
				{
					Bounds bounds = obj.CalculateBounds();
					mOnyxInstance.Gizmos.DrawBox(bounds.Center, bounds.Size, Color.White.ToVector().Xyz);
				}

				Bounds activeBounds = Selection.ActiveObject.CalculateBounds();
				mOnyxInstance.Gizmos.DrawBox(activeBounds.Center, activeBounds.Size * 1.01f, Color.Orange.ToVector().Xyz);
			}
		}

		// --------------------------------------------------------------------

		private void RenderScene()
		{
			if (!mCanDraw)
				return;

			renderCanvas.MakeCurrent();
			mOnyxInstance.Resources.RefreshDirty();

			mNavigation.UpdateCamera();

			mOnyxInstance.Renderer.MainRender(SceneManagement.ActiveScene, mNavigation.Camera, renderCanvas.Width, renderCanvas.Height);
			mOnyxInstance.Renderer.Render(mGridRenderer, mNavigation.Camera);

			HighlightSelected();

			mOnyxInstance.Gizmos.DrawComponentGizmos(mNavigation.Camera, SceneManagement.ActiveScene);
			
			renderCanvas.SwapBuffers();
		}

		// --------------------------------------------------------------------

		public void UpdateHierarchy()
		{
			sceneHierarchy.UpdateScene();
		}

		// --------------------------------------------------------------------

		private void Select(SceneObject obj)
		{
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				Selection.Add(obj);
			}
			else
			{
				Selection.ActiveObject = obj;
			}
		}

		// --------------------------------------------------------------------

		private void SetEditingEntity(EntityProxy proxy)
		{
			mSelectedEntity = proxy;
			Selection.ActiveObject = null;
		}

		// --------------------------------------------------------------------

		#region RenderCanvas callbacks

		private void renderCanvas_Load(object sender, EventArgs e)
		{
			InitializeEditor();
			SceneManagement.LoadInitScene();

			mCanDraw = true;

			RenderScene();
		}

		private void renderCanvas_Paint(object sender, PaintEventArgs e)
		{
			RenderScene();
			renderInfoLabel.Text = string.Format("Render Time: {0}ms", Math.Round(mOnyxInstance.Renderer.RenderTime));
			labelLoggerOutput.Text = Logger.Instance.Content;
			Profiler.Instance.Clear();
		}

		private void renderCanvas_Click(object sender, EventArgs e)
		{
			MouseEventArgs mouseEvent = e as MouseEventArgs;

			if (mouseEvent.Button == MouseButtons.Left && !mObjectHandler.IsHandling)
			{
				Ray clickRay = mNavigation.Camera.ScreenPointToRay(mouseEvent.X, mouseEvent.Y, renderCanvas.Width, renderCanvas.Height);

				RaycastHit hit = new RaycastHit();
				if (!EditingEntity)
				{
					if (Physics.RaycastScene(clickRay, out hit, SceneManagement.ActiveScene))
					{
						Select(hit.Object);
					}
					else
					{
						Selection.ActiveObject = null;
					}
				}
				else
				{
					if (Physics.RaycastEntity(clickRay, out hit, mSelectedEntity))
					{
						Select(hit.Object);
					}
					else
					{
						Selection.ActiveObject = null;
					}
				}

				renderCanvas.Refresh();
			}
		}

		private void renderCanvas_DoubleClick(object sender, EventArgs e)
		{
			if (Selection.ActiveObject != null && Selection.ActiveObject.GetType() == typeof(EntityProxy))
			{
				EntityProxy proxy = (EntityProxy)Selection.ActiveObject;
				sceneHierarchy.EnterEntity(proxy);
				SetEditingEntity(proxy);
			}else if (Selection.ActiveObject == null && EditingEntity)
			{
				sceneHierarchy.ExitEntity();
				SetEditingEntity(null);
			}
		}

		#endregion RenderCanvas callbacks

		// --------------------------------------------------------------------

		#region UI callbacks

		private void timer1_Tick(object sender, EventArgs e)
		{
			mNavigation.OnFrameTick();
		}

		private void toolStripButtonEntityManager_Click(object sender, EventArgs e)
		{
			new EntitySelectorWindow().Show();
		}

		private void toolStripButtonSaveProject_Click(object sender, EventArgs e)
		{
			EditorSceneUtils.Save();
			ProjectLoader.Save();
			Logger.Instance.Clear();
			Logger.Instance.Append("Saved " + DateTime.Now.ToString());
			UpdateFormTitle();
		}

		private void toolStripButtonMaterials_Click(object sender, EventArgs e)
		{
			MaterialEditorWindow matEditor = new MaterialEditorWindow();
			matEditor.Show();
		}

		private void toolStripButtonTextures_Click(object sender, EventArgs e)
		{
			new TextureManagerWindow().Show();
		}

		private void toolStripButtonNewProject_Click(object sender, EventArgs e)
		{
			var confirmResult = MessageBox.Show("Are you sure to start a new project?", "New Project", MessageBoxButtons.YesNo);
			if (confirmResult == DialogResult.Yes)
			{
                mOnyxInstance.Reset();
                ProjectManager.Instance.New();
				SceneManagement.New();
				ProjectLoader.Save();
			}
		}

		private void toolStripButtonOpenProject_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "Onyx3d project files (*.o3dproj)|*.o3dproj";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				ProjectManager.Instance.Load(openFileDialog1.FileName);
				SceneManagement.LoadInitScene();
			}
		}

		private void toolStripButtonChangeScene_Click(object sender, EventArgs e)
		{
			SceneSelectorWindow ss = new SceneSelectorWindow();
			ss.OnSceneSelected += OnSceneChanged;
			ss.Show();
		}

		private void toolStripButtonOpenScene_Click(object sender, EventArgs e)
		{
			EditorSceneUtils.Open();
		}

		private void toolStripCreateCube_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddPrimitive(BuiltInMesh.Cube, "Cube");
		}

		private void toolStripCreateCylinder_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddPrimitive(BuiltInMesh.Cylinder, "Cylinder");
		}

		private void toolStripCreateTeapot_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddPrimitive(BuiltInMesh.Teapot, "Teapot");
		}

		private void toolStripCreateSphere_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddPrimitive(BuiltInMesh.Sphere, "Sphere");
		}

		private void toolStripCreateQuad_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddPrimitive(BuiltInMesh.Quad, "Quad");
		}

		private void toolStripCreateLight_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddLight();
		}

		private void toolStripCreateTemplate_Click(object sender, EventArgs e)
		{
			EditorEntityUtils.AddProxy();
		}

		private void toolStripCreateCamera_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddCamera();
		}

		private void toolStripButtonMove_Click(object sender, EventArgs e)
		{
			toolStripButtonScale.Checked = false;
			toolStripButtonMove.Checked = true;
			toolStripButtonRotate.Checked = false;
			mObjectHandler.SetAxisAction(ObjectHandler.HandlerAxisAction.Translate);
			renderCanvas.Refresh();
		}

		private void toolStripButtonScale_Click(object sender, EventArgs e)
		{
			toolStripButtonScale.Checked = true;
			toolStripButtonMove.Checked = false;
			toolStripButtonRotate.Checked = false;
			mObjectHandler.SetAxisAction(ObjectHandler.HandlerAxisAction.Scale);
			renderCanvas.Refresh();
		}

		private void toolStripButtonRotate_Click(object sender, EventArgs e)
		{
			toolStripButtonScale.Checked = false;
			toolStripButtonMove.Checked = false;
			toolStripButtonRotate.Checked = true;
			mObjectHandler.SetAxisAction(ObjectHandler.HandlerAxisAction.Rotate);
			renderCanvas.Refresh();
		}

		private void toolStripButtonImportModel_Click(object sender, EventArgs e)
		{
			if (ProjectManager.Instance.CurrentProjectPath.Length == 0)
			{
				ProjectLoader.Save();
				UpdateFormTitle();
			}
			else
			{
				new ModelImporterWindow().Show();
			}
		}

		private void duplicateSceneObjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.Duplicate();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Selection.ActiveObject != null)
				EditorSceneObjectUtils.Delete(Selection.Selected);
		}

		private void toolStripCreateReflectionProbe_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.AddReflectionProbe();
		}

		private void bakeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			renderCanvas.MakeCurrent();
			mOnyxInstance.Renderer.RefreshReflectionProbes(SceneManagement.ActiveScene);
			renderCanvas.Refresh();
		}

		private void setParentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.SetActiveAsParent();
		}

		private void clearParentToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.ClearParent();
		}

		private void groupObjectsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditorSceneObjectUtils.Group(Selection.Selected);
		}

		private void createEntityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditorEntityUtils.CreateFromSelection();
		}

		private void excludeFromEntityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditorEntityUtils.ExcludeSelection();
		}

		private void saveSceneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			EditorSceneUtils.Save();
		}

		private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SceneManagement.New();
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int newAssets = ProjectManager.Instance.Content.RefreshAssets();
			if (newAssets > 0)
			{
				MessageBox.Show(string.Format("{0} new assets have been imported", newAssets));
			}
		}

		private void revertMaterialsToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DefaultMaterial defaultMat = new DefaultMaterial();
			foreach (OnyxProjectAsset matAsset in ProjectManager.Instance.Content.Materials)
			{
				Material mat = AssetLoader<Material>.Load(matAsset.Path, true);

				foreach (KeyValuePair<string, MaterialProperty> prop in defaultMat.Properties)
				{
					if (!mat.Properties.ContainsKey(prop.Key))
					{
						mat.Properties.Add(prop.Key, prop.Value.Clone());
					}
					else
					{
						mat.Properties[prop.Key].Order = prop.Value.Order;
					}

					// TODO - Check if the property type has changed and update it
				}

				AssetLoader<Material>.Save(mat, matAsset.Path);
                ProjectManager.Instance.Content.MarkDirty(mat.LinkedProjectAsset.Guid);
			}
			mOnyxInstance.Resources.RefreshDirty();
		}

		private void OnEntityEditingChange(object sender, OnHierarchyEntityChange e)
		{
			SetEditingEntity(e.EntityProxy);
		}

		#endregion UI callbacks

		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (SceneManagement.ActiveScene.UnsavedChanges)
			{
				DialogResult result = MessageBox.Show("There are unsaved changes in the scene, do you want to save before closing?", "Unsaved Scene Changes", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					EditorSceneUtils.Save();
				}
			}

			if (ProjectManager.Instance.IsDirty)
			{
				DialogResult result = MessageBox.Show("There are unsaved changes in the project, do you want to save before closing?", "Unsaved Project Changes", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					ProjectLoader.Save();
				}
			}
		}

        private void reloadDefaultShaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Shader shader = mOnyxInstance.Resources.GetShader(BuiltInShader.Default);
            shader.Load("../../../../Onyx3D/Resources/Shaders/VertexShader.glsl", "../../../../Onyx3D/Resources/Shaders/FragmentShader.glsl");
            renderCanvas.Refresh();
        }
    }
}