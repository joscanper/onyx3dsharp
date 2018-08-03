using Onyx3D;
using System;

namespace Onyx3DEditor
{
    public static class SceneManagement
    {
        public static Action<Scene> OnSceneChanged;

        public static OnyxProjectSceneAsset mActiveSceneAsset;
        public static Scene mActiveScene;

        public static Scene ActiveScene { get { return mActiveScene; } }
        public static OnyxProjectSceneAsset ActiveSceneAsset { get { return mActiveSceneAsset; } }


        // --------------------------------------------------------------------

        public static void New()
        {
            mActiveSceneAsset = null;
            mActiveScene = new Scene();

            EditorSceneObjectUtils.AddReflectionProbe(false);
            EditorSceneObjectUtils.AddPrimitive(BuiltInMesh.Quad, "Quad");

            OnSceneChanged?.Invoke(mActiveScene);
        }

        // --------------------------------------------------------------------

        public static void LoadInitScene()
        {
            mActiveSceneAsset = ProjectManager.Instance.Content.GetInitScene();
            if (mActiveSceneAsset == null)
            {
                New();
                return;
            }
            
            mActiveScene = AssetLoader<Scene>.Load(ProjectContent.GetAbsolutePath(mActiveSceneAsset.Path), Onyx3DEngine.Instance);
            OnSceneChanged?.Invoke(mActiveScene);
        }
    }
}
