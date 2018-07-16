
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
		protected List<SceneObject> mChildren = new List<SceneObject>();
		private Scene mScene;

		public string Id;
		public Transform Transform;
		public bool Active = true;

		public List<Component> Components { get { return mComponents; } }

		public SceneObject Parent
		{
			get { return mParent; }
			set { SetParent(value); }
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

        public void Destroy()
        {
			Dispose();
            mComponents.Clear();
            mChildren.Clear();
            mScene = null;
			SetParent(null);
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
			if (mScene != null)
				mScene.SetDirty();
		}

		public T AddComponent<T>() where T : Component, new()
		{
			T newComp = new T();
            newComp.Attach(this);
			mComponents.Add(newComp);
			if (mScene != null)
				mScene.SetDirty();
			return newComp;
		}

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

		public virtual List<T> GetComponents<T>() where T : Component
		{

			List<T> components = new List<T>();
			for (int i = 0; i < mComponents.Count; ++i)
			{
                if (mComponents[i].GetType().IsSubclassOf(typeof(T)) || mComponents[i].GetType() == typeof(T))
                {
                    T comp = (T)mComponents[i];
                    if (comp != null)
                        components.Add(comp);
                }
			}
			return components;
		}

		public virtual T GetComponentInChildren<T>() where T:Component
		{

			List<T> components = new List<T>();

			T myComponent = GetComponent<T>();
            if (myComponent != null)
                return myComponent;

			for (int i = 0; i < mChildren.Count; ++i)
			{
				if (!mChildren[i].Active)
					continue;

				T c = mChildren[i].GetComponentInChildren<T>();
                if (c != null)
                    return c;
			}
            
            return null;
		}

        public virtual List<T> GetComponentsInChildren<T>() where T : Component
        {

			List<T> components = new List<T>();

            List<T> myComponents = GetComponents<T>();
            if (myComponents != null)
                components.AddRange(myComponents);

            for (int i = 0; i < mChildren.Count; ++i)
            {
				if (!mChildren[i].Active)
					continue;

				List<T> c = mChildren[i].GetComponentsInChildren<T>();
                if (c != null && c.Count > 0)
                    components.AddRange(c);
            }

            return components;
        }


        public void RemoveAllChildren()
		{
            for (int i = 0; i < ChildCount; ++i)
                GetChild(i).Destroy();

			mChildren.Clear();
			if (mScene != null)
				mScene.SetDirty();
		}

        public void ForEachChild(Action<SceneObject> a)
        {
            for (int i = 0; i < ChildCount; ++i)
                a.Invoke(GetChild(i));
        }

        public void ForEachComponent(Action<Component> a)
        {
            for (int i = 0; i < mComponents.Count; ++i)
                a.Invoke(mComponents[i]);
        }

        public static SceneObject CreatePrimitive(ResourcesManager resources, int meshType, string name)
        {
            SceneObject primitive = new SceneObject(name);
            MeshRenderer mesh = primitive.AddComponent<MeshRenderer>();
            mesh.Mesh = resources.GetMesh(meshType);
            primitive.Transform.LocalPosition = new Vector3(0, 0, 0);
            mesh.Material = resources.GetMaterial(BuiltInMaterial.Default);
            return primitive;
        }

        public Bounds CalculateBoundingBox()
        {
            Bounds b = new Bounds();
            List<MeshRenderer> renderers = GetComponentsInChildren<MeshRenderer>();
            for(int i=0; i<renderers.Count; ++i)
            {
                if (renderers[i].Mesh == null)
                    continue;

                List<Vertex> vertices = renderers[i].Mesh.Vertices;
                for (int v = 0; v < vertices.Count; ++v)
                {
                    Vector3 worldPos = renderers[i].Transform.LocalToWorld(vertices[v].Position);
                    b.Encapsulate(worldPos);
                }
            }
            return b;
        }

        public SceneObject Clone()
        {
            SceneObject newObj = new SceneObject(this.Id, this.Scene);
			newObj.Transform.Copy(Transform);
			
			ForEachComponent((component) => {
                Component c = component.Clone();
                newObj.AddComponent(c);
            });

            ForEachChild((child) =>
            {
                SceneObject newChild = child.Clone();
				newChild.Parent = newObj;
			});

            return newObj;
        }


		public virtual void Dispose()
		{
			ForEachComponent((component) =>
			{
				component.OnDestroy();
			});
		}

		public void SetParent(SceneObject parent, bool keepWorldPos = true)
		{

			Vector3 worldPos = Transform.Position;

			if (mParent != null)
				mParent.mChildren.Remove(this);

			if (mScene != null)
				mScene.SetDirty();

			if (parent != null && parent.mScene != null)
				parent.mScene.SetDirty();

			mParent = parent;
			if (parent != null)
			{
				parent.mChildren.Add(this);
				mScene = parent.Scene;

				if (keepWorldPos)
					Transform.LocalPosition = parent.Transform.WorldToLocal(worldPos);
			}
			else
			{
				if (mScene != null)
					mParent = mScene.Root;
			}

			
			Transform.SetDirty();
			
		}

		public Bounds CalculateBounds()
		{
			MeshRenderer mr = GetComponent<MeshRenderer>();
			if (mr != null)
			{
				//TODO - Get from children an encapsulate to form a bound that contains all mesh renderers
				return mr.Bounds;
			}
			else
			{
				Bounds b = new Bounds();
				b.Center = Transform.Position;
				b.SetMinMax(b.Center - Vector3.One * 0.25f, b.Center+ Vector3.One * 0.25f);
				return b;
			}
		}

		// ----------- Serialization ------------

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
							obj.Parent = this;
						}

                        if (reader.Name.Equals("TemplateProxy"))
                        {
                            TemplateProxy tmp = new TemplateProxy("", Scene);
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
