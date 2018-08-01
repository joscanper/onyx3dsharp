using Onyx3D;
using System.Collections.Generic;

namespace Onyx3DEditor
{
    public static class EditorSceneObjectUtils
    {
        public static void Group(List<SceneObject> objects)
        {
            if (objects.Count == 0)
                return;

            SceneObject newObj = new SceneObject("New Group", mScene);
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
            mScene.SetDirty();
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
            sceneHierarchy.UpdateScene();
        }

        // --------------------------------------------------------------------

        public static void ClearParent()
        {
            if (Selection.ActiveObject == null)
                return;

            foreach (SceneObject obj in Selection.Selected)
            {
                obj.Parent = mScene.Root;
            }

            sceneHierarchy.UpdateScene();
        }

        // --------------------------------------------------------------------

        public static void AddPrimitive(int meshType, string name, bool select = true)
        {
            SceneObject primitive = SceneObject.CreatePrimitive(mOnyxInstance.Resources, meshType, name);
            primitive.Parent = mScene.Root;
            if (select)
                Selection.ActiveObject = primitive;
        }

        // --------------------------------------------------------------------

        public static void AddReflectionProbe(bool select = true)
        {
            SceneObject obj = new SceneObject("ReflectionProbe", mScene);
            obj.Parent = mScene.Root;
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
            light.Parent = mScene.Root;
            Selection.ActiveObject = light;
        }
    }
}
