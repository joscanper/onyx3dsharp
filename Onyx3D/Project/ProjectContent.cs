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
		
		public List<OnyxProjectAsset> Scenes = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Textures = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Materials = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Meshes = new List<OnyxProjectAsset>();
        public List<OnyxProjectAsset> Models = new List<OnyxProjectAsset>();
        public List<OnyxProjectAsset> Entities = new List<OnyxProjectAsset>();

        [XmlIgnore]
		public Dictionary<int, OnyxProjectAsset> mMappedResources = new Dictionary<int, OnyxProjectAsset>();

        // --------------------------------------------------------------------

        public void Init()
		{

			DefaultXMLSettings.NewLineOnAttributes = true;
			DefaultXMLSettings.Indent = true;

			// Built-in meshes (from 100000000)
			AddAsset(new OnyxProjectAsset("./Resources/Meshes/teapot.o3dmesh", "Teapot", BuiltInMesh.Teapot));
			AddAsset(new OnyxProjectAsset("./Resources/Meshes/cube.o3dmesh", "Cube", BuiltInMesh.Cube));
			AddAsset(new OnyxProjectAsset("./Resources/Meshes/cylinder.o3dmesh", "Cylinder", BuiltInMesh.Cylinder));
			AddAsset(new OnyxProjectAsset("./Resources/Meshes/torus.o3dmesh", "Torus", BuiltInMesh.Torus));
			AddAsset(new OnyxProjectAsset("./Resources/Meshes/sphere.o3dmesh", "Sphere", BuiltInMesh.Sphere));
            AddAsset(new OnyxProjectAsset("./Resources/Meshes/quad.o3dmesh", "Quad", BuiltInMesh.Quad));
            AddAsset(new OnyxProjectAsset("./Resources/Meshes/gizmo_camera.o3dmesh", "Quad", BuiltInMesh.GizmoCamera));

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
            AddAssets(Models);
        }

        // --------------------------------------------------------------------

        public void AddAssets<T>(List<T> assets) where T : OnyxProjectAsset
		{
			foreach (T asset in assets)
				AddAsset(asset);
		}

        // --------------------------------------------------------------------

        public void AddAsset(OnyxProjectAsset asset)
		{
			mMappedResources.Add(asset.Guid, asset);
		}

        // --------------------------------------------------------------------

        public OnyxProjectAsset GetAsset(int id)
		{
            if (!mMappedResources.ContainsKey(id))
                return null;
			return mMappedResources[id];
		}

        // --------------------------------------------------------------------

        public OnyxProjectAsset GetInitScene()
		{
			return Scenes.Count == 0 ? null : Scenes[0];
		}

        // --------------------------------------------------------------------

        public OnyxProjectAsset GetEntityByName(string name)
        {
            foreach (OnyxProjectAsset t in ProjectManager.Instance.Content.Entities)
                if (t.Name == name)
                    return t;

            return null;
        }

        // --------------------------------------------------------------------

        public static void CreateProjectHierarchy(string path)
        {
            Directory.CreateDirectory(path + "/Assets");
            Directory.CreateDirectory(path + "/Assets/Textures");
            Directory.CreateDirectory(path + "/Assets/Models");
            Directory.CreateDirectory(path + "/Assets/Models/Meshes");
            Directory.CreateDirectory(path + "/Assets/Scenes");
            Directory.CreateDirectory(path + "/Assets/Entities");
            Directory.CreateDirectory(path + "/Assets/Materials");
        }

        // --------------------------------------------------------------------

        public int RefreshAssets()
        {
            int n = 0;
            n += RefreshAssetsFromFolder(GetTexturesPath(), Textures, "*.png", ContentIds.Textures);
            return n;
        }

        // --------------------------------------------------------------------

        public int RefreshAssetsFromFolder<T>(string folderAbsolute, List<T> list, string format, int initID) where T:OnyxProjectAsset
        {
            Dictionary<string, OnyxProjectAsset> hashed = GetHashedByPath(list);
            string folderPath = GetRelativePath(folderAbsolute);
            DirectoryInfo d = new DirectoryInfo(folderAbsolute);
            FileInfo[] Files = d.GetFiles(format);

            int n = 0;
            foreach (FileInfo file in Files)
            {
                string filePath = string.Format("{0}{1}", folderPath, file.Name);
                if (!hashed.ContainsKey(filePath))
                {
                    T newItem = (T)new OnyxProjectAsset(filePath, Path.GetFileNameWithoutExtension(file.Name), GetNewId(list, initID));
                    list.Add(newItem);
                    ++n;
                }
            }

            if (n > 0)
                AddAssets(list);

            return n;
        }

        private Dictionary<string, OnyxProjectAsset> GetHashedByPath<T>(List<T> list) where T:OnyxProjectAsset
        {
            Dictionary<string, OnyxProjectAsset> hash = new Dictionary<string, OnyxProjectAsset>();
            foreach(T item in list)
                hash.Add(item.Path, item);
            return hash;
        }

        // --------------------------------------------------------------------
        // ---- Add Utils
        // --------------------------------------------------------------------

        public OnyxProjectAsset AddScene(string path, bool relative = false, Scene scene = null)
        {
            return AddObject(path, relative, Scenes, GetNewId(Scenes, ContentIds.Scenes), scene);
        }

        // --------------------------------------------------------------------

        public OnyxProjectAsset AddMaterial(string path, bool relative = false, Material mat = null)
		{
            return AddObject(path, relative, Materials, GetNewId(Materials, ContentIds.Materials), mat);
        }

        // --------------------------------------------------------------------

        public OnyxProjectAsset AddTexture(string path, bool relative = false, Texture texture = null)
        {
            return AddObject(path, relative, Textures, GetNewId(Textures, ContentIds.Textures), texture);
        }

        // --------------------------------------------------------------------

        public OnyxProjectAsset AddMesh(string path, bool relative = false, Mesh mesh = null)
		{
            return AddObject(path, relative, Meshes, GetNewId(Meshes, ContentIds.Meshes), mesh);
        }

        // --------------------------------------------------------------------

        public OnyxProjectAsset AddEntity(string path, bool relative = false, Entity entity = null)
        {
            return AddObject(path, relative, Entities, GetNewId(Entities, ContentIds.Entities), entity);
        }

        // --------------------------------------------------------------------

        public OnyxProjectAsset AddModel(string path, bool relative = false, ModelSupportData model = null)
        {
            return AddObject(path, relative, Models, GetNewId(Models, ContentIds.Models), model);
        }

        // --------------------------------------------------------------------

        public TAsset AddObject<TObj, TAsset>(string path, bool relative, List<TAsset> list, int id, TObj obj) where TObj : GameAsset where TAsset : OnyxProjectAsset
        {
            TAsset asset = (TAsset)Activator.CreateInstance(typeof(TAsset), relative ? path : GetRelativePath(path), Path.GetFileNameWithoutExtension(path), id);
            if (obj != null)
                obj.LinkedProjectAsset = asset;
            list.Add(asset);
            AddAsset(asset);
            return asset;
        }


        // --------------------------------------------------------------------
        // ---- Remove Utils
        // --------------------------------------------------------------------

        public bool RemoveMesh(int guid)
        {
            return RemoveObject(guid, Meshes);
        }
        
        // --------------------------------------------------------------------

        private bool RemoveObject<TAsset>(int guid, List<TAsset> list) where TAsset : OnyxProjectAsset
        {
            int removed = list.RemoveAll(asset => asset.Guid == guid);
            mMappedResources.Remove(guid);
            return removed > 0;
        }

        // --------------------------------------------------------------------
        // ----- Path Utils
        // --------------------------------------------------------------------

        public static string GetMeshPath(string meshName)
        {
            return string.Format("{0}{1}\\{2}{3}", GetModelsPath(), "Meshes", meshName, ".o3dmesh");
        }

        public static string GetEntityPath(string entityName)
		{
			return string.Format("{0}{1}{2}", GetEntitiesPath(), entityName, ".o3dent");
		}

		public static string GetAbsolutePath(string relativePath)
        {

            if (relativePath.StartsWith("./"))
                return relativePath;

            return string.Format("{0}\\{1}", ProjectManager.Instance.Directory, relativePath);
        }

        public static string GetModelsPath()
        {
            return GetProjectAssetPath("Models");
        }

        public static string GetTexturesPath()
        {
            return GetProjectAssetPath("Textures");
        }

        public static string GetEntitiesPath()
        {
            return GetProjectAssetPath("Entities");
        }


        public static string GetProjectAssetPath(string folder)
        {
            return string.Format("{0}\\Assets\\{1}\\", ProjectManager.Instance.Directory, folder);
        }
        // --------------------------------------------------------------------

        public static string GetRelativePath(string absolutePath)
        {
            Uri projectUri = new Uri(ProjectManager.Instance.CurrentProjectPath);
            Uri assetUri = new Uri(absolutePath);

            return string.Format("{0}",projectUri.MakeRelativeUri(assetUri).ToString());
        }

        // --------------------------------------------------------------------

        private int GetNewId<T>(List<T> list, int start) where T : OnyxProjectAsset
        {
            if (list.Count == 0)
                return start;
            return list[list.Count - 1].Guid + 1;
        }
	}
}
