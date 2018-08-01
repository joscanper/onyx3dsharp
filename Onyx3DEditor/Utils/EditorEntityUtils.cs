using Onyx3D;
using System.Windows.Forms;

namespace Onyx3DEditor
{
    public static class EditorEntityUtils
    {

        public static void CreateFromSelection()
        {
            if (Selection.ActiveObject == null)
                return;

            if (Selection.ActiveObject.GetType() == typeof(EntityProxy))
            {
                if (Selection.Selected.Count > 0)
                {
                    if (MessageBox.Show("Add selected objects to active entity?", "Add to entity", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Entity entity = (Selection.ActiveObject as EntityProxy).EntityRef;

                        foreach (SceneObject obj in Selection.Selected)
                        {
                            if (obj != Selection.ActiveObject)
                            {
                                obj.Parent = Selection.ActiveObject; // Do this so the localposition is transformed into the instance space
                                obj.Parent = entity.Root;
                            }
                        }

                        EntityLoader.Save(entity, entity.LinkedProjectAsset.AbsolutePath);
                        Selection.ActiveObject = Selection.ActiveObject;
                    }
                }
                return;
            }

            NewEntityWindow window = new NewEntityWindow();
            if (window.ShowDialog() == DialogResult.OK)
            {
                SceneObject parent = Selection.ActiveObject.Parent;

                OpenTK.Vector3 position = Selection.MiddlePoint();
                Entity entity = EntityLoader.Create(Selection.Selected, window.EntityName, position);
                EntityProxy proxy = new EntityProxy(Selection.ActiveObject.Id, mScene);

                proxy.EntityRef = entity;
                proxy.Parent = parent;
                proxy.Transform.LocalPosition = position;

                Selection.ActiveObject = proxy;

            }
            window.Dispose();
        }


        // --------------------------------------------------------------------

        public static void ExcludeSelection()
        {
            foreach (SceneObject obj in Selection.Selected)
            {
                obj.Parent = mScene.Root;
            }

            sceneHierarchy.UpdateScene();
        }

        // --------------------------------------------------------------------

        public static void AddProxy()
        {
            EntityProxy tmp = new EntityProxy("TemplateProxy");
            tmp.Parent = mScene.Root;
            Selection.ActiveObject = tmp;
        }
    }
}
