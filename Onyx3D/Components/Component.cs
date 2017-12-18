using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	[Serializable]
	public abstract class Component : Object
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
    }
}
