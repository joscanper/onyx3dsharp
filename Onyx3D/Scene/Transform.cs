using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{
	public class Transform
	{
		public SceneObject SceneObject;
		public Transform Parent;
		public Vector3 Position;
		public Quaternion Rotation;


        public Matrix4 GetRotationMatrix()
        {
            return GetYawMatrix(0);
        }

        public Matrix4 GetYawMatrix(float rotY)
        {
            Matrix4 yaw = Matrix4.Identity;
            yaw[0, 0] = (float)Math.Cos(rotY);
            yaw[0, 2] = (float)-Math.Sin(rotY);
            yaw[2, 0] = (float)Math.Sin(rotY);
            yaw[2, 2] = (float)Math.Cos(rotY);
            return yaw;
        }
	}
}
