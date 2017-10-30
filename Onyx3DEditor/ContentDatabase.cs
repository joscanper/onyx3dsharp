using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3DEditor
{

	class TextureDBEntry
	{
		public string path;
		public string id;
	}

	class ContentDatabase
	{
		public static List<TextureDBEntry> Textures = new List<TextureDBEntry>();
	}
}
