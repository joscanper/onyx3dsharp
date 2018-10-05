using System;
using System.Collections.Generic;
using System.Xml;

namespace Onyx3D
{
    public class EntityProxy : SceneObject
    {
		private EntityRenderer mRenderer;
		private Entity mEntity;

		// --------------------------------------------------------------------

		public Entity EntityRef
		{
			set
			{
				//mChildren.Clear();
				//mChildren.Add(value.Root);
				mEntity = value;
				mRenderer.UpdateRenderers(mEntity);
				if (Scene != null)
					Scene.SetDirty();
			}
			get { return mEntity; }
		}

		// --------------------------------------------------------------------

		public EntityProxy(string id, Scene scene = null, int instanceId = 0) : base(id, scene, instanceId)
        {
			mRenderer = AddComponent<EntityRenderer>();

		}

		// --------------------------------------------------------------------

		public override SceneObject Clone()
		{
			EntityProxy newObj = new EntityProxy(this.Id, this.Scene);
			newObj.EntityRef = mEntity;
			newObj.Copy(this);

			return newObj;
		}

		// --------------------------------------------------------------------

		public override T GetComponentInChildren<T>(bool excludeEntities = false)
		{
			
			T c = base.GetComponentInChildren<T>();
			if (c == null && !excludeEntities && EntityRef != null)
				return EntityRef.Root.GetComponentInChildren<T>(excludeEntities = false);
			else
				return c;
		}

		// --------------------------------------------------------------------

		public override List<T> GetComponentsInChildren<T>(bool excludeEntities = false)
		{
			List<T> components = new List<T>();
			GetComponentsInChildren<T>(components, excludeEntities);
			return components;
		}
		
		// --------------------------------------------------------------------

		public override void GetComponentsInChildren<T>(List<T> components, bool excludeEntities = false)
		{			
			base.GetComponentsInChildren(components);

			if (excludeEntities || EntityRef == null)
				return;

			EntityRef.Root.GetComponentsInChildren<T>(components, excludeEntities);
		}

		// --------------------------------------------------------------------

		public override Bounds CalculateBounds()
		{
			mRenderer.UpdateBounds();
			return base.CalculateBounds();
		}

		// --------------------------------------------------------------------

		public override void GetIntersectedRendererBounds(Ray ray, List<Renderer> list)
		{
			base.GetIntersectedRendererBounds(ray, list);

			mRenderer.UpdateBounds();
			
			EntityRef.Root.GetIntersectedRendererBounds(ray, list);
		}

		// --------------------------------------------------------------------
		// ------ Serialization ------
		// --------------------------------------------------------------------

		private static readonly string sXmlNodeName = "EntityProxy";
		private static readonly string sXmlAtttributeId = "id";
		private static readonly string sXmlAtttributeInstanceId = "instanceId";
		private static readonly string sXmlAtttributeEntityGuid = "entityGuid";

		public override void ReadXml(XmlReader reader)
        {
            Id = reader.GetAttribute(sXmlAtttributeId);
            InstanceId = Convert.ToInt32(reader.GetAttribute(sXmlAtttributeInstanceId));
            EntityRef = Onyx3DEngine.Instance.Resources.GetEntity(Convert.ToInt32(reader.GetAttribute(sXmlAtttributeEntityGuid)));

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
                        if (reader.Name.Equals(sXmlNodeName))
                            return;
                        break;
                }
            }
        }

		// --------------------------------------------------------------------

		public override void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(sXmlNodeName);

            writer.WriteAttributeString(sXmlAtttributeId, Id);
            writer.WriteAttributeString(sXmlAtttributeInstanceId, InstanceId.ToString());
            writer.WriteAttributeString(sXmlAtttributeEntityGuid, EntityRef.LinkedProjectAsset.Guid.ToString());

            Transform.WriteXml(writer);

			writer.WriteEndElement();
		}

    }
}
