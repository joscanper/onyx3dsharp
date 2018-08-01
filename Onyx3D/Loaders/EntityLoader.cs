using OpenTK;
using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{
    public class EntityLoader
    {
        public static Entity Load(string path)
        {
            XmlReader xmlReader = XmlReader.Create(path);
            Entity entity = new Entity();
            entity.ReadXml(xmlReader);
            xmlReader.Close();
            return entity;
        }

        public static void Save(Entity entity, string path)
        {
            XmlWriter xmlWriter = XmlWriter.Create(path, ProjectContent.DefaultXMLSettings);
            entity.WriteXml(xmlWriter);
            xmlWriter.Close();
        }

		public static Entity Create(SceneObject obj, string name)
		{
			Vector3 position = obj.Transform.Position;

			SceneObject rootNode = new SceneObject(name);
			obj.Parent = rootNode;
			obj.Transform.LocalPosition = Vector3.Zero;

			Entity entity = new Entity(rootNode);
			string entityPath = ProjectContent.GetEntityPath(name);
			Save(entity, entityPath);

			OnyxProjectAsset asset = ProjectManager.Instance.Content.AddTemplate(entityPath, false, entity);
			asset.Name = name;
			return entity;
		}

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
			Save(entity, entityPath);

			OnyxProjectAsset asset = ProjectManager.Instance.Content.AddTemplate(entityPath, false, entity);
			asset.Name = name;
			return entity;
		}
	}
}
