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

	public class BuiltInMeshes
	{
		public Mesh Teapot;
		public Mesh Sphere;
		public Mesh Torus;
		public Mesh Cube;
		public Mesh Cylinder;
	}


	public class ContentManager
	{
		public Dictionary<string, Material> Materials;
		public Dictionary<string, Shader> Shaders;
		public Dictionary<string, Texture> Textures;
		
		public BuiltInMaterials BuiltInMaterials = new BuiltInMaterials();
		public BuiltInMeshes BuiltInMeshes = new BuiltInMeshes();
		public BuiltInShaders BuiltInShaders = new BuiltInShaders();
		public BuiltInTextures BuiltInTextures = new BuiltInTextures();

		public void Init()
		{
			// Meshes
			BuiltInMeshes.Teapot	= ObjLoader.Load("./Resources/Models/teapot.obj");
			BuiltInMeshes.Torus		= ObjLoader.Load("./Resources/Models/torus.obj");
			BuiltInMeshes.Sphere	= ObjLoader.Load("./Resources/Models/sphere.obj");
			BuiltInMeshes.Cube		= ObjLoader.Load("./Resources/Models/cube.obj");
			BuiltInMeshes.Cylinder	= ObjLoader.Load("./Resources/Models/cylinder.obj");

			// Textures
			BuiltInTextures.Checker = new Texture("./Resources/Textures/checker.png");

			//Shaders
			BuiltInShaders.Default			= new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl");
			BuiltInShaders.Unlit			= new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitFragmentShader.glsl");
			BuiltInShaders.UnlitVertexColor = new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/UnlitVertexColorFragmentShader.glsl");


			// Materials
			BuiltInMaterials.Default = new Material();
			BuiltInMaterials.Default.Shader = BuiltInShaders.Default;
			BuiltInMaterials.Default.Properties.Add("t_base", new TextureMaterialProperty(MaterialPropertyType.Sampler2D, BuiltInTextures.Checker, 0));
			BuiltInMaterials.Default.Properties.Add("fresnel", new MaterialProperty(MaterialPropertyType.Float, 5.0f));
			BuiltInMaterials.Default.Properties.Add("fresnel_strength", new MaterialProperty(MaterialPropertyType.Float, 2.0f));

			BuiltInMaterials.Unlit = new Material();
			BuiltInMaterials.Unlit.Shader = BuiltInShaders.Unlit;
			BuiltInMaterials.Unlit.Properties.Add("color", new MaterialProperty(MaterialPropertyType.Vector4, Vector4.One));

			BuiltInMaterials.UnlitVertexColor = new Material();
			BuiltInMaterials.UnlitVertexColor.Shader = BuiltInShaders.UnlitVertexColor;
		}
	}
	
}

