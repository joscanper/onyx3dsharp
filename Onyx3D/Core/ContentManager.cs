using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{
	
	public class ContentManager : Singleton<ContentManager>
	{
		public Dictionary<string, Material> Materials;
		public Dictionary<string, Shader> Shaders;
		public Dictionary<string, Texture> Textures;

		//public Texture DefaultTexture;
		//public Material DefaultMaterial;
		//public Shader DefaultShader;

		public class BuiltInTextures
		{
			public static Texture Checker;
		}

		public class BuiltInShaders
		{
			public static Shader Default;
			public static Shader Unlit;
		}

		public class BuiltInMaterials
		{
			public static Material Default;
			public static Material Unlit;
		}

		public class BuiltInMeshes
		{
			public static Mesh Teapot;
			public static Mesh Sphere;
			public static Mesh Torus;
			public static Mesh Cube;
			public static Mesh Cylinder;
		}


		public void Init()
		{
			// Meshes
			BuiltInMeshes.Teapot = ObjLoader.Load("./Resources/Models/teapot.obj");
			BuiltInMeshes.Torus = ObjLoader.Load("./Resources/Models/torus.obj");
			BuiltInMeshes.Sphere = ObjLoader.Load("./Resources/Models/sphere.obj");
			BuiltInMeshes.Cube = ObjLoader.Load("./Resources/Models/cube.obj");
			BuiltInMeshes.Cylinder = ObjLoader.Load("./Resources/Models/cylinder.obj");

			// Textures
			BuiltInTextures.Checker = new Texture("./Resources/Textures/checker.png");

			//Shaders
			BuiltInShaders.Default = new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl");
			BuiltInShaders.Unlit = new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitFragmentShader.glsl");

			// Materials
			BuiltInMaterials.Default = new Material();
			BuiltInMaterials.Default.Shader = BuiltInShaders.Default;
			BuiltInMaterials.Default.Properties.Add("base", new TextureMaterialProperty(MaterialPropertyType.Sampler2D, BuiltInTextures.Checker, 0));
			BuiltInMaterials.Default.Properties.Add("fresnel", new MaterialProperty(MaterialPropertyType.Float, 2.0f));

			BuiltInMaterials.Unlit = new Material();
			BuiltInMaterials.Unlit.Shader = BuiltInShaders.Unlit;
			BuiltInMaterials.Unlit.Properties.Add("color", new MaterialProperty(MaterialPropertyType.Vector4, Vector4.One));

		}
	}
}
