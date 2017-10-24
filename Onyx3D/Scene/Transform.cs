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
		public Vector3 LocalPosition = Vector3.Zero;
		public Vector3 LocalScale = Vector3.One;
		public Quaternion LocalRotation = Quaternion.Identity;

		public float testRotX;
		public float testRotY;
		public float testRotZ;

		public Matrix4 GetModelMatrix()
		{
			Matrix4 t = Matrix4.CreateTranslation(LocalPosition);
			Matrix4 r = Matrix4.CreateFromQuaternion(LocalRotation);
			Matrix4 s = Matrix4.CreateScale(LocalScale);
			//TODO - Check if needs to be rebaked

			return s * r * t;
		}

		Matrix4 GetScaleMatrix()
		{
			return Matrix4.CreateScale(LocalScale);
		}

		public Matrix4 GetTranslationMatrix()
		{
			return Matrix4.CreateTranslation(LocalPosition);
		}

        public Matrix4 GetRotationMatrix()
        {
			return Matrix4.CreateFromQuaternion(LocalRotation);
		}

        public Matrix4 GetYawMatrix(float rotY)
        {
			return Matrix4.CreateRotationY(rotY);
        }

		public Matrix4 GetPitchMatrix(float rotX)
		{
			return Matrix4.CreateRotationX(rotX);
		}

		public Matrix4 GetRollMatrix(float rotZ)
		{
			return Matrix4.CreateRotationZ(rotZ);
		}
	}
}
