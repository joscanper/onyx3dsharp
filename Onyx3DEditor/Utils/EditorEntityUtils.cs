using Onyx3D;
using OpenTK;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Onyx3DEditor
{
    public static class EditorEntityUtils
    {
        // --------------------------------------------------------------------

        public static Entity Create(SceneObject obj, string name)
        {
            Vector3 position = obj.Transform.Position;

            SceneObject rootNode = new SceneObject(name);
            obj.Parent = rootNode;
            obj.Transform.LocalPosition = Vector3.Zero;

            Entity entity = new Entity(rootNode);
            string entityPath = ProjectContent.GetEntityPath(name);
            AssetLoader<Entity>.Save(entity, entityPath, false);

            OnyxProjectAsset asset = ProjectManager.Instance.Content.AddEntity(entityPath, false, entity);
            asset.Name = name;
            return entity;
        }

        // ----------------------------s----------------------------------------

        public static Entity Create(List<SceneObject> objects, string name, Vector3 position)
        {

            SceneObject rootNode = new SceneObject(name);
            rootNode.Transform.Position = position;

            foreach (SceneObject obj in objects)
            {
                obj.Parent = rootNode;
            }

            rootNode.Transform.Position = Vector3.Zero;

            Entity entity = new Entity(rootNode);
            string entityPath = ProjectContent.GetEntityPath(name);
            AssetLoader<Entity>.Save(entity, entityPath, false);

            OnyxProjectAsset asset = ProjectManager.Instance.Content.AddEntity(entityPath, false, entity);
            asset.Name = name;
            return entity;
        }
        
        // --------------------------------------------------------------------

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

                        AssetLoader<Entity>.Save(entity, entity.LinkedProjectAsset.Path);
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
                Entity entity = Create(Selection.Selected, window.EntityName, position);
                EntityProxy proxy = new EntityProxy(Selection.ActiveObject.Id, SceneManagement.ActiveScene);

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
                obj.Parent = SceneManagement.ActiveScene.Root;
            }

            MainWindow.Instance.UpdateHierarchy();
            //sceneHierarchy.UpdateScene();
        }

        // --------------------------------------------------------------------

        public static void AddProxy()
        {
            EntityProxy tmp = new EntityProxy("TemplateProxy");
            tmp.Parent = SceneManagement.ActiveScene.Root;
            Selection.ActiveObject = tmp;
        }

       
    }
}
