using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	public class SceneObject
	{
		private List<Component> mComponents = new List<Component>();
        public string Id;
		public Transform Transform = new Transform();

        public SceneObject(string id)
        {
            Id = id;
        }

		public T AddComponent<T>() where T : Component, new()
		{
			T newComp = new T();
            newComp.Attach(this);
			mComponents.Add(newComp);
            return newComp;
		}
	}
}
