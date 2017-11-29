using System.Drawing;
using OpenTK;
using Onyx3D;
using System.Windows.Forms;
using System;

namespace Onyx3DEditor
{

	public class OnyxViewerNavigation
	{

		const float DragFactor = 0.05f;
		const float RotationFactor = 0.01f;
		const float ZoomFactor = 0.005f;

		public enum NavigationAction
		{
			None,
			Rotating,
			Dragging,
		}

		public NavigationAction CurrentAction;

		public Vector2 MouseOffset { get; private set; }

		private Vector2 mCurrentMousePos;
		public Camera NavigationCamera;
		//private GLControl mRenderCanvas;

		public OnyxViewerNavigation()
		{
			MouseOffset = new Vector2(0, 0);
		}

		public void CreateCamera()
		{
			SceneObject mCameraPivot = new SceneObject("CameraPivot");
			mCameraPivot.Transform.LocalPosition = new Vector3(0, 0.5f, 3);

			NavigationCamera = new Camera("MainCamera");
			NavigationCamera.Parent = mCameraPivot;
		}

		public void UpdateMousePosition(Point pos)
		{
			Vector2 newPosV2 = new Vector2(pos.X, pos.Y);
			MouseOffset = newPosV2 - mCurrentMousePos;
			mCurrentMousePos = newPosV2;
		}

		public void UpdateCamera()
		{
			switch (CurrentAction)
			{
				case NavigationAction.Dragging:
					DragCamera();
					break;
				case NavigationAction.Rotating:
					RotateCamera();
					break;
			}
			
		}

		private void DragCamera()
		{
			Vector3 camPos = NavigationCamera.Parent.Transform.LocalPosition;
			Vector3 translation = new Vector3(-MouseOffset.X * DragFactor, MouseOffset.Y * DragFactor, 0);
			translation = Vector3.TransformVector(translation, NavigationCamera.Parent.Transform.GetRotationMatrix());
			
			
			camPos += translation;

			NavigationCamera.Parent.Transform.LocalPosition = camPos;
		}

		private void RotateCamera()
		{
			NavigationCamera.Parent.Transform.Rotate(Quaternion.FromEulerAngles(new Vector3(0,-MouseOffset.X * RotationFactor, 0)));
			NavigationCamera.Transform.Rotate(Quaternion.FromEulerAngles(new Vector3(0, 0,-MouseOffset.Y * RotationFactor)));
		}

		#region Mouse Interaction

		public void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Middle)
				CurrentAction = NavigationAction.Dragging;
			else if (e.Button == MouseButtons.Right)
				CurrentAction = NavigationAction.Rotating;
			else
				CurrentAction = NavigationAction.None;
		}

		public void OnMouseUp(object sender, MouseEventArgs e)
		{
			CurrentAction = NavigationAction.None;
		}

		public void OnMouseMove(object sender, MouseEventArgs e)
		{
			UpdateMousePosition(e.Location);
			if (e.Button != MouseButtons.None) {
				GLControl renderCanvas = sender as GLControl;
				if (renderCanvas != null)
					renderCanvas.Refresh();
			}
		}

		public void OnMouseWheel(object sender, MouseEventArgs e)
		{
			float change = -e.Delta;
			Vector3 forwardCam = Vector3.TransformVector(Vector3.UnitZ, NavigationCamera.Transform.GetRotationMatrix());
			Vector3 forwardCamParent = Vector3.TransformVector(Vector3.UnitZ, NavigationCamera.Parent.Transform.GetRotationMatrix());
			forwardCamParent.Y = forwardCam.Y;
			NavigationCamera.Parent.Transform.Translate(forwardCamParent * change * ZoomFactor);
			GLControl renderCanvas = sender as GLControl;
			if (renderCanvas != null)
				renderCanvas.Refresh();
		}

		#endregion
	}


}
