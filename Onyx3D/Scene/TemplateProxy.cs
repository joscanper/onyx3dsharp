using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace Onyx3D
{
    public class TemplateProxy : SceneObject
    {
        private Template mTemplate;
        public Template Template
        {
            set
            {
                if (ChildCount > 0)
                    RemoveAllChildren();

                mTemplate = value;
                SceneObject newObject = mTemplate.Root.Clone();
                newObject.Parent = this;
            }
            get
            {
                return mTemplate;
            }
        }

        public TemplateProxy(string id, Scene scene = null, int instanceId = 0) : base(id, scene, instanceId)
        {
        }

        // ------ Serialization ------

        public override void ReadXml(XmlReader reader)
        {
            Id = reader.GetAttribute("id");
            InstanceId = Convert.ToInt32(reader.GetAttribute("instanceId"));
            Template = Onyx3DEngine.Instance.Resources.GetTemplate(Convert.ToInt32(reader.GetAttribute("templateGuid")));

            if (reader.IsEmptyElement)
                return;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:

                        if (reader.Name.Equals("Transform"))
                        {
                            Transform.ReadXml(reader);
                        }

                        //ComponentLoader.Load(obj, reader);
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("TemplateProxy"))
                            return;
                        break;
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("TemplateProxy");
            writer.WriteAttributeString("id", Id);
            writer.WriteAttributeString("instanceId", InstanceId.ToString());
            writer.WriteAttributeString("templateGuid", Template.LinkedProjectAsset.Guid.ToString());

            Transform.WriteXml(writer);
        }

    }
}
