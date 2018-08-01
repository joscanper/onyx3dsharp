
using System.IO;
using System.Xml.Serialization;

namespace Onyx3D
{
    
	public class ProjectManager : Singleton<ProjectManager>
	{
		private static readonly int VERSION = 1;

		private ProjectData mData;
		private string mCurrentProjectPath;

        public string Directory { get { return Path.GetDirectoryName(CurrentProjectPath); } }
		public string CurrentProjectPath { get { return mCurrentProjectPath; } }
		public ProjectContent Content { get { return mData.Content; } }

        // --------------------------------------------------------------------

        public void New()
		{
			mData = new ProjectData();
			mCurrentProjectPath = "";
		}

        // --------------------------------------------------------------------

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
			mCurrentProjectPath = Path.GetFullPath(path);
			return true;
		}

        // --------------------------------------------------------------------

        public void Save()
		{
            if (mCurrentProjectPath.Length > 0)
			    Save(mCurrentProjectPath);
		}

        // --------------------------------------------------------------------

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
