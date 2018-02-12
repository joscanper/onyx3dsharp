using System;

using OpenTK;

namespace Onyx3D
{

	public struct CameraUBufferData
	{
		public Matrix4 View;
		public Matrix4 Projection;
		public Vector3 Position;
	}

	[Serializable]
	public class Camera : SceneObject
	{
		public float Near;
		public float Far;

		protected Matrix4 mProjection;

		UBO<CameraUBufferData> mCameraUBO;
		CameraUBufferData mUBufferData;

		public UBO<CameraUBufferData> UBO { get { return mCameraUBO; } }
		
		public Matrix4 ViewMatrix
		{
			get {
				return Transform.ModelMatrix.Inverted();
			}
		}

		public Matrix4 ProjectionMatrix
		{
			get { return mProjection; }
		}


		public Camera(string id, float near = 0.1f, float far = 1000) : base(id)
		{
			Near = near;
			Far = far;
			mUBufferData = new CameraUBufferData();
			mCameraUBO = new UBO<CameraUBufferData>(mUBufferData, "CameraData");
		}

		
		public void UpdateUBO()
		{
			mUBufferData.View = ViewMatrix;
			mUBufferData.Projection = ProjectionMatrix;
			mUBufferData.Position = Transform.LocalToWorld(Transform.LocalPosition);
			mCameraUBO.Update(mUBufferData);
		}

		public virtual void Update()
		{
			UpdateUBO();
		}
		
		public Ray ViewportPointToRay(Vector2 viewportPoint)
		{
			Ray r = new Ray();
			r.Origin = Transform.Position;
			r.Direction = new Vector3( -Vector4.UnitZ * Transform.ModelMatrix);
			return r;
		}
	}


	[Serializable]
	public class PerspectiveCamera : Camera
	{
		public float FOV;
		public float Aspect;

		public PerspectiveCamera(string id, float fov, float aspect, float near = 0.1f, float far = 1000) : base(id, near, far)
		{
			FOV = fov;
			Aspect = aspect;
			Update();
		}

		public override void Update()
		{
			base.Update();
			mProjection = Matrix4.CreatePerspectiveFieldOfView(FOV, Aspect, Near, Far);
		}
	}

	[Serializable]
	public class OrthoCamera : Camera
	{
		public float W;
		public float H;
		
		public OrthoCamera(string id, float w, float h, float near = 0.1f, float far = 1000) : base(id, near, far)
		{
			W = w;
			H = h;
			Update();
		}

		public override void Update()
		{
			base.Update();
			mProjection = Matrix4.CreateOrthographic(W, H, Near, Far);
		}
	}
}
