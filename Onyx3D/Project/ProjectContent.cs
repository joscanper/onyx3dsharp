using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Onyx3D
{

	public enum BuiltInMesh
	{
		Teapot,
		Sphere,
		Torus,
		Cube,
		Cylinder
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
	}


	[Serializable]
	public class ProjectContent
	{
		
		public List<OnyxProjectAsset> Scenes = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Textures = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Materials = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Meshes = new List<OnyxProjectAsset>();

		
		[XmlIgnore]
		public Dictionary<BuiltInMesh, OnyxProjectAsset> BuiltInMeshes { get; private set; }

		public void Init()
		{
			
			BuiltInMeshes = new Dictionary<BuiltInMesh, OnyxProjectAsset>();

			// Built-in meshes
			AddToBuiltInAsset(new OnyxProjectAsset("./Resources/Models/teapot.obj", 100000001), Meshes, BuiltInMeshes, BuiltInMesh.Teapot);
			AddToBuiltInAsset(new OnyxProjectAsset("./Resources/Models/cube.obj", 100000002), Meshes, BuiltInMeshes, BuiltInMesh.Cube);
			AddToBuiltInAsset(new OnyxProjectAsset("./Resources/Models/cylinder.obj", 100000003), Meshes, BuiltInMeshes, BuiltInMesh.Cylinder);
			AddToBuiltInAsset(new OnyxProjectAsset("./Resources/Models/torus.obj", 100000004), Meshes, BuiltInMeshes, BuiltInMesh.Torus);
			AddToBuiltInAsset(new OnyxProjectAsset("./Resources/Models/sphere.obj", 100000005), Meshes, BuiltInMeshes, BuiltInMesh.Sphere);
			
			
		}

		private void AddToBuiltInAsset<T>(OnyxProjectAsset asset, List<OnyxProjectAsset> resourceList,  Dictionary<T, OnyxProjectAsset> builtInList, T type)
		{

			resourceList.Add(asset);
			builtInList.Add(type, asset);

		}
	}
}
