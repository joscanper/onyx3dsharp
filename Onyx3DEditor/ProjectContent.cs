using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3DEditor
{

	[Serializable]
	class TextureDBEntry
	{
		public string path;
		public string id;
	}

	[Serializable]
	class ProjectContent
	{
		public List<TextureDBEntry> Textures = new List<TextureDBEntry>();
	}
}
