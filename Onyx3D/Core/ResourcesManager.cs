using Assimp;
using Assimp.Configs;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{

	public class ResourcesManager : EngineComponent
	{
        private Dictionary<Type, Func<OnyxProjectAsset, GameAsset>> mLoaders = new Dictionary<Type, Func<OnyxProjectAsset, GameAsset>>();
        private Dictionary<int, GameAsset> mLoadedAssets = new Dictionary<int, GameAsset>();

        public ResourcesManager()
        {
            mLoaders.Add(typeof(Entity), LoadEntity);
            mLoaders.Add(typeof(Material), LoadMaterial);
            mLoaders.Add(typeof(Texture), LoadTexture);
            mLoaders.Add(typeof(Mesh), LoadMesh);
            mLoaders.Add(typeof(Shader), LoadShader);
        }

        // --------------------------------------------------------------------

        public void ClearAll()
        {
            mLoadedAssets.Clear();            
        }

		// --------------------------------------------------------------------

		public void RefreshDirty()
        {
            HashSet<int> dirtyGuids = ProjectManager.Instance.Content.GetDirtyAssetsGuid();
            foreach (int guid in dirtyGuids)
                Refresh(guid);
            ProjectManager.Instance.Content.ClearDirty();
        }

        // --------------------------------------------------------------------

        private void Refresh(int guid)
        {
            if (!mLoadedAssets.ContainsKey(guid))
                return;

            GameAsset asset = mLoadedAssets[guid];
            Type assetType = asset.GetType();
            OnyxProjectAsset projectAsset = asset.LinkedProjectAsset;
            if (projectAsset == null)
            {
                projectAsset = ProjectManager.Instance.Content.GetAsset(guid);
                mLoadedAssets[guid].LinkedProjectAsset = projectAsset;
            }

            mLoadedAssets[guid].Copy(mLoaders[assetType](projectAsset));

            Logger.Instance.Append("Reloaded "+ assetType +" guid : " + guid);
        }

        // --------------------------------------------------------------------
        // -----------------------------------------------------------  Getters
        // --------------------------------------------------------------------


        private T GetResource<T>(int id, Func<OnyxProjectAsset, T> loadFallback, int defaultAsset) where T : GameAsset
		{
            
			if (!mLoadedAssets.ContainsKey(id))
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.GetAsset(id);
				if (asset == null)
				{
					if (defaultAsset > 0)
						return GetResource(defaultAsset, loadFallback, 0);
					else
						return null;
				}

                mLoadedAssets[id] = loadFallback(asset);
                mLoadedAssets[id].LinkedProjectAsset = asset; 
			}

			return (T)mLoadedAssets[id];
		}

		// --------------------------------------------------------------------

		public Mesh GetMesh(int id)
		{
			Mesh m = GetResource(id, LoadMesh, BuiltInMesh.Cube);
			return m;
		}

        // --------------------------------------------------------------------

        public Material GetMaterial(int id)
		{
			return GetResource(id, LoadMaterial, BuiltInMaterial.NotFound);
		}

		// --------------------------------------------------------------------

		public Texture GetTexture(int id)
		{
			return GetResource(id, LoadTexture, BuiltInTexture.Checker);
		}

		// --------------------------------------------------------------------

		public Shader GetShader(int id)
		{
			return GetResource(id, LoadShader, BuiltInShader.Default);
		}

		// --------------------------------------------------------------------

		public Entity GetEntity(int id)
        {
            return GetResource(id, LoadEntity, 0);
        }


		// --------------------------------------------------------------------
		// -----------------------------------------------------------  Loaders
		// --------------------------------------------------------------------

        // TODO - All assets should be able to be loaded using AssetLoader
        // I have to add meta files for those assets that don´t have it yet

		private Mesh LoadMesh(OnyxProjectAsset asset)
		{
			return AssetLoader<Mesh>.Load(asset.Path, true);
		}

		// --------------------------------------------------------------------

		private Material LoadMaterial(OnyxProjectAsset asset)
		{
			return AssetLoader<Material>.Load(asset.Path, true);
		}

		// --------------------------------------------------------------------

		private Shader LoadShader(OnyxProjectAsset asset)
		{
			OnyxProjectShaderAsset sAsset = (OnyxProjectShaderAsset)asset;
			return new Shader(sAsset.PathVertex, sAsset.PathFragment);
		}

		// --------------------------------------------------------------------

		private Texture LoadTexture(OnyxProjectAsset asset)
		{
			return new Texture(asset.AbsolutePath);
		}

		// --------------------------------------------------------------------

		private Entity LoadEntity(OnyxProjectAsset asset)
        {
            return AssetLoader<Entity>.Load(asset.Path, true);
        }
        
    }

}

