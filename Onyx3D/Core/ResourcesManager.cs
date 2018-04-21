using Assimp;
using Assimp.Configs;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{

	public class ResourcesManager : EngineComponent
	{
		private Dictionary<int, Mesh> mMeshes = new Dictionary<int, Mesh>();
		private Dictionary<int, Texture> mTextures = new Dictionary<int, Texture>();
		private Dictionary<int, Material> mMaterials = new Dictionary<int, Material>();
		private Dictionary<int, Shader> mShaders = new Dictionary<int, Shader>();

		// ----------------------------------------------------------------  Getters

       
		private T GetResource<T>(int id, Dictionary<int, T> map, Func<OnyxProjectAsset, T> loadFallback, int defaultAsset) where T : GameAsset
		{

			if (!map.ContainsKey(id))
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.GetAsset(id);
                if (asset == null)
                    return GetResource(defaultAsset, map, loadFallback, 0);

                map[id] = loadFallback(asset);
				map[id].LinkedProjectAsset = asset; 
			}

			return map[id];
		}

        private void ReloadResource<T>(int id, Dictionary<int, T> map, Func<OnyxProjectAsset, T> loadFallback) where T : GameAsset
        {
            OnyxProjectAsset asset = GetResource(id, map, loadFallback, 0).LinkedProjectAsset;
            T newAsset = loadFallback(asset);
            map[id].Copy(newAsset);
        }

        public Mesh GetMesh(int id)
		{
			return GetResource(id, mMeshes, LoadMesh, BuiltInMesh.Cube);
		}

		public Material GetMaterial(int id)
		{
			return GetResource(id, mMaterials, LoadMaterial, BuiltInMaterial.NotFound);
		}
		
		public Texture GetTexture(int id)
		{
			return GetResource(id, mTextures, LoadTexture, BuiltInTexture.Checker);
		}

		public Shader GetShader(int id)
		{
			return GetResource(id, mShaders, LoadShader, BuiltInShader.Default);
		}

        public void ReloadMaterial(int id)
        {
            ReloadResource(id, mMaterials, LoadMaterial);
        }

		// ----------------------------------------------------------------  Loaders

		private Mesh LoadMesh(OnyxProjectAsset asset)
		{
            OnyxProjectMeshAsset mAsset = (OnyxProjectMeshAsset)asset;
            if (mAsset.IsFromModel)
            {
                AssimpLoader.ImportMeshes(mAsset.AbsolutePath, (guid, mesh)=>
                {
                    mMeshes.Add(guid, mesh);
                    mMeshes[guid].LinkedProjectAsset = ProjectManager.Instance.Content.GetAsset(guid);
                });
                return GetMesh(asset.Guid);
            }
            else
            {
                // TODO - Legacy, remove when all objects are models
                return ObjLoader.Load(asset.AbsolutePath);
            }
		}
        

        private Material LoadMaterial(OnyxProjectAsset asset)
		{
			return MaterialLoader.Load(asset.AbsolutePath);
		}

		private Shader LoadShader(OnyxProjectAsset asset)
		{
			OnyxProjectShaderAsset sAsset = (OnyxProjectShaderAsset)asset;
			return new Shader(sAsset.PathVertex, sAsset.PathFragment);
		}

		private Texture LoadTexture(OnyxProjectAsset asset)
		{
			return new Texture(asset.AbsolutePath);
		}


	}
	
}

