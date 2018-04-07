using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Onyx3D
{


	[Serializable]
	public class ProjectContent
	{

		public static XmlWriterSettings DefaultXMLSettings = new XmlWriterSettings();
		

		
		public List<OnyxProjectSceneAsset> Scenes = new List<OnyxProjectSceneAsset>();
		public List<OnyxProjectAsset> Textures = new List<OnyxProjectAsset>();
		public List<OnyxProjectMaterialAsset> Materials = new List<OnyxProjectMaterialAsset>();
		public List<OnyxProjectAsset> Meshes = new List<OnyxProjectAsset>();

		[XmlIgnore]
		public Dictionary<int, OnyxProjectAsset> mMappedResources = new Dictionary<int, OnyxProjectAsset>();

		public void Init()
		{

			DefaultXMLSettings.NewLineOnAttributes = true;
			DefaultXMLSettings.Indent = true;

			// Built-in meshes (from 100000000)
			AddAsset(new OnyxProjectAsset("./Resources/Models/teapot.obj", BuiltInMesh.Teapot));
			AddAsset(new OnyxProjectAsset("./Resources/Models/cube.obj", BuiltInMesh.Cube));
			AddAsset(new OnyxProjectAsset("./Resources/Models/cylinder.obj", BuiltInMesh.Cylinder));
			AddAsset(new OnyxProjectAsset("./Resources/Models/torus.obj", BuiltInMesh.Torus));
			AddAsset(new OnyxProjectAsset("./Resources/Models/sphere.obj", BuiltInMesh.Sphere));
            AddAsset(new OnyxProjectAsset("./Resources/Models/quad.obj", BuiltInMesh.Quad));

            //  Built-in textures (from 200000000)
            AddAsset(new OnyxProjectAsset("./Resources/Textures/checker.png", BuiltInTexture.Checker));

			// Built-in shaders (from 300000000)
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl", BuiltInShader.Default));
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitFragmentShader.glsl", BuiltInShader.Unlit));
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitVertexColorFragmentShader.glsl", BuiltInShader.UnlitVertexColor));

			// Built-in materials (from 400000000)
			AddAsset(new OnyxProjectAsset("./Resources/Materials/Default.o3dmat", BuiltInMaterial.Default));
			AddAsset(new OnyxProjectAsset("./Resources/Materials/Unlit.o3dmat", BuiltInMaterial.Unlit));
			AddAsset(new OnyxProjectAsset("./Resources/Materials/UnlitVertexColor.o3dmat", BuiltInMaterial.UnlitVertexColor));

			AddAssets(Scenes);
			AddAssets(Materials);
			AddAssets(Textures);
			AddAssets(Meshes);
		}


		public void AddAssets<T>(List<T> assets) where T : OnyxProjectAsset
		{
			foreach (T asset in assets)
				AddAsset(asset);
		}


		public void AddAsset(OnyxProjectAsset asset)
		{
			mMappedResources.Add(asset.Guid, asset);
		}

		public OnyxProjectAsset GetAsset(int id)
		{
			return mMappedResources[id];
		}

		public OnyxProjectSceneAsset GetInitScene()
		{
			return Scenes.Count == 0 ? null : Scenes[0];
		}

		public void AddMaterial(Material m, string path)
		{
			OnyxProjectMaterialAsset matAsset = new OnyxProjectMaterialAsset(GetRelativePath(path), Path.GetFileNameWithoutExtension(path), GetNewMaterialId());
			m.LinkedProjectAsset = matAsset;
			Materials.Add(matAsset);
			AddAsset(matAsset);
		}

        // -----


        public static string GetAbsolutePath(string relativePath)
        {

            if (relativePath.StartsWith("./"))
                return relativePath;

            return string.Format("{0}\\{1}", ProjectManager.Instance.Directory, relativePath);
            
        }


        public static string GetRelativePath(string absolutePath)
        {
            Uri projectUri = new Uri(ProjectManager.Instance.CurrentProjectPath);
            Uri assetUri = new Uri(absolutePath);

            return projectUri.MakeRelativeUri(assetUri).ToString();
        }

        // -----

        private int GetNewMaterialId()
		{
			return ContentIds.Materials + Materials.Count;
		}

		private int GetNewMeshId()
		{
			return ContentIds.Meshes + Meshes.Count;
		}

		private int GetNewSceneId()
		{
			return ContentIds.Scenes + Scenes.Count;
		}

		private int GetNewTextureId()
		{
			return ContentIds.Textures + Textures.Count;
		}
	}
}
