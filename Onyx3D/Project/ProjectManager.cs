using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


using Onyx3D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Onyx3D
{


	public class ProjectManager : Singleton<ProjectManager>
	{
		const int VERSION = 1;

		private ProjectData mData;
		private string mCurrentProjectPath;


		public ProjectContent Content { get { return mData.Content; } }


		public string CurrentProjectPath
		{
			get { return mCurrentProjectPath; }
		}

		public void New()
		{
			mData = new ProjectData();
			mCurrentProjectPath = "";
		}

		public bool Load(string path)
		{

			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
			}

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectData));
			StreamReader stream = new StreamReader(path);
			mData = (ProjectData)xmlSerializer.Deserialize(stream);

			stream.Close();

			Content.Init();

			return true;
		}
		

		public void Save()
		{
			Save(CurrentProjectPath);
		}

		public void Save(string fileName)
		{
			mCurrentProjectPath = fileName;
			StreamWriter stream = null;
			try
			{
			
				XmlSerializer xmlSerializer = new XmlSerializer(mData.GetType());
				stream = new StreamWriter(fileName, false);
				xmlSerializer.Serialize(stream, mData);
			}
			finally
			{
				if (null != stream)
					stream.Close();
			}
		}
	}
}
