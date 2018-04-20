
using Assimp;
using Assimp.Configs;
using Onyx3D;
using OpenTK;
using System.IO;
using System.Windows.Forms;

namespace Onyx3DEditor
{
	public partial class ModelImporterWindow : Form
	{
		private string mCurrentPath;
		private Assimp.Scene mCurrentModel;

		public ModelImporterWindow()
		{
			InitializeComponent();

			
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

				// TODO - Check if o3dmod file exists
				// TODO - Parse and show model content

				buttonImport.Enabled = true;
			}
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



			// TODO - Create or update .o3dmod file
		}

		private void ImportMaterialTextures(string directoryPath, Assimp.Material assimpMaterial, Onyx3D.DefaultMaterial onyxMaterial)
		{
			if (assimpMaterial.HasTextureDiffuse)
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.AddTexture(Path.Combine(directoryPath, assimpMaterial.TextureDiffuse.FilePath));
				onyxMaterial.SetAlbedo(asset.Guid);
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
				MaterialLoader.Save(onyxMaterial, newMaterialPath, false);
				ProjectManager.Instance.Content.AddMaterial(newMaterialPath, false, onyxMaterial);
			}
		}

		private void ImportMeshes()
		{
			for(int i=0; i<mCurrentModel.MeshCount; ++i)
			{
				Onyx3D.Mesh onyxMesh = mCurrentModel.Meshes[i].ToOnyx3D();
				ProjectManager.Instance.Content.AddMesh(mCurrentPath, false, onyxMesh);
			}
		}
	}
}


public static class AssimpExtension
{
	public static Vector3 ToOnyx3D(this Vector3D v)
	{
		return new Vector3(v.X, v.Y, v.Z);
	}

	public static Onyx3D.DefaultMaterial ToOnyx3D(this Assimp.Material material)
	{
		Onyx3D.DefaultMaterial newMaterial = new Onyx3D.DefaultMaterial();
		
		// TODO - Load the material;

		return newMaterial;
	}

	public static Onyx3D.Mesh ToOnyx3D(this Assimp.Mesh mesh)
	{
		Onyx3D.Mesh newMesh = new Onyx3D.Mesh();

		newMesh.Indices = mesh.GetIndices();
		for (int vi = 0; vi < mesh.VertexCount; ++vi)
		{
			Vertex newVertex = new Vertex();
			newVertex.Position = mesh.Vertices[vi].ToOnyx3D();

			if (mesh.HasTextureCoords(0))
			{
				Vector3D texCoord = mesh.TextureCoordinateChannels[0][vi];
				newVertex.TexCoord = texCoord.ToOnyx3D().Xy;
			}

			newMesh.Vertices.Add(newVertex);
		}
		return newMesh;
	}
}