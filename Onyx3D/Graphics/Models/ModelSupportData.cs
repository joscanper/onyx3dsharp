
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Onyx3D
{

    [Serializable]
    public class ModelSupportData
    {
        public string ModelFile;

        public List<int> Meshes = new List<int>();
        public List<int> Textures = new List<int>();
        public List<int> Materials = new List<int>();

        private string mPath;
        private string mAbsolutePath;

        public string FilePath { get { return mPath; } }
        public string AbsolutePath { get { return mAbsolutePath; } }

        private ModelSupportData() { }

        public ModelSupportData(string path)
        {
            ModelFile = path;
            mPath = GetPath();
            mAbsolutePath = ProjectContent.GetAbsolutePath(mPath);
        }

        private string GetPath()
        {
            if (ModelFile.Length == 0)
                throw new Exception("ModelSupportData needs the a model file path");

            return Path.Combine(Path.GetDirectoryName(ModelFile), Path.GetFileNameWithoutExtension(ModelFile) + ".o3dmod");
        }
    }
    
    public static class ModelSupportDataLoader
    {
        public static ModelSupportData Load(string path)
        {

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open \"" + path + "\", file does not exist.");
            }

            ModelSupportData mData;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModelSupportData));
            StreamReader stream = new StreamReader(path);
            mData = (ModelSupportData)xmlSerializer.Deserialize(stream);

            stream.Close();

            return mData;
        }

        public static void Save(ModelSupportData data)
        {
            StreamWriter stream = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModelSupportData));
                stream = new StreamWriter(data.AbsolutePath, false);
                xmlSerializer.Serialize(stream, data);
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }
    }
}
