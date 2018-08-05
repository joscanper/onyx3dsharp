
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
    public class Entity : GameAsset, IXmlSerializable
    {
        public SceneObject Root;

        public Entity(){}

        // --------------------------------------------------------------------

        public Entity(SceneObject root = null)
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
            reader.ReadStartElement("Entity");
            reader.ReadToNextSibling("Root");
            this.Root = new SceneObject("EntityRoot");
            this.Root.ReadXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("Entity");


            writer.WriteStartElement("Root");

            for (int i = 0; i < Root.ChildCount; ++i)
                Root.GetChild(i).WriteXml(writer);

            writer.WriteEndElement();
            
            writer.WriteEndElement();

            writer.WriteEndDocument();
        }
    }
}
