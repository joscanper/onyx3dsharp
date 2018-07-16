using System;
using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{
    public class TemplateProxy : SceneObject
    {
		public Template Template { set; get; }

		public TemplateProxy(string id, Scene scene = null, int instanceId = 0) : base(id, scene, instanceId)
        {
        }

		public override List<T> GetComponents<T>()
		{

			if (Template != null && Template.Root != null)
				return Template.Root.GetComponents<T>();
			else
				return null;

			// TODO - Should this get component on children too?
			
		}

		public override T GetComponentInChildren<T>()
		{
			
			if (Template != null && Template.Root != null)
				return Template.Root.GetComponentInChildren<T>();
			else
				return null;
			
			// TODO - Should this get component on children too?
		}

		public override List<T> GetComponentsInChildren<T>()
		{

			if (Template != null && Template.Root != null)
				return Template.Root.GetComponentsInChildren<T>();
			else
				return null;
			
			// TODO - Should this get component on children too?
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
