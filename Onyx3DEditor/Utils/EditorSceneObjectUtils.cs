using Onyx3D;
using OpenTK;
using System.Collections.Generic;

namespace Onyx3DEditor
{
    public static class EditorSceneObjectUtils 
    {
        public static void Group(List<SceneObject> objects)
        {
            if (objects.Count == 0)
                return;

            SceneObject newObj = new SceneObject("New Group", objects[0].Scene);
            newObj.Transform.LocalPosition = Selection.ActiveObject.Transform.LocalPosition;
            newObj.Parent = objects[0];
            foreach (SceneObject obj in objects)
            {
                obj.Parent = newObj;
            }

            Selection.ActiveObject = newObj;
        }

        // --------------------------------------------------------------------

        public static void Duplicate()
        {
            if (Selection.ActiveObject == null)
                return;

            SceneObject clone = Selection.ActiveObject.Clone();
            clone.Parent = Selection.ActiveObject.Parent;

            Selection.ActiveObject = clone;
        }

        // --------------------------------------------------------------------

        public static void Delete(SceneObject o)
        {
            if (o == null)
                return;

            o.Destroy();
            o = null;
            
            Selection.ActiveObject = null;
        }

        // --------------------------------------------------------------------

        public static void SetActiveAsParent()
        {
            if (Selection.ActiveObject == null)
                return;

            foreach (SceneObject obj in Selection.Selected)
            {
                if (obj != Selection.ActiveObject)
                    obj.Parent = Selection.ActiveObject;
            }

            MainWindow.Instance.UpdateHierarchy();
        }

        // --------------------------------------------------------------------

        public static void ClearParent()
        {
            if (Selection.ActiveObject == null || Selection.Selected.Count == 0)
                return;

            Scene scene = SceneManagement.ActiveScene;
            foreach (SceneObject obj in Selection.Selected)
            {
                obj.Parent = scene.Root;
            }

            MainWindow.Instance.UpdateHierarchy();
        }

        // --------------------------------------------------------------------

        public static void AddPrimitive(int meshType, string name, bool select = true)
        {
            SceneObject primitive = SceneObject.CreatePrimitive(meshType, name);
            primitive.Parent = SceneManagement.ActiveScene.Root;
            if (select)
                Selection.ActiveObject = primitive;
        }

        // --------------------------------------------------------------------

        public static void AddReflectionProbe(bool select = true)
        {
            SceneObject obj = new SceneObject("ReflectionProbe", SceneManagement.ActiveScene);
            obj.Parent = SceneManagement.ActiveScene.Root;
            obj.Transform.LocalPosition = new Vector3(0, 0, 0);
            ReflectionProbe mReflectionProbe = obj.AddComponent<ReflectionProbe>();
            mReflectionProbe.Init(64);
            if (select)
                Selection.ActiveObject = obj;
        }

        // --------------------------------------------------------------------

        public static void AddLight()
        {
            SceneObject light = new SceneObject("Light");
            Light lightC = light.AddComponent<Light>();
            light.Parent = SceneManagement.ActiveScene.Root;
            Selection.ActiveObject = light;
        }
    }
}
