
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
	public class Scene : IXmlSerializable
	{
	
		public SceneObject Root;
		public Camera ActiveCamera;
		public Lighting Lighting = new Lighting();
        public MeshRenderer Sky;

		public Scene()
		{
			Root = new SceneObject("", this);
		}

		// ------ Serialization ------

		public XmlSchema GetSchema()
		{
			throw new System.NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{
			
			reader.ReadStartElement("Scene");

			Lighting.ReadXml(reader);

			reader.ReadToNextSibling("Root");
			this.Root = new SceneObject("", this, 0);
			this.Root.ReadXml(reader);
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartDocument();

			writer.WriteStartElement("Scene");

			Lighting.WriteXml(writer);

			writer.WriteStartElement("Root");

			for (int i = 0; i < Root.ChildCount; ++i)
				Root.GetChild(i).WriteXml(writer);

			writer.WriteEndElement();

			writer.WriteEndElement();

			writer.WriteEndDocument();
		}
	}
}
