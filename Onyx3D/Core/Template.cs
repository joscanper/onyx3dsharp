
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
    public class Template : GameAsset, IXmlSerializable
    {
        public SceneObject Root;

        public Template(SceneObject root = null)
        {
            Root = root;
        }

        // ------------ Serialization -------------

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("Template");
            reader.ReadToNextSibling("Root");
            this.Root = new SceneObject("TemplateRoot");
            this.Root.ReadXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("Template");


            writer.WriteStartElement("Root");

            for (int i = 0; i < Root.ChildCount; ++i)
                Root.GetChild(i).WriteXml(writer);

            writer.WriteEndElement();
            
            writer.WriteEndElement();

            writer.WriteEndDocument();
        }
    }
}
