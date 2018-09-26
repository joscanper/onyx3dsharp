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
        private Dictionary<int, Entity> mEntities = new Dictionary<int, Entity>();


        public void RefreshAll()
        {
            Refresh(mMeshes, LoadMesh);
            Refresh(mTextures, LoadTexture);
            Refresh(mMaterials, LoadMaterial);
            Refresh(mShaders, LoadShader);
            Refresh(mEntities, LoadEntity);
        }

        private void Refresh<T>(Dictionary<int, T> dict, Func<OnyxProjectAsset, T> loadFallback) where T : GameAsset
        {
            foreach(KeyValuePair<int, T> asset in dict)
            {
                if (asset.Value.IsDirty)
                {
                    ReloadResource(asset.Key, dict, loadFallback);
                }
            }
        }

        // ----------------------------------------------------------------  Getters

        private T GetResource<T>(int id, Dictionary<int, T> map, Func<OnyxProjectAsset, T> loadFallback, int defaultAsset) where T : GameAsset
		{
            
			if (!map.ContainsKey(id))
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.GetAsset(id);
				if (asset == null)
				{
					if (defaultAsset > 0)
						return GetResource(defaultAsset, map, loadFallback, 0);
					else
						return null;
				}

                Onyx3D.MakeCurrent();
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
            map[id].IsDirty = false;
        }

        public Mesh GetMesh(int id)
		{
			Mesh m = GetResource(id, mMeshes, LoadMesh, BuiltInMesh.Cube);
			return m;
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

        public Entity GetEntity(int id)
        {
            return GetResource(id, mEntities, LoadEntity, 0);
        }

     
        // ----------------------------------------------------------------  Loaders

        private Mesh LoadMesh(OnyxProjectAsset asset)
		{
			return AssetLoader<Mesh>.Load(asset.Path, true);
		}

        private Material LoadMaterial(OnyxProjectAsset asset)
		{
			return AssetLoader<Material>.Load(asset.Path, true);
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

        private Entity LoadEntity(OnyxProjectAsset asset)
        {
            return AssetLoader<Entity>.Load(asset.Path, true);
        }


    }

}

