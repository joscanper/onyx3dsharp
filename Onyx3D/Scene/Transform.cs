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

		public Transform(SceneObject sceneObject)
		{
			SceneObject = sceneObject;
		}

		public Matrix4 GetModelMatrix()
		{
			//TODO - Check if needs to be rebaked
			
			Matrix4 t = Matrix4.CreateTranslation(LocalPosition);
			Matrix4 r = Matrix4.CreateFromQuaternion(LocalRotation);
			Matrix4 s = Matrix4.CreateScale(LocalScale);

			Matrix4 model = s * r * t;
			if (SceneObject.Parent != null)
				model *= SceneObject.Parent.Transform.GetModelMatrix() ;

			return model;
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

		public void Rotate(Vector3 euler)
		{
			Quaternion rotation = Quaternion.FromEulerAngles(euler);
			Rotate(rotation);
		}

		public void Rotate(Quaternion rot)
		{
			LocalRotation = LocalRotation * rot;
		}

		public void Translate(Vector3 translation)
		{
			LocalPosition += translation;
		}

		public Vector3 LocalToWorld(Vector3 point)
		{
			
			Vector4 world = new Vector4(point,1);
			world = world * GetModelMatrix();
			return new Vector3(world);
		}
	}
}
