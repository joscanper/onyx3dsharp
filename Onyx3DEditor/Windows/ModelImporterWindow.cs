using Assimp;
using Assimp.Configs;
using Onyx3D;
using OpenTK;
using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace Onyx3DEditor
{
	public partial class ModelImporterWindow : Form
	{
        private static readonly float sMeshScalar = 0.005f;
        private static readonly int sNewAssetIcon = 0;
        private static readonly int sUpdateAssetIcon = 1;

        private static readonly float sModelScale = 2f;

		private string mCurrentPath;
		private Assimp.Scene mCurrentModel;
        private int[] mLoadedMeshesGuids;
        private ModelSupportData mSupportFile;
        
        private SceneObject mPreview;
        private PreviewRenderer mPreviewRenderer;

        private bool SupportFileFound { get { return mSupportFile.Guid > 0; } }
        
        // --------------------------------------------------------------------

        public ModelImporterWindow()
		{
			InitializeComponent();
		}
        
        // --------------------------------------------------------------------

        private void ModelImporterWindow_Shown(object sender, EventArgs e)
        {
			if (!DesignMode)
			{
                mPreviewRenderer = new PreviewRenderer();
                mPreviewRenderer.Init(pictureBox1.Width, pictureBox1.Height, this.Handle);
                RenderPreview();
            }
        }

        // --------------------------------------------------------------------

        private void LoadModel(string path)
        {
            textBoxPath.Text = path;
            mCurrentPath = path;
            
            //https://github.com/assimp/assimp-net/blob/master/AssimpNet.Sample/SimpleOpenGLSample.cs

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
            mCurrentModel = importer.ImportFile(path, PostProcessPreset.TargetRealTimeMaximumQuality);
            
            ReloadSupportFile();

            buttonImport.Enabled = true;
            ShowPreview();
        }

        // --------------------------------------------------------------------

        private void ReloadSupportFile()
        {
            string modelPath = mCurrentPath;
            string dataPath = ModelSupportData.GetPath(modelPath);
            
            if (File.Exists(dataPath))
            {
                mSupportFile = AssetStreamLoader<ModelSupportData>.Load(dataPath, false);
                
            }
            else
            {
                mSupportFile = new ModelSupportData(ProjectContent.GetRelativePath(modelPath));
            }
        }

        // --------------------------------------------------------------------

        private void ShowPreview()
        {
            if (mPreview != null)
            {
                mPreview.RemoveAllChildren();
                mPreview.RemoveAllComponents();
            }

            mPreview = ParseNode(mCurrentModel.RootNode, true, null);
			UpdatePreviewObject();
		}

        // --------------------------------------------------------------------

        private void UpdatePreviewObject()
		{
			Bounds bounds = mPreview.CalculateBounds();
			float scale = sModelScale / bounds.Size.Length;
			mPreview.Transform.LocalScale = Vector3.One * scale;
			mPreview.Parent = mPreviewRenderer.Scene.Root;

            SetTreeViewModel(mPreview);
            RenderPreview();
        }

        // --------------------------------------------------------------------

        private void RenderPreview()
        {
            mPreviewRenderer.Render();
            pictureBox1.Image = mPreviewRenderer.AsBitmap();
        }

        // --------------------------------------------------------------------

        private SceneObject ParseNode(Assimp.Node node, bool preview, SceneObject parent)
        {
            string nodeName = GetSafeFileName(node.Name);
            SceneObject mySceneObject = new SceneObject(nodeName);
            mySceneObject.Parent = parent;
            mySceneObject.Transform.FromMatrix(node.Transform.ToOnyx3D());
            if (!preview)
                mySceneObject.Transform.LocalPosition = mySceneObject.Transform.LocalPosition * sMeshScalar;
            

            if (node.HasMeshes)
            {
                for(int i=0; i < node.MeshCount; ++i)
                { 
                    MeshRenderer meshRenderer = mySceneObject.AddComponent<MeshRenderer>();
                    if (preview)
                    {
						mCurrentModel.Meshes[node.MeshIndices[i]].Name = nodeName;
						meshRenderer.Mesh = mCurrentModel.Meshes[node.MeshIndices[i]].ToOnyx3D();
                        meshRenderer.Material = new DefaultMaterial();
                    }else
                    {
                        meshRenderer.Mesh = Onyx3DEngine.Instance.Resources.GetMesh(mLoadedMeshesGuids[node.MeshIndices[i]]);
                        meshRenderer.Material = Onyx3DEngine.Instance.Resources.GetMaterial(BuiltInMaterial.Default);
                    }
                }
            }

            foreach (Node child in node.Children)
            {
                SceneObject childSceneObject = ParseNode(child, preview, mySceneObject);
            }

            return mySceneObject;
        }

        // --------------------------------------------------------------------

        private void CreateEntity()
        {
            NewEntityWindow window = new NewEntityWindow();
            if (window.ShowDialog() == DialogResult.OK)
            {
                SceneObject root = ParseNode(mCurrentModel.RootNode, false, null);
                OnyxProjectAsset entity = ProjectManager.Instance.Content.GetEntityByName(window.EntityName);
                if (entity == null)
                    EditorEntityUtils.Create(root, window.EntityName);
                else
                    AssetLoader<Entity>.Save(new Entity(root), entity.Path);
            }
		}
        
        // --------------------------------------------------------------------

        private void ImportMeshes()
		{
            mLoadedMeshesGuids = new int[mCurrentModel.MeshCount];
            
            List<ModelSupportData.MeshData> previousMeshes = new List<ModelSupportData.MeshData>(mSupportFile.Meshes);
            mSupportFile.Meshes.Clear();

            for (int i=0; i < mCurrentModel.MeshCount; ++i)
			{
                mLoadedMeshesGuids[i] = ImportMesh(mCurrentModel.Meshes[i], previousMeshes);
            }

            foreach (ModelSupportData.MeshData meshData in previousMeshes)
            {
                if (mSupportFile.GetMeshId(meshData.Name) < 0)
                {
                    if (MessageBox.Show("Mesh " + meshData.Name + " is no longer part of the new model. Do you want to keep it in the project?", "Mesh Disappeared", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        ProjectManager.Instance.Content.RemoveMesh(meshData.Id);
                    }
                }
            }
		}

        // --------------------------------------------------------------------

        private int ImportMesh(Assimp.Mesh newMesh, List<ModelSupportData.MeshData> previousMeshes)
        {
            Onyx3D.Mesh onyxMesh = newMesh.ToOnyx3D();
            onyxMesh.Scale(sMeshScalar);
            string name = GetSafeFileName(newMesh.Name);
            string meshPath = ProjectContent.GetMeshPath(name);
            AssetLoader<Onyx3D.Mesh>.Save(onyxMesh, meshPath, false);

            int id = -1;
            for (int prevIndex = 0; prevIndex < previousMeshes.Count; ++prevIndex)
            {
                ModelSupportData.MeshData mesh = previousMeshes[prevIndex];
                if (mesh.Name == name)
                {
                    id = mesh.Id;
                    previousMeshes.Remove(mesh);
                    break;
                }
            }

            if (id < 0)
            {
                id = ProjectManager.Instance.Content.AddMesh(meshPath, false, onyxMesh).Guid;
            }
            else
            {
                if (ProjectManager.Instance.Content.GetAsset(id) == null)
                {
                    ProjectManager.Instance.Content.AddObject(meshPath, false, ProjectManager.Instance.Content.Meshes, id, onyxMesh);
                }
            }


            ModelSupportData.MeshData data = new ModelSupportData.MeshData();
            data.Id = id;
            data.Name = name;
            mSupportFile.Meshes.Add(data);

            onyxMesh.LinkedProjectAsset.Dirty = true;

            return id;
        }

        // --------------------------------------------------------------------

        private string GetSafeFileName(string filename)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            // Builds a string out of valid chars
            Array.ForEach(invalidFileNameChars,
                c => filename = filename.Replace(c.ToString(), "_"));

            return filename;
        }
        // --------------------------------------------------------------------

        private void SetTreeViewModel(SceneObject obj)
        {
            modelTreeView.Nodes.Clear();
            AddSceneObjectToTreeNode(null, obj);
            modelTreeView.ExpandAll();
        }

        // --------------------------------------------------------------------

        private void AddSceneObjectToTreeNode(TreeNode node, SceneObject sceneObject)
        {
            SceneTreeNode objectNode = new SceneTreeNode(sceneObject);
            
            if (node == null)
            {
                objectNode.ImageIndex = SupportFileFound ? sUpdateAssetIcon : sNewAssetIcon;
                modelTreeView.Nodes.Add(objectNode);
            }
            else
            {
                if (SupportFileFound)
                {
                    objectNode.ImageIndex = mSupportFile.GetMeshId(sceneObject.Id) > 0 ? sUpdateAssetIcon : sNewAssetIcon;
                }
                else
                {
                    objectNode.ImageIndex = sNewAssetIcon;
                }
                node.Nodes.Add(objectNode);
            }

            objectNode.SelectedImageIndex = objectNode.ImageIndex;

            sceneObject.ForEachChild((c) =>
            {
                AddSceneObjectToTreeNode(objectNode, c);
            });
        }

        // --------------------------------------------------------------------

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            mPreviewRenderer.Dispose();
        }

        // --------------------------------------------------------------------

        #region UI callbacks

        private void buttonImport_Click(object sender, System.EventArgs e)
        {
        
            if (mCurrentModel == null)
                return;

            if (mCurrentModel.HasMeshes)
                ImportMeshes();

            if (!SupportFileFound)
            {
                OnyxProjectAsset asset = ProjectManager.Instance.Content.AddModel(mSupportFile.FilePath, true, mSupportFile);
                mSupportFile.Guid = asset.Guid;
            }

            AssetStreamLoader<ModelSupportData>.Save(mSupportFile, mSupportFile.FilePath);

            if (MessageBox.Show("Do you want to create an entity from the imported mesh?", "Create Entity", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CreateEntity();
            }

            ProjectLoader.Save();

            //LoadModel(mCurrentPath);
        }
        // --------------------------------------------------------------------

        private void buttonOpen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                LoadModel(openDialog.FileName);
            }
        }
        
        #endregion

    }
}
