using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	class SceneNode
	{
		private List<Component> mComponents;
		public Transform Transform;

		public void AddComponent<T>() where T : Component, new()
		{
			T newComp = new T();
			mComponents.Add(newComp);
		}
	}
}
