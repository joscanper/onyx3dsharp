
using System.IO;

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
            mData = AssetStreamLoader<ProjectData>.Load(path, false);
            Content.Init();
			mCurrentProjectPath = Path.GetFullPath(path);
			return true;
		}

        // --------------------------------------------------------------------

        public void Save()
		{
            Save(mCurrentProjectPath);
		}

        // --------------------------------------------------------------------

        public void Save(string path)
		{
			mCurrentProjectPath = path;
            AssetStreamLoader<ProjectData>.Save(mData, path, false);
        }
	}
}
