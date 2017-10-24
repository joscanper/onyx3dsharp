using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{
	public class Camera : SceneObject
	{
		Matrix4 mProjection;

		public Matrix4 ViewMatrix
		{
			get {
				return Transform.GetModelMatrix().Inverted();
			}
		}

		public Matrix4 ProjectionMatrix
		{
			get { return mProjection; }
		}


		public Camera(string id) : base(id)
		{
			
		}

		public void InitPerspective(float fov, float aspect, float near = 0.1f, float far = 1000)
		{
			mProjection = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
		}

		public void InitOrtho()
		{
			//TODO
		}


	}
}
