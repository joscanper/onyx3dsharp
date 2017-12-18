using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Onyx3D;

namespace Onyx3DEditor
{
	[Serializable]
	class OnyxProjectAsset
	{
		public string Path;
		public Guid Id;

		public OnyxProjectAsset(string path)
		{
			Path = path;
			Id = Guid.NewGuid();
		}
	}


	[Serializable]
	class ProjectContent
	{
		public List<OnyxProjectAsset> Scenes = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Textures = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Materials = new List<OnyxProjectAsset>();
		public List<OnyxProjectAsset> Meshes = new List<OnyxProjectAsset>();
	}
}
