using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	
	public class ContentManager : Singleton<ContentManager>
	{
		public Dictionary<string, Material> Materials;
		public Dictionary<string, Shader> Shaders;
		public Dictionary<string, Texture> Textures;

		public Texture DefaultTexture;
		public Material DefaultMaterial;
		public Shader DefaultShader;

		public void Init()
		{
			// Meshes
			PrimitiveMeshes.Teapot = ObjLoader.Load("./Resources/Models/teapot.obj");
			PrimitiveMeshes.Torus = ObjLoader.Load("./Resources/Models/torus.obj");
			PrimitiveMeshes.Sphere = ObjLoader.Load("./Resources/Models/sphere.obj");

			// Textures
			DefaultTexture = new Texture("./Resources/Textures/checker.png");

			//Shaders
			DefaultShader = new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl");

			// Materials
			DefaultMaterial = new Material();
			DefaultMaterial.Shader = DefaultShader;
			DefaultMaterial.Properties.Add("base", new MaterialProperty(MaterialPropertyType.Sampler2D, DefaultTexture.Id, 0));

		}
	}
}
