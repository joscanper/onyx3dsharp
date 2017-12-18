using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Onyx3D
{
	
	public class SceneObject : Object
	{
	
		private List<Component> mComponents = new List<Component>();
		private SceneObject mParent;
		private List<SceneObject> mChildren = new List<SceneObject>();
		private Scene mScene;

		public string Id;
		public Transform Transform;
		

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

	}
}
