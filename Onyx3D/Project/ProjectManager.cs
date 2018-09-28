
using System.IO;

namespace Onyx3D
{
    
	public class ProjectManager : Singleton<ProjectManager>
	{
		private static readonly int VERSION = 1;

		private ProjectData mData;
		private bool mDirty;
		private string mCurrentProjectPath;

        public string Directory { get { return Path.GetDirectoryName(CurrentProjectPath); } }
		public string CurrentProjectPath { get { return mCurrentProjectPath; } }
		public ProjectContent Content { get { return mData.Content; } }
		public bool IsDirty { get { return mDirty; } }

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
			SetDirty(false);
		}

        // --------------------------------------------------------------------

        public void Save(string path)
		{
			mCurrentProjectPath = path;
            AssetStreamLoader<ProjectData>.Save(mData, path, false);
        }

		// --------------------------------------------------------------------

		public void SetDirty(bool dirty)
		{
			mDirty = dirty;
		}
	}
}
