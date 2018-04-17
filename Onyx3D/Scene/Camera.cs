using System;

using OpenTK;
using System.Runtime.InteropServices;

namespace Onyx3D
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CameraUBufferData
	{
		public Matrix4 View;
		public Matrix4 Projection;
		public Vector4 Position;
	}

	[Serializable]
	public abstract class Camera : SceneObject
	{
		public float Near;
		public float Far;
		//public Rect Viewport = new Rect(0, 0, 1, 1);

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
			mUBufferData.Position = new Vector4(Transform.Position, 1);
			mCameraUBO.Update(mUBufferData);
		}

		public virtual void Update()
		{
			UpdateUBO();
		}

        public Ray ScreenPointToRay(float x, float y, float screenW, float screenH)
        {
            Vector2 viewportPoint = new Vector2(x / screenW, y / screenH);
            viewportPoint.X = (viewportPoint.X - 0.5f) * 2;
            viewportPoint.Y = -(viewportPoint.Y - 0.5f) * 2;
            return ViewportPointToRay(viewportPoint);
        }


        public abstract Ray ViewportPointToRay(Vector2 viewportPoint);

    }


	[Serializable]
	public class PerspectiveCamera : Camera
	{
		public float FOV;
		public float Aspect;

		public PerspectiveCamera(string id, float fov, float aspect, float near = 0.01f, float far = 1000) : base(id, near, far)
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


        public override Ray ViewportPointToRay(Vector2 viewportPoint)
        {
            Vector3 nearPlanePoint = new Vector3();
            float nearPlaneHalfH = Near * (float)Math.Tan(FOV / 2.0f);
            nearPlanePoint.X = nearPlaneHalfH * Aspect * viewportPoint.X;
            nearPlanePoint.Y = nearPlaneHalfH * viewportPoint.Y;
            nearPlanePoint.Z = -Near;

            nearPlanePoint = Transform.LocalToWorld(nearPlanePoint);

            Ray r = new Ray()
            {
                Origin = nearPlanePoint,
                Direction = (nearPlanePoint - Transform.Position).Normalized()
            };

            return r;
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

        public override Ray ViewportPointToRay(Vector2 viewportPoint)
        {
            Vector3 nearPlanePoint = new Vector3();
            
            nearPlanePoint.X = W / 2.0f * viewportPoint.X;
            nearPlanePoint.Y = H / 2.0f * viewportPoint.Y;
            nearPlanePoint.Z = -Near;

            nearPlanePoint = Transform.LocalToWorld(nearPlanePoint);

            Ray r = new Ray()
            {
                Origin = nearPlanePoint,
                Direction = -Transform.Forward
            };

            return r;
        }
    }
}
