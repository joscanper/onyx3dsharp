using System.Drawing;
using OpenTK;
using Onyx3D;
using System.Windows.Forms;
using System;

namespace Onyx3DEditor
{

	public class OnyxViewerNavigation
	{

        GLControl mBoundControl;

		const char FORWARDS_CHAR = 'w';
		const char LEFT_CHAR = 'a';
		const char RIGHT_CHAR = 'd';
		const char BACKWARDS_CHAR = 's';

		const float DragFactor = 0.005f;
		const float RotationFactor = 0.005f;
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
		public PerspectiveCamera NavigationCamera;

		public OnyxViewerNavigation()
		{
			MouseOffset = new Vector2(0, 0);
		}

		public void Bind(GLControl renderCanvas)
		{
            mBoundControl = renderCanvas;
            renderCanvas.MouseDown += new MouseEventHandler(OnMouseDown);
			renderCanvas.MouseUp += new MouseEventHandler(OnMouseUp);
			renderCanvas.MouseMove += new MouseEventHandler(OnMouseMove);
			renderCanvas.MouseWheel += new MouseEventHandler(OnMouseWheel);
			renderCanvas.KeyPress += new KeyPressEventHandler(OnKeyPress);
		}

		public void CreateCamera()
		{
			SceneObject mCameraPivot = new SceneObject("CameraPivot");
			mCameraPivot.Transform.LocalPosition = new Vector3(0, 0.5f, 3);

            NavigationCamera = new PerspectiveCamera("MainCamera", MathHelper.DegreesToRadians(60), 1.5f);
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
            NavigationCamera.Aspect = (float)mBoundControl.Width / (float)mBoundControl.Height;

            switch (CurrentAction)
			{
				case NavigationAction.Dragging:
					DragCamera();
					break;
				case NavigationAction.Rotating:
					RotateCamera();
					break;
			}
			NavigationCamera.Update();

		}

		private void DragCamera()
		{
			Vector3 translation = new Vector3(-MouseOffset.X * DragFactor, MouseOffset.Y * DragFactor, 0);
			MoveCamera(translation);
		}

		private void MoveCamera(Vector3 translation)
		{
			Vector3 camPos = NavigationCamera.Parent.Transform.LocalPosition;
			translation = Vector3.TransformVector(translation, NavigationCamera.Transform.GetRotationMatrix());
			translation = Vector3.TransformVector(translation, NavigationCamera.Parent.Transform.GetRotationMatrix());
			camPos += translation;
			NavigationCamera.Parent.Transform.LocalPosition = camPos;
		}

		private void RotateCamera()
		{
			NavigationCamera.Parent.Transform.Rotate(Quaternion.FromEulerAngles(new Vector3(0,-MouseOffset.X * RotationFactor, 0)));
			NavigationCamera.Transform.Rotate(Quaternion.FromEulerAngles(new Vector3(-MouseOffset.Y * RotationFactor, 0, 0)));
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
			MoveCamera(Vector3.UnitZ * (-e.Delta) * ZoomFactor);
			GLControl renderCanvas = sender as GLControl;
			if (renderCanvas != null)
				renderCanvas.Refresh();
		}

		public void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			MouseOffset = Vector2.Zero;

			if (e.KeyChar.Equals(RIGHT_CHAR))
				MoveCamera(Vector3.UnitX * DragFactor * 10);
			else if (e.KeyChar.Equals(BACKWARDS_CHAR))
				MoveCamera(Vector3.UnitZ * DragFactor * 10);
			else if (e.KeyChar.Equals(FORWARDS_CHAR))
				MoveCamera(-Vector3.UnitZ * DragFactor * 10);
			else if (e.KeyChar.Equals(LEFT_CHAR))
				MoveCamera(-Vector3.UnitX * DragFactor * 10);

			GLControl renderCanvas = sender as GLControl;
			if (renderCanvas != null)
				renderCanvas.Refresh();
		}

		#endregion
	}


}
