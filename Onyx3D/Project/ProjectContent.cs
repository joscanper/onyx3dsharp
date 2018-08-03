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
		public List<OnyxProjectMeshAsset> Meshes = new List<OnyxProjectMeshAsset>();
        public List<OnyxProjectAsset> Entities = new List<OnyxProjectAsset>();

        [XmlIgnore]
		public Dictionary<int, OnyxProjectAsset> mMappedResources = new Dictionary<int, OnyxProjectAsset>();

		public void Init()
		{

			DefaultXMLSettings.NewLineOnAttributes = true;
			DefaultXMLSettings.Indent = true;

			// Built-in meshes (from 100000000)
			AddAsset(new OnyxProjectMeshAsset("./Resources/Models/teapot.obj", "Teapot", BuiltInMesh.Teapot, true));
			AddAsset(new OnyxProjectMeshAsset("./Resources/Models/cube.obj", "Cube", BuiltInMesh.Cube, true));
			AddAsset(new OnyxProjectMeshAsset("./Resources/Models/cylinder.obj", "Cylinder", BuiltInMesh.Cylinder, true));
			AddAsset(new OnyxProjectMeshAsset("./Resources/Models/torus.obj", "Torus", BuiltInMesh.Torus, true));
			AddAsset(new OnyxProjectMeshAsset("./Resources/Models/sphere.obj", "Sphere", BuiltInMesh.Sphere, true));
            AddAsset(new OnyxProjectMeshAsset("./Resources/Models/quad.obj", "Quad", BuiltInMesh.Quad, true));
            AddAsset(new OnyxProjectMeshAsset("./Resources/Models/camera_gizmo.obj", "Quad", BuiltInMesh.GizmoCamera, true));

            //  Built-in textures (from 200000000)
            AddAsset(new OnyxProjectAsset("./Resources/Textures/checker.png", "Checker", BuiltInTexture.Checker));
            AddAsset(new OnyxProjectAsset("./Resources/Textures/white.jpg", "White",BuiltInTexture.White));
            AddAsset(new OnyxProjectAsset("./Resources/Textures/black.jpg", "Black", BuiltInTexture.Black));
			AddAsset(new OnyxProjectAsset("./Resources/Textures/normal.jpg", "Normal", BuiltInTexture.Normal));
			AddAsset(new OnyxProjectAsset("./Resources/Textures/brdf_lut.png", "BRDFLut", BuiltInTexture.BRDFLut));
			

			// Built-in shaders (from 300000000)
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl", BuiltInShader.Default));
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitFragmentShader.glsl", BuiltInShader.Unlit));
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitVertexColorFragmentShader.glsl", BuiltInShader.UnlitVertexColor));
			AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/ReflectionProbeFragmentShader.glsl", BuiltInShader.ReflectionProbe));
            AddAsset(new OnyxProjectShaderAsset("./Resources/Shaders/SkyVertexShader.glsl", "./Resources/Shaders/SkyFragmentShader.glsl", BuiltInShader.ProceduralSky));

            // Built-in materials (from 400000000)
            AddAsset(new OnyxProjectAsset("./Resources/Materials/NotFound.o3dmat", "NotFound", BuiltInMaterial.NotFound));
            AddAsset(new OnyxProjectAsset("./Resources/Materials/Default.o3dmat", "Default", BuiltInMaterial.Default));
			AddAsset(new OnyxProjectAsset("./Resources/Materials/Unlit.o3dmat", "Unlit", BuiltInMaterial.Unlit));
			AddAsset(new OnyxProjectAsset("./Resources/Materials/UnlitVertexColor.o3dmat", "UnlitVertexColor", BuiltInMaterial.UnlitVertexColor));
			AddAsset(new OnyxProjectAsset("./Resources/Materials/ReflectionProbe.o3dmat", "ReflectionProbe", BuiltInMaterial.ReflectionProbe));
            AddAsset(new OnyxProjectAsset("./Resources/Materials/Sky.o3dmat", "Sky", BuiltInMaterial.Sky));

            AddAssets(Scenes);
			AddAssets(Materials);
			AddAssets(Textures);
			AddAssets(Meshes);
            AddAssets(Entities);
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
            if (!mMappedResources.ContainsKey(id))
                return null;
			return mMappedResources[id];
		}

		public OnyxProjectSceneAsset GetInitScene()
		{
			return Scenes.Count == 0 ? null : Scenes[0];
		}

        public OnyxProjectSceneAsset AddScene(string path, bool relative = false, Scene scene = null)
        {
            OnyxProjectSceneAsset asset = new OnyxProjectSceneAsset(relative ? path : GetRelativePath(path), Path.GetFileNameWithoutExtension(path), GetNewSceneId());
            if (scene != null)
                scene.LinkedProjectAsset = asset;
            Scenes.Add(asset);
            return asset;
        }

		public OnyxProjectMaterialAsset AddMaterial(string path, bool relative = false, Material mat = null)
		{
			OnyxProjectMaterialAsset matAsset = new OnyxProjectMaterialAsset(relative ? path : GetRelativePath(path), Path.GetFileNameWithoutExtension(path), GetNewMaterialId());
            if (mat!= null)
			    mat.LinkedProjectAsset = matAsset;
			Materials.Add(matAsset);
			AddAsset(matAsset);
            return matAsset;
		}

        public OnyxProjectAsset AddTexture(string path, bool relative = false, Texture texture = null)
        {
            OnyxProjectAsset textureAsset = new OnyxProjectAsset(relative ? path : GetRelativePath(path), Path.GetFileNameWithoutExtension(path), GetNewTextureId());
            if (texture != null)
                texture.LinkedProjectAsset = textureAsset;
            Textures.Add(textureAsset);
            AddAsset(textureAsset);
            return textureAsset;
        }

		public OnyxProjectMeshAsset AddMesh(string path, bool relative = false, Mesh mesh = null)
		{
            OnyxProjectMeshAsset meshAsset = new OnyxProjectMeshAsset(relative ? path : GetRelativePath(path), Path.GetFileNameWithoutExtension(path), GetNewMeshId());
			if (mesh != null)
				mesh.LinkedProjectAsset = meshAsset;
			Meshes.Add(meshAsset);
			AddAsset(meshAsset);
			return meshAsset;
		}

        public OnyxProjectAsset AddTemplate(string path, bool relative = false, Entity tmp = null)
        {
            OnyxProjectAsset entityAsset = new OnyxProjectAsset(relative ? path : GetRelativePath(path), Path.GetFileNameWithoutExtension(path), GetNewEntityId());
            if (tmp != null)
                tmp.LinkedProjectAsset = entityAsset;
            Entities.Add(entityAsset);
            AddAsset(entityAsset);
            return entityAsset;
        }
        
        // -----

        public static string GetEntityPath(string entityName)
		{
			return string.Format("{0}\\{1}\\{2}{3}", ProjectManager.Instance.Directory, "Entities", entityName, ".o3dent");
		}

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
			return GetNewId(Materials, ContentIds.Materials);
		}

		private int GetNewMeshId()
		{
			return GetNewId(Meshes, ContentIds.Meshes);
		}

		private int GetNewSceneId()
		{
			return GetNewId(Scenes, ContentIds.Scenes);
        }

		private int GetNewTextureId()
		{
			return GetNewId(Textures, ContentIds.Textures);
        }

        private int GetNewEntityId()
        {
            return GetNewId(Entities, ContentIds.Entities);
        }


        private int GetNewId<T>(List<T> list, int start) where T : OnyxProjectAsset
        {
            if (list.Count == 0)
                return start;
            return list[list.Count - 1].Guid + 1;
        }
	}
}
