using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
	
	public abstract class Component : Object, IXmlSerializable
	{
        private SceneObject mObject;
        private Transform mTransform;

		public Component(int instanceId = 0) : base(instanceId) { }

        public void Attach(SceneObject obj)
        {
			if (mObject != null)
				mObject.RemoveComponent(this);
				
			mObject = obj;
            mTransform = obj.Transform;
        }


		public SceneObject SceneObject
        {
            get { return mObject; } 
        }

        public Transform Transform
        {
            get { return mTransform; }
        }

		public virtual void OnDirtyTransform() { }

        public virtual void OnDrawGizmos(GizmosManager gizmos) { }

		// ---- Serialization ----

		public XmlSchema GetSchema() { return null; }

		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.EndElement:
						return;
					case XmlNodeType.Element:
						ReadComponentXmlNode(reader);
						break;
				}
			}
			
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Component");
			writer.WriteAttributeString("className", this.GetType().Name);

			WriteComponentXml(writer);

			writer.WriteEndElement();
		}

		public abstract void WriteComponentXml(XmlWriter writer);

		public abstract void ReadComponentXmlNode(XmlReader writer);

		public static Component GetComponent(XmlReader reader)
		{
			String className = reader.GetAttribute("className");
			Type classType = Type.GetType("Onyx3D." + className);
			Component component = Activator.CreateInstance(classType) as Component;
			component.ReadXml(reader);
			return component;
		}

	}
}
