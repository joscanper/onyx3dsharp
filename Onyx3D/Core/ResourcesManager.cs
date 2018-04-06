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

		private T GetResource<T>(int id, Dictionary<int, T> map, Func<OnyxProjectAsset, T> loadFallback) where T : GameAsset
		{
			if (!map.ContainsKey(id))
			{
				OnyxProjectAsset asset = ProjectManager.Instance.Content.GetAsset(id);
				map[id] = loadFallback(asset);
				map[id].LinkedProjectAsset = asset; 
			}

			return map[id];
		}

		public Mesh GetMesh(int id)
		{
			return GetResource(id, mMeshes, LoadMesh);
		}

		public Material GetMaterial(int id)
		{
			return GetResource(id, mMaterials, LoadMaterial);
		}
		
		public Texture GetTexture(int id)
		{
			return GetResource(id, mTextures, LoadTexture);
		}

		public Shader GetShader(int id)
		{
			return GetResource(id, mShaders, LoadShader);
		}

		// ----------------------------------------------------------------  Loaders

		private Mesh LoadMesh(OnyxProjectAsset asset)
		{
			return ObjLoader.Load(asset.Path);
		}

		private Material LoadMaterial(OnyxProjectAsset asset)
		{
			XmlReader xmlReader = XmlReader.Create(asset.Path);
			Material m = new Material();
			m.ReadXml(xmlReader);
			return m;
		}

		private Shader LoadShader(OnyxProjectAsset asset)
		{
			OnyxProjectShaderAsset sAsset = (OnyxProjectShaderAsset)asset;
			return new Shader(sAsset.PathVertex, sAsset.PathFragment);
		}

		private Texture LoadTexture(OnyxProjectAsset asset)
		{
			return new Texture(asset.Path);
		}
	}
	
}

