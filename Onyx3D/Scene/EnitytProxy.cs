using System;
using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{
    public class EntityProxy : SceneObject
    {
		private EntityRenderer mRenderer;
		private Entity mEntity;

		public Entity EntityRef
		{
			set
			{
				//mChildren.Clear();
				//mChildren.Add(value.Root);
				mEntity = value;
				mRenderer.UpdateRenderers(mEntity);
			}
			get { return mEntity; }
		}

		public EntityProxy(string id, Scene scene = null, int instanceId = 0) : base(id, scene, instanceId)
        {
			mRenderer = AddComponent<EntityRenderer>();

		}


		public override SceneObject Clone()
		{
			EntityProxy newObj = new EntityProxy(this.Id, this.Scene);
			newObj.EntityRef = mEntity;
			newObj.Copy(this);

			return newObj;
		}

		/*
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
		*/

		// ------ Serialization ------

		public override void ReadXml(XmlReader reader)
        {
            Id = reader.GetAttribute("id");
            InstanceId = Convert.ToInt32(reader.GetAttribute("instanceId"));
            EntityRef = Onyx3DEngine.Instance.Resources.GetEntity(Convert.ToInt32(reader.GetAttribute("entityGuid")));

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
                        if (reader.Name.Equals("EntityProxy"))
                            return;
                        break;
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("EntityProxy");

            writer.WriteAttributeString("id", Id);
            writer.WriteAttributeString("instanceId", InstanceId.ToString());
            writer.WriteAttributeString("entityGuid", EntityRef.LinkedProjectAsset.Guid.ToString());

            Transform.WriteXml(writer);

			writer.WriteEndElement();
		}

    }
}
