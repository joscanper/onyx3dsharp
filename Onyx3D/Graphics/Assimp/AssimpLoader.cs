using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assimp;
using Assimp.Configs;

namespace Onyx3D
{
    public static class AssimpLoader
    {

        public static void ImportMeshes(string path, Action<int, Mesh> callback)
        {

            ModelSupportData supportData = ModelSupportDataLoader.Load(path);

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
            Assimp.Scene mImportedModel = importer.ImportFile(ProjectContent.GetAbsolutePath(supportData.ModelFile), PostProcessPreset.TargetRealTimeMaximumQuality);

            for (int i = 0; i < mImportedModel.MeshCount; ++i)
            {
                Onyx3D.Mesh onyxMesh = mImportedModel.Meshes[i].ToOnyx3D();
                callback(supportData.Meshes[i], onyxMesh);    
            }
        }

    }
}
