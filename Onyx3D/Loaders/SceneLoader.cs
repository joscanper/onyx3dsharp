using System.Xml;

namespace Onyx3D
{
	public class SceneLoader
	{
		public static Scene Load(string path)
		{
            XmlReader xmlReader = XmlReader.Create(path);
			
			Scene scene = new Scene();
			scene.ReadXml(xmlReader);

			xmlReader.Close();
			
			return scene;
		}

		public static void Save(Scene myScene, string path)
		{
			XmlWriter xmlWriter = XmlWriter.Create(path, ProjectContent.DefaultXMLSettings);

			myScene.WriteXml(xmlWriter);

			xmlWriter.Close();
		}
	}
}
