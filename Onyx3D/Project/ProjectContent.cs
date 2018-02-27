using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Onyx3D
{


	[Serializable]
	public class ProjectContent
	{
		
		public List<OnyxProjectSceneAsset> Scenes = new List<OnyxProjectSceneAsset>();
		public List<OnyxProjectAsset> Textures = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Materials = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Meshes = new List<OnyxProjectAsset>();

		[XmlIgnore]
		public Dictionary<int, OnyxProjectAsset> mMappedResources = new Dictionary<int, OnyxProjectAsset>();

		public void Init()
		{
			
			// Built-in meshes (from 100000000)
			AddAsset(new OnyxProjectAsset("./Resources/Models/teapot.obj", BuiltInMesh.Teapot));
			AddAsset(new OnyxProjectAsset("./Resources/Models/cube.obj", BuiltInMesh.Cube));
			AddAsset(new OnyxProjectAsset("./Resources/Models/cylinder.obj", BuiltInMesh.Cylinder));
			AddAsset(new OnyxProjectAsset("./Resources/Models/torus.obj", BuiltInMesh.Torus));
			AddAsset(new OnyxProjectAsset("./Resources/Models/sphere.obj", BuiltInMesh.Sphere));

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
	}
}
