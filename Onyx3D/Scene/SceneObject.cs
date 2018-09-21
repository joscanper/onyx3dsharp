
using OpenTK;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Onyx3D
{
	
	public class SceneObject : Object, IXmlSerializable, IDisposable
	{
	
		private List<Component> mComponents = new List<Component>();
		private SceneObject mParent;
		private Bounds mObjectBounds = new Bounds();
		private List<Renderer> mTmpRendererList = new List<Renderer>();

		// --------------------------------------------------------------------

		protected List<SceneObject> mChildren = new List<SceneObject>();

		// --------------------------------------------------------------------

		public string Id;
		public Transform Transform;
		public bool Active = true;

		// --------------------------------------------------------------------

		public int ChildCount { get { return mChildren.Count;  } }
		public SceneObject GetChild(int index) { return mChildren[index]; }
		public Scene Scene { get; private set; }
		public List<Component> Components { get { return mComponents; } }
		
		public SceneObject Parent
		{
			get { return mParent; }
			set { SetParent(value); }
		}

		// --------------------------------------------------------------------

		public SceneObject(string id, Scene scene = null, int instanceId = 0) : base(instanceId)
        {
            Id = id;
			Scene = scene;
			Transform = new Transform(this);
		}

		// --------------------------------------------------------------------

		public void Destroy()
        {
			Dispose();
            mComponents.Clear();
            mChildren.Clear();
            if (Scene != null)
            {
                Scene.SetDirty();
                Scene = null;
            }
			SetParent(null);
        }

		// --------------------------------------------------------------------

		public bool RemoveComponent(Component c)
		{
			return mComponents.Remove(c);
		}

		// --------------------------------------------------------------------

		public void RemoveAllComponents()
        {
            mComponents.Clear();
        }

		// --------------------------------------------------------------------

		public void AddComponent(Component comp)
		{
			comp.Attach(this);
			mComponents.Add(comp);
			if (Scene != null)
				Scene.SetDirty();
		}

		// --------------------------------------------------------------------

		public T AddComponent<T>() where T : Component, new()
		{
			T newComp = new T();
            newComp.Attach(this);
			mComponents.Add(newComp);
			if (Scene != null)
				Scene.SetDirty();
			return newComp;
		}

		// --------------------------------------------------------------------

		public T GetComponent<T>() where T : Component
		{
			for (int i=0; i<mComponents.Count; ++i)
			{


                if (mComponents[i].GetType().IsSubclassOf(typeof(T)) || mComponents[i].GetType() == typeof(T))
                {
                    T comp = (T)mComponents[i];
                    if (comp != null)
                        return comp;
                }
			}
			return null;
		}

		// --------------------------------------------------------------------

		public List<T> GetComponents<T>() where T : Component
		{
			List<T> components = new List<T>();
			GetComponents(components);
			return components;
		}

		// --------------------------------------------------------------------

		public void GetComponents<T>(List<T> components) where T : Component
		{	
			for (int i = 0; i < mComponents.Count; ++i)
			{
				if (mComponents[i].GetType().IsSubclassOf(typeof(T)) || mComponents[i].GetType() == typeof(T))
				{
					T comp = (T)mComponents[i];
					if (comp != null)
						components.Add(comp);
				}
			}
		}

		// --------------------------------------------------------------------

		public virtual T GetComponentInChildren<T>(bool excludeEntities = false) where T:Component
		{
			List<T> components = new List<T>();

			T myComponent = GetComponent<T>();
            if (myComponent != null)
                return myComponent;

			for (int i = 0; i < mChildren.Count; ++i)
			{
				if (!mChildren[i].Active)
					continue;

				T c = mChildren[i].GetComponentInChildren<T>(excludeEntities);
                if (c != null)
                    return c;
			}
            
            return null;
		}

		// --------------------------------------------------------------------

		public virtual List<T> GetComponentsInChildren<T>(bool excludeEntities = false) where T : Component
        {
			List<T> components = new List<T>();
			GetComponentsInChildren(components, excludeEntities);
			return components;
        }

		// --------------------------------------------------------------------

		public virtual void GetComponentsInChildren<T>(List<T> components, bool excludeEntities = false) where T : Component
		{
			GetComponents(components);
			
			for (int i = 0; i < mChildren.Count; ++i)
			{
				if (!mChildren[i].Active)
					continue;

				mChildren[i].GetComponentsInChildren(components, excludeEntities);
			}
		}

		// --------------------------------------------------------------------

		public List<Entity> GetEntitiesInChildren()
		{
			List<Entity> entities = new List<Entity>();
			EntityProxy thisEntity = this as EntityProxy;
			if (thisEntity != null && thisEntity.EntityRef != null)
				entities.Add(thisEntity.EntityRef);

			for (int i = 0; i < mChildren.Count; ++i)
			{
				if (!mChildren[i].Active)
					continue;

				mChildren[i].GetEntitiesInChildren(entities);
			}

			return entities;
		}

		// --------------------------------------------------------------------

		public void GetEntitiesInChildren(List<Entity> entities)
		{
			EntityProxy thisEntity = this as EntityProxy;
			if (thisEntity != null && thisEntity.EntityRef != null)
				entities.Add(thisEntity.EntityRef);

			for (int i = 0; i < mChildren.Count; ++i)
			{
				if (!mChildren[i].Active)
					continue;

				mChildren[i].GetEntitiesInChildren(entities);
			}
		}

		// --------------------------------------------------------------------

		public void GetEntityProxiesInChildren(List<EntityProxy> entities)
		{
			ForEachChild((c) =>
			{
				if (c.GetType() == typeof(EntityProxy))
					entities.Add(c as EntityProxy);

				c.GetEntityProxiesInChildren(entities);
			});
		}

		// --------------------------------------------------------------------

		public void RemoveAllChildren()
		{
            for (int i = 0; i < ChildCount; ++i)
                GetChild(i).Destroy();

			mChildren.Clear();
			if (Scene != null)
				Scene.SetDirty();
		}

		// --------------------------------------------------------------------

		public void ForEachChild(Action<SceneObject> a)
        {
            for (int i = 0; i < ChildCount; ++i)
                a.Invoke(GetChild(i));
        }

		// --------------------------------------------------------------------

		public void ForEachComponent(Action<Component> a)
        {
            for (int i = 0; i < mComponents.Count; ++i)
                a.Invoke(mComponents[i]);
        }

		// --------------------------------------------------------------------

		public static SceneObject CreatePrimitive(int meshType, string name)
        {
            SceneObject primitive = new SceneObject(name);
            MeshRenderer mesh = primitive.AddComponent<MeshRenderer>();
            mesh.Mesh = Onyx3DEngine.Instance.Resources.GetMesh(meshType);
            primitive.Transform.LocalPosition = new Vector3(0, 0, 0);
            mesh.Material = Onyx3DEngine.Instance.Resources.GetMaterial(BuiltInMaterial.Default);
            return primitive;
        }

		// --------------------------------------------------------------------

		public virtual Bounds CalculateBounds()
		{
			mObjectBounds.Clear();
			mTmpRendererList.Clear();

			GetComponentsInChildren<Renderer>(mTmpRendererList, true);
			if (mTmpRendererList.Count > 0)
			{
				mObjectBounds = mTmpRendererList[0].Bounds;
				foreach (Renderer renderer in mTmpRendererList)
				{
					mObjectBounds.Encapsulate(renderer.Bounds);
				}
			}
			else
			{
				mObjectBounds.SetMinMax(mObjectBounds.Center, mObjectBounds.Center);
				mObjectBounds.Center = Transform.Position;

			}
			return mObjectBounds;
		}


		// --------------------------------------------------------------------

		public virtual SceneObject Clone()
        {
            SceneObject newObj = new SceneObject(this.Id, this.Scene);
			newObj.Copy(this);

            return newObj;
        }

		// --------------------------------------------------------------------

		protected void Copy(SceneObject obj)
		{
			Parent = obj.Parent;

			Transform.Copy(obj.Transform);

			obj.ForEachComponent((component) => {
				Component c = component.Clone();
				AddComponent(c);
			});

			obj.ForEachChild((child) =>
			{
				SceneObject newChild = child.Clone();
				newChild.Parent = this;
			});
		}

		// --------------------------------------------------------------------

		public virtual void Dispose()
		{
			ForEachComponent((component) =>
			{
				component.OnDestroy();
			});
		}

		// --------------------------------------------------------------------

		public void SetParent(SceneObject parent, bool keepWorldPos = true)
		{

			Vector3 worldPos = Transform.Position;

			if (mParent != null)
				mParent.mChildren.Remove(this);

			if (Scene != null)
				Scene.SetDirty();

			if (parent != null && parent.Scene != null)
				parent.Scene.SetDirty();

			mParent = parent;
			if (parent != null)
			{
				parent.mChildren.Add(this);
				Scene = parent.Scene;

				if (keepWorldPos)
					Transform.LocalPosition = parent.Transform.WorldToLocal(worldPos);
			}
			else
			{
				if (Scene != null)
					mParent = Scene.Root;
			}


			if (Scene != null)
				Scene.SetDirty();

			Transform.SetDirty();
		}

        // --------------------------------------------------------------------

        public virtual void OnDrawGizmos(GizmosManager gizmos)
        {

        }

		// --------------------------------------------------------------------

		private List<Renderer> mRaycastCandidates = new List<Renderer>();

		public bool IntersectRay(Ray ray, out RaycastHit hit)
		{
			hit = new RaycastHit();

			mRaycastCandidates.Clear();
			this.GetIntersectedRendererBounds(ray, this, mRaycastCandidates);

			hit.Distance = float.MaxValue;
			RaycastHit objHit = new RaycastHit();
			foreach (Renderer renderer in mRaycastCandidates)
			{
				if (renderer.IntersectsRay(ray, out objHit) && objHit.Distance < hit.Distance)
				{
					hit = objHit;
				}
			}

			return hit.Object != null;
		}

		// --------------------------------------------------------------------

		private void GetIntersectedRendererBounds(Ray ray, SceneObject obj, List<Renderer> list)
		{
			mTmpRendererList.Clear();
			obj.GetComponentsInChildren<Renderer>(mTmpRendererList, true);
			if (mTmpRendererList.Count > 0)
			{
				for (int i = 0; i < mTmpRendererList.Count; ++i)
				{
					if (mTmpRendererList[i].Bounds.IntersectsRay(ray))
					{
						list.Add(mTmpRendererList[i]);
					}
				}
			}
		}

		// --------------------------------------------------------------------
		// ----------- Serialization ------------
		// --------------------------------------------------------------------

		public XmlSchema GetSchema()
		{
			throw new System.NotImplementedException();
		}

        public virtual void ReadXml(XmlReader reader)
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
							obj.SetParent(this, false);
						}

                        if (reader.Name.Equals("EntityProxy"))
                        {
                            EntityProxy tmp = new EntityProxy("", Scene);
                            tmp.ReadXml(reader);
                            tmp.Parent = this;
                        }

                        if (reader.Name.Equals("Transform"))
						{
                            Transform.ReadXml(reader);
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

        // --------------------------------------------------------------------

        public virtual void WriteXml(XmlWriter writer)
		{

			writer.WriteStartElement("SceneObject");
			writer.WriteAttributeString("id", Id);
			writer.WriteAttributeString("instanceId", InstanceId.ToString());

            Transform.WriteXml(writer);

			foreach (Component c in Components)
				c.WriteXml(writer);

			for (int i = 0; i < ChildCount; ++i)
				GetChild(i).WriteXml(writer);


			writer.WriteEndElement();
		}

		
	}
}
