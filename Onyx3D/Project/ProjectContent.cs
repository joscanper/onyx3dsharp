using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Onyx3D
{

	public class BuiltInShader
	{
		public static int Default = 300000001;
		public static int Unlit = 300000002;
		public static int UnlitVertexColor = 300000003;
	}

	public class BuiltInMesh
	{
		public static int Teapot = 100000001;
		public static int Sphere = 100000002;
		public static int Torus = 100000003;
		public static int Cube = 100000004;
		public static int Cylinder = 100000005;
	}

	public class BuiltInTexture
	{
		public static int Checker = 200000001;
		public static int White = 200000002;
		public static int Black = 200000003;
		public static int Normal = 200000004;
	}


	public class BuiltInMaterial
	{
		public static int Default = 400000001;
		public static int Unlit = 400000002;
		public static int UnlitVertexColor = 400000003;
	}

	[Serializable]
	public class OnyxProjectAsset
	{
		public static int LastGeneratedGuid { get; private set; }

		public string Path;

		public int Guid;

		public OnyxProjectAsset() { }

		public OnyxProjectAsset(string path, int guid = 0)
		{
			Path = path;
			Guid = guid;
			if (guid == 0)
				Guid = LastGeneratedGuid + 1;
		}
	};

	[Serializable]
	public class OnyxProjectShaderAsset : OnyxProjectAsset
	{
		public string PathVertex;
		public string PathFragment;

		public OnyxProjectShaderAsset(string pathV, string pathF, int guid = 0) : base("", guid)
		{
			PathVertex = pathV;
			PathFragment = pathF;
		}
	};


	[Serializable]
	public class ProjectContent
	{
		
		public List<OnyxProjectAsset> Scenes = new List<OnyxProjectAsset>();
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

			// TODO - Read MY Project Assets ------
		}

		public void AddAsset(OnyxProjectAsset asset)
		{
			mMappedResources.Add(asset.Guid, asset);
		}

		public OnyxProjectAsset GetAsset(int id)
		{
			return mMappedResources[id];
		}
	}
}
