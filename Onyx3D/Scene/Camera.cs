using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{

	public struct CameraUBufferData
	{
		public Matrix4 View;
		public Matrix4 Projection;
		public Vector3 Position;
	}


	public class Camera : SceneObject
	{
		Matrix4 mProjection;

		UBO<CameraUBufferData> mCameraUBO;
		CameraUBufferData mUBufferData;

		public UBO<CameraUBufferData> UBO { get { return mCameraUBO; } }
		
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
			mUBufferData = new CameraUBufferData();
			mCameraUBO = new UBO<CameraUBufferData>(mUBufferData, "CameraData");
		}

		public void InitPerspective(float fov, float aspect, float near = 0.1f, float far = 1000)
		{
			mProjection = Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
		}

		public void InitOrtho(float w, float h, float near = 0.1f, float far = 1000)
		{
			mProjection = Matrix4.CreateOrthographic(w, h, near, far);
		}

		public void UpdateUBO()
		{
			mUBufferData.View = ViewMatrix;
			mUBufferData.Projection = ProjectionMatrix;
			mUBufferData.Position = Transform.LocalPosition;
			mCameraUBO.Update(mUBufferData);
		}
		
	}
}
