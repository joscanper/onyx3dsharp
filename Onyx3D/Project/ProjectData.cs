using System;
using System.Xml.Serialization;

namespace Onyx3D
{

	[Serializable]
	public class ProjectData
	{

		public int Version;
		public ProjectContent Content = new ProjectContent();
		//public ProjectSettings Settings = new ProjectSettings();

		public ProjectData()
		{
			Clear();
		}

		public void Clear()
		{
			Content = new ProjectContent();
			Content.Init();
		}


	}

}
