
using OpenTK;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
	
	public class SceneObject : Object, IXmlSerializable
	{
	
		private List<Component> mComponents = new List<Component>();
		private SceneObject mParent;
		private List<SceneObject> mChildren = new List<SceneObject>();
		private Scene mScene;

		public string Id;
		public Transform Transform;

		public List<Component> Components { get { return mComponents; } }

		public SceneObject Parent
		{
			get { return mParent; }
			set
			{
				if (mParent != null)
					mParent.mChildren.Remove(this);


				mParent = value;
				if (value != null)
				{
					value.mChildren.Add(this);
					mScene = value.Scene;
				}
				else
				{
					if (mScene != null)
						Parent = mScene.Root;
				}

				Transform.SetDirty();
			}
		}

		public Scene Scene
		{
			get { return mScene; }
		}

		public int ChildCount
		{
			get { return mChildren.Count;  }
		}

		public SceneObject GetChild(int index)
		{
			return mChildren[index];
		}
		

		public SceneObject(string id, Scene scene = null, int instanceId = 0) : base(instanceId)
        {
            Id = id;
			mScene = scene;
			Transform = new Transform(this);
		}

		public bool RemoveComponent(Component c)
		{
			return mComponents.Remove(c);
		}

        public void RemoveAllComponents()
        {
            mComponents.Clear();
        }

		public void AddComponent(Component comp)
		{
			comp.Attach(this);
			mComponents.Add(comp);
		}

		public T AddComponent<T>() where T : Component, new()
		{
			T newComp = new T();
            newComp.Attach(this);
			mComponents.Add(newComp);
            return newComp;
		}

		public T GetComponent<T>() where T : Component
		{
			for (int i=0; i<mComponents.Count; ++i)
			{
				T comp = (T)mComponents[i];
				if (comp != null)
					return comp;
			}
			return null;
		}

		public List<T> GetComponents<T>() where T : Component
		{
			List<T> components = new List<T>();
			for (int i = 0; i < mComponents.Count; ++i)
			{
				T comp = (T)mComponents[i];
				if (comp != null)
					components.Add(comp);
			}
			return components;
		}

		// ----------- Serialization ------------

		public XmlSchema GetSchema()
		{
			throw new System.NotImplementedException();
		}

		public void ReadXml(XmlReader reader)
		{

			Id = reader.GetAttribute("id");
			InstanceId = Convert.ToInt32(reader.GetAttribute("instanceId"));
			
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if (reader.Name.Equals("SceneObject"))
						{ 
							SceneObject obj = new SceneObject("", Scene);
							obj.ReadXml(reader);
							obj.Parent = this;
						}

						if (reader.Name.Equals("Transform"))
						{ 
							Transform.LocalPosition = XmlUtils.StringToVector3(reader.GetAttribute("position"));
							Vector4 rotation = XmlUtils.StringToVector4(reader.GetAttribute("rotation"));
							Transform.LocalRotation = Quaternion.FromAxisAngle(rotation.Xyz, rotation.W);
							Transform.LocalScale = XmlUtils.StringToVector3(reader.GetAttribute("scale"));
						}

						if (reader.Name.Equals("Component"))
						{
							Component c = Component.GetComponent(reader);
							Components.Add(c);
							c.Attach(this);
						}
							//ComponentLoader.Load(obj, reader);
						break;
					case XmlNodeType.EndElement:
						if (reader.Name.Equals("SceneObject"))
							return;
						break;
				}

			}

		}

		public void WriteXml(XmlWriter writer)
		{

			writer.WriteStartElement("SceneObject");
			writer.WriteAttributeString("id", Id);
			writer.WriteAttributeString("instanceId", InstanceId.ToString());

			writer.WriteStartElement("Transform");
			writer.WriteAttributeString("position", XmlUtils.Vector3ToString(Transform.LocalPosition));
			writer.WriteAttributeString("rotation", XmlUtils.Vector4ToString(Transform.LocalRotation.ToAxisAngle()));
			writer.WriteAttributeString("scale", XmlUtils.Vector3ToString(Transform.LocalScale));
			writer.WriteEndElement();


			foreach (Component c in Components)
				c.WriteXml(writer);

			for (int i = 0; i < ChildCount; ++i)
				GetChild(i).WriteXml(writer);


			writer.WriteEndElement();
		}
	}
}
