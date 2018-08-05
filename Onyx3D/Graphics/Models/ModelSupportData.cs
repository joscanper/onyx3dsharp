
using System;
using System.Collections.Generic;
using System.IO;

namespace Onyx3D
{


    [Serializable]
    public class ModelSupportData : GameAsset
    {
        public struct MeshData
        {
            public int Id;
            public string Name;
        }

        public string ModelFile;
        public int Guid;

        public List<MeshData> Meshes = new List<MeshData>();
        
        public string FilePath { get { return GetPath(ModelFile); } }

        // --------------------------------------------------------------------

        private ModelSupportData() {}

        // --------------------------------------------------------------------

        public ModelSupportData(string modelPath)
        {
            ModelFile = modelPath;
        }

        // --------------------------------------------------------------------

        public int GetMeshId(string name)
        {
            foreach(MeshData m in Meshes)
            {
                if (name == m.Name)
                    return m.Id;
            }
            return -1;
        }

        // --------------------------------------------------------------------

        public static string GetPath(string modelPath)
        {
            if (modelPath.Length == 0)
                throw new Exception("ModelSupportData needs the a model file path");

            return Path.Combine(Path.GetDirectoryName(modelPath), Path.GetFileNameWithoutExtension(modelPath) + ".o3dmod");
        }
    }
    
}
