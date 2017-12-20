using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{

	public class BuiltInTextures
	{
		public Texture Checker;
	}

	public class BuiltInShaders
	{
		public Shader Default;
		public Shader Unlit;
		public Shader UnlitVertexColor;
	}

	public class BuiltInMaterials
	{
		public Material Default;
		public Material Unlit;
		public Material UnlitVertexColor;
	}


	public class ResourceManager
	{
		private Dictionary<string, Mesh> mMeshes;
		private Dictionary<string, Material> mMaterials;
		private Dictionary<string, Shader> mShaders;
		private Dictionary<string, Texture> mTextures;

		public BuiltInMaterials BuiltInMaterials = new BuiltInMaterials();
		public BuiltInShaders BuiltInShaders = new BuiltInShaders();
		public BuiltInTextures BuiltInTextures = new BuiltInTextures();
		
		public void Init()
		{
			
			// Textures
			BuiltInTextures.Checker = new Texture("./Resources/Textures/checker.png");

			//Shaders
			BuiltInShaders.Default			= new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl");
			BuiltInShaders.Unlit			= new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitFragmentShader.glsl");
			BuiltInShaders.UnlitVertexColor = new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitVertexColorFragmentShader.glsl");


			// Materials
			BuiltInMaterials.Default = new Material();
			BuiltInMaterials.Default.Shader = BuiltInShaders.Default;
			BuiltInMaterials.Default.Properties.Add("base_color", new MaterialProperty(MaterialPropertyType.Color, new Vector4(1,1,1, 1)));
			BuiltInMaterials.Default.Properties.Add("base_texture", new TextureMaterialProperty(MaterialPropertyType.Sampler2D, BuiltInTextures.Checker, 0));
			BuiltInMaterials.Default.Properties.Add("fresnel", new MaterialProperty(MaterialPropertyType.Float, 2.5f));
			BuiltInMaterials.Default.Properties.Add("fresnel_strength", new MaterialProperty(MaterialPropertyType.Float, 0.5f));
			

			BuiltInMaterials.Unlit = new Material();
			BuiltInMaterials.Unlit.Shader = BuiltInShaders.Unlit;
			BuiltInMaterials.Unlit.Properties.Add("color", new MaterialProperty(MaterialPropertyType.Vector4, Vector4.One));

			BuiltInMaterials.UnlitVertexColor = new Material();
			BuiltInMaterials.UnlitVertexColor.Shader = BuiltInShaders.UnlitVertexColor;
		}

		public Mesh GetMesh(BuiltInMesh mesh)
		{
			/*
			OnyxProjectAsset asset = ProjectLoader.Instance.Content.BuiltInMeshes[mesh];
			string id = "#BUILTIN#" + asset.Guid;
			Mesh m = mMeshes[id];
			if (m == null)
				m = LoadMesh(asset, id);
				*/
			return (new Mesh());
		}

		public Mesh LoadMesh(OnyxProjectAsset asset, string id)
		{
			Mesh m = ObjLoader.Load(asset.Path);
			mMeshes[id] = m;
			return m;
		}
	}
	
}

