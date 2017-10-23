using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
	public abstract class Component
	{
        private SceneObject mObject;
        private Transform mTransform;

        public void Attach(SceneObject obj)
        {
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
