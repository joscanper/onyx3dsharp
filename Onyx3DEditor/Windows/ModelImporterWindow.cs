
using Assimp;
using Assimp.Configs;
using Onyx3D;
using OpenTK;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Onyx3DEditor
{
	public partial class ModelImporterWindow : Form
	{
        private static readonly float ModelScale = 2;

		private string mCurrentPath;
		private Assimp.Scene mCurrentModel;
        private int[] mLoadedMeshes;
        private ModelSupportData mSupportFile;
        private SceneObject mPreview;

        public ModelImporterWindow()
		{
			InitializeComponent();
		}

        private void ModelImporterWindow_Shown(object sender, System.EventArgs e)
        {
			if (!DesignMode)
			{
				onyx3DControl.Init();
				onyx3DControl.DrawGrid = true;
			}
        }

        private void buttonOpen_Click(object sender, System.EventArgs e)
		{
		    	
			OpenFileDialog openDialog = new OpenFileDialog();
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxPath.Text = openDialog.FileName;
				mCurrentPath = openDialog.FileName;

				//https://github.com/assimp/assimp-net/blob/master/AssimpNet.Sample/SimpleOpenGLSample.cs
				
				AssimpContext importer = new AssimpContext();
				importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
				mCurrentModel = importer.ImportFile(openDialog.FileName, PostProcessPreset.TargetRealTimeMaximumQuality);

                // TODO - Check if o3dmod file exists and load it 
                mSupportFile = new ModelSupportData(ProjectContent.GetRelativePath(mCurrentPath));

                // TODO - Parse and show model content with overwritting options
                labelMeshes.Text = mCurrentModel.MeshCount + " Meshes";
                labelMaterials.Text = mCurrentModel.MaterialCount + " Materials";
                labelTextures.Text = mCurrentModel.TextureCount + " Textures";
                
                buttonImport.Enabled = true;

                ShowPreview();
            }
		}

        private void ShowPreview()
        {
            if (mPreview != null)
            {
                mPreview.RemoveAllChildren();
                mPreview.RemoveAllComponents();
            }

            mPreview = ParseNode(mCurrentModel.RootNode, true);
            Bounds bounds = mPreview.CalculateBoundingBox();
            float scale = ModelScale / bounds.Size.Length;
            mPreview.Transform.LocalScale = Vector3.One * scale;
            mPreview.Parent = onyx3DControl.Scene.Root;

			sceneHierarchyControl1.SetObject(mPreview);

			onyx3DControl.Refresh();
		}

		private void buttonImport_Click(object sender, System.EventArgs e)
		{
			if (mCurrentModel == null)
				return;

			if (mCurrentModel.HasMeshes)
				ImportMeshes();

			//if (mCurrentModel.HasTextures)
			//	ImportTextures();

			if (mCurrentModel.HasMaterials)
				ImportMaterials();


            ModelSupportDataLoader.Save(mSupportFile);

            ImportTemplate();

			ProjectLoader.Save();
        }

        
        private SceneObject ParseNode(Assimp.Node node, bool preview)
        {
            
            SceneObject mySceneObject = new SceneObject(node.Name);
            mySceneObject.Transform.FromMatrix(node.Transform.ToOnyx3D());

            if (node.HasMeshes)
            {
                for(int i=0; i < node.MeshCount; ++i)
                { 
                    MeshRenderer meshRenderer = mySceneObject.AddComponent<MeshRenderer>();
                    if (preview)
                    {
						mCurrentModel.Meshes[node.MeshIndices[i]].Name = node.Name;
						meshRenderer.Mesh = mCurrentModel.Meshes[node.MeshIndices[i]].ToOnyx3D();
                        meshRenderer.Material = new DefaultMaterial();
                    }else
                    {
                        meshRenderer.Mesh = onyx3DControl.OnyxInstance.Resources.GetMesh(mLoadedMeshes[node.MeshIndices[i]]);
                        meshRenderer.Material = onyx3DControl.OnyxInstance.Resources.GetMaterial(BuiltInMaterial.Default);
                    }
                }
            }

            foreach (Assimp.Node child in node.Children)
            {
                SceneObject childSceneObject = ParseNode(child, preview);
                childSceneObject.Parent = mySceneObject;
            }

            return mySceneObject;
        }


        private void ImportTemplate()
        {
            SceneObject root = ParseNode(mCurrentModel.RootNode, false);

            string templatePath = Path.Combine(Path.GetDirectoryName(mSupportFile.AbsolutePath), Path.GetFileNameWithoutExtension(mSupportFile.ModelFile) + ".o3dtemp");
            Template template = new Template(root);
            TemplateLoader.Save(template, templatePath);

            OnyxProjectAsset asset =  ProjectManager.Instance.Content.AddTemplate(templatePath, false, template);

            labelTemplate.Text = "\n Imported Template : " + asset.Guid;
        }

        private void ImportMaterialTextures(string directoryPath, Assimp.Material assimpMaterial, Onyx3D.DefaultMaterial onyxMaterial)
		{
			if (assimpMaterial.HasTextureDiffuse)
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.AddTexture(Path.Combine(directoryPath, assimpMaterial.TextureDiffuse.FilePath));
				onyxMaterial.SetAlbedo(asset.Guid);
                
                // TODO - Save in model support

                labelTextures.Text += "\n Imported : " + asset.Guid;
            }
			
			/*
			if (assimpMaterial.HasTextureNormal)
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.AddTexture(Path.Combine(directoryPath, assimpMaterial.TextureNormal.FilePath));
				onyxMaterial.SetNormal(asset.Guid);
			}
			*/
		}

		private void ImportMaterials()
		{
			string directoryPath = Path.GetDirectoryName(mCurrentPath);
			for (int i = 0; i < mCurrentModel.MaterialCount; ++i)
			{
				
				Onyx3D.DefaultMaterial onyxMaterial = mCurrentModel.Materials[i].ToOnyx3D();

				ImportMaterialTextures(directoryPath, mCurrentModel.Materials[i], onyxMaterial);

				string newMaterialPath = Path.Combine(directoryPath, mCurrentModel.Materials[i].Name + ".o3mat");
				AssetLoader<Onyx3D.Material>.Save(onyxMaterial, newMaterialPath, false);
				OnyxProjectAsset asset = ProjectManager.Instance.Content.AddMaterial(newMaterialPath, false, onyxMaterial);

                // TODO - Check asset operation
                mSupportFile.Materials.Add(asset.Guid);

                labelMaterials.Text += "\n Imported : " + onyxMaterial.LinkedProjectAsset.Guid;
            }
		}

		private void ImportMeshes()
		{
            mLoadedMeshes = new int[mCurrentModel.MeshCount];

            for (int i=0; i<mCurrentModel.MeshCount; ++i)
			{
				Onyx3D.Mesh onyxMesh = mCurrentModel.Meshes[i].ToOnyx3D();
				
				string meshPath = Path.GetDirectoryName(mSupportFile.FilePath) + "/" + mCurrentModel.Meshes[i].Name + ".o3dmesh";
				AssetLoader<Onyx3D.Mesh>.Save(onyxMesh, meshPath, true);
				OnyxProjectAsset asset = ProjectManager.Instance.Content.AddMesh(meshPath, true, onyxMesh);
                mLoadedMeshes[i] = asset.Guid;

                // TODO - Check asset operation
                mSupportFile.Meshes.Add(asset.Guid);

                labelMeshes.Text += "\n Imported : " + onyxMesh.LinkedProjectAsset.Guid;
            }
		}

    }
}
