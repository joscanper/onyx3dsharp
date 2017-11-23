using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Onyx3D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Onyx3DEditor
{
	

	class ProjectManager : Singleton<ProjectManager>
	{
		const int VERSION = 1;
		
		public ProjectContent Content = new ProjectContent();
		//public ProjectSettings Settings = new ProjectSettings();

		private string mCurrentProjectPath;

		public string CurrentProjectPath
		{
			get { return mCurrentProjectPath; }
		}

		public void New()
		{
			Content = new ProjectContent();
			mCurrentProjectPath = "";
		}

		public bool Load(string path)
		{

			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
			}

			mCurrentProjectPath = path;
			Stream stream = null;
			try {
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
				Load(stream);
			} catch {
				return false;
			} finally {
				if (null != stream)
					stream.Close();
			}
		
			return true;
		}

		public void Load(Stream stream)
		{
			IFormatter formatter = new BinaryFormatter();
			int version = (int)formatter.Deserialize(stream);
			Debug.Assert(version == VERSION);
			Content = (ProjectContent)formatter.Deserialize(stream);
		}

		public void Save()
		{
			Save(CurrentProjectPath);
		}

		public void Save(string fileName)
		{
			mCurrentProjectPath = fileName;
			Stream stream = null;
			try
			{
				IFormatter formatter = new BinaryFormatter();
				stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, VERSION);
				formatter.Serialize(stream, Content);
			}
			catch
			{
				// do nothing, just ignore any possible errors
			}
			finally
			{
				if (null != stream)
					stream.Close();
			}
		}
	}
}
