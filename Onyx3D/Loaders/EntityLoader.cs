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
			Entity entity = new Entity(obj);
			string entityPath = ProjectContent.GetEntityPath(name);
			EntityLoader.Save(entity, entityPath);
			OnyxProjectAsset asset = ProjectManager.Instance.Content.AddTemplate(entityPath, false, entity);
			asset.Name = name;
			return entity;
		}
    }
}
