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

		public Material DefaultMaterial;
		public Shader DefaultShader;

		public void Init()
		{
			PrimitiveMeshes.Teapot = ObjLoader.Load("./Resources/Models/teapot.obj");

			DefaultMaterial = new Material();
			DefaultShader = new Shader("./Resources/Shaders/VertexShader.glsl", "./Resources/Shaders/FragmentShader.glsl");
			DefaultMaterial.Shader = DefaultShader;
		}
	}
}
