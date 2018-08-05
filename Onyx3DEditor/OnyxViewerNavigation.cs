using System.Drawing;
using OpenTK;
using Onyx3D;
using System.Windows.Forms;
using System;

namespace Onyx3DEditor
{

	public class OnyxViewerNavigation
	{

        public enum NavigationAction
        {
            None,
            Rotating,
            Dragging,
        }

        private static readonly char FORWARDS_CHAR = 'w';
        private static readonly char LEFT_CHAR = 'a';
        private static readonly char RIGHT_CHAR = 'd';
        private static readonly char BACKWARDS_CHAR = 's';
        private static readonly char FOCUS_CHAR = 'f';

        private static readonly char VIEW_FRONT_CHAR = '1';
        private static readonly char VIEW_RIGHT_CHAR = '2';
        private static readonly char VIEW_TOP_CHAR = '3';

        private static readonly char VIEW_PERSPECTIVE_CHAR = '5';

		const float DragFactor = 0.005f;
		const float RotationFactor = 0.005f;
		const float ZoomFactor = 0.005f;

        // --------------------------------------------------------------------

        private SceneObject mCameraPivotTop;
		private SceneObject mCameraPivotXZRot;
		private SceneObject mCameraPivotZMov;

        private GLControl mBoundControl;
        private Vector2 mCurrentMousePos;
        private PerspectiveCamera mNavigationCamera;
		private OrthoCamera mOrthoCamera;
		private Camera mSelectedCamera;

		public Camera Camera { get { return mSelectedCamera; } }

        public NavigationAction CurrentAction;

        public Vector2 MouseOffset { get; private set; }

        // --------------------------------------------------------------------

        public OnyxViewerNavigation()
		{
			MouseOffset = new Vector2(0, 0);
		}

        // --------------------------------------------------------------------

        public void Bind(GLControl renderCanvas)
		{
            mBoundControl = renderCanvas;
            renderCanvas.MouseDown += new MouseEventHandler(OnMouseDown);
			renderCanvas.MouseUp += new MouseEventHandler(OnMouseUp);
			renderCanvas.MouseMove += new MouseEventHandler(OnMouseMove);
			renderCanvas.MouseWheel += new MouseEventHandler(OnMouseWheel);
            renderCanvas.KeyPress += new KeyPressEventHandler(OnKeyPress);
		}

        // --------------------------------------------------------------------

        public void CreateCamera()
		{
			mCameraPivotTop = new SceneObject("CameraPivotTop");

			mCameraPivotXZRot = new SceneObject("CameraPivotXZ");
			mCameraPivotXZRot.Parent = mCameraPivotTop;

			mCameraPivotZMov = new SceneObject("CameraPivotZMov");
			mCameraPivotZMov.Parent = mCameraPivotXZRot;

			mNavigationCamera = new PerspectiveCamera("MainCamera", MathHelper.DegreesToRadians(60), 1.5f);
			mNavigationCamera.Parent = mCameraPivotZMov;

			mOrthoCamera = new OrthoCamera("OrthoCamera", 5f, 5f);
			mOrthoCamera.Parent = mCameraPivotZMov;

			mCameraPivotTop.Transform.LocalPosition = new Vector3(0, 0.5f, 3);

			mSelectedCamera = mNavigationCamera;
		}

        // --------------------------------------------------------------------

        public void UpdateMousePosition(Point pos)
		{
			Vector2 newPosV2 = new Vector2(pos.X, pos.Y);
			MouseOffset = newPosV2 - mCurrentMousePos;
			mCurrentMousePos = newPosV2;
		}

        // --------------------------------------------------------------------

        public void UpdateCamera()
		{
            mNavigationCamera.Aspect = (float)mBoundControl.Width / (float)mBoundControl.Height;

            switch (CurrentAction)
			{
				case NavigationAction.Dragging:
					DragCamera();
					break;
				case NavigationAction.Rotating:
					RotateCamera();
					break;
			}
			mSelectedCamera.Update();

		}

        // --------------------------------------------------------------------

        private void DragCamera()
		{
			Vector3 translation = new Vector3(-MouseOffset.X * DragFactor, MouseOffset.Y * DragFactor, 0);
			MoveCamera(translation);
		}

        // --------------------------------------------------------------------

        private void MoveCamera(Vector3 translation)
		{
			Vector3 camPos = mCameraPivotTop.Transform.LocalPosition;
			translation = Vector3.TransformVector(translation, mCameraPivotXZRot.Transform.GetRotationMatrix());
			translation = Vector3.TransformVector(translation, mCameraPivotTop.Transform.GetRotationMatrix());
			camPos += translation;
			mCameraPivotTop.Transform.LocalPosition = camPos;
		}

        // --------------------------------------------------------------------

        private void RotateCamera()
		{
			mCameraPivotTop.Transform.Rotate(Quaternion.FromEulerAngles(new Vector3(0,-MouseOffset.X * RotationFactor, 0)));
			mCameraPivotXZRot.Transform.Rotate(Quaternion.FromEulerAngles(new Vector3(-MouseOffset.Y * RotationFactor, 0, 0)));
		}

        // --------------------------------------------------------------------

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

        // --------------------------------------------------------------------

        public void OnMouseUp(object sender, MouseEventArgs e)
		{
			CurrentAction = NavigationAction.None;
		}

        // --------------------------------------------------------------------

        public void OnMouseMove(object sender, MouseEventArgs e)
		{
			UpdateMousePosition(e.Location);
			if (e.Button != MouseButtons.None) {
				GLControl renderCanvas = sender as GLControl;
				if (renderCanvas != null)
					renderCanvas.Refresh();
			}
		}

        // --------------------------------------------------------------------

        public void OnMouseWheel(object sender, MouseEventArgs e)
		{
			mCameraPivotZMov.Transform.Translate(Vector3.UnitZ * (-e.Delta) * ZoomFactor);
			mOrthoCamera.W += (-e.Delta) * ZoomFactor;
			mOrthoCamera.H += (-e.Delta) * ZoomFactor;
			GLControl renderCanvas = sender as GLControl;
			if (renderCanvas != null)
				renderCanvas.Refresh();
		}

        // --------------------------------------------------------------------

        public void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			MouseOffset = Vector2.Zero;
            
			if (e.KeyChar.Equals(VIEW_FRONT_CHAR))
				SetView(Quaternion.Identity, Quaternion.Identity);
			else if (e.KeyChar.Equals(VIEW_RIGHT_CHAR))
				SetView(Quaternion.FromMatrix(new Matrix3(Matrix4.LookAt(Vector3.Zero, -Vector3.UnitX, Vector3.UnitY))), Quaternion.Identity);
			else if (e.KeyChar.Equals(VIEW_TOP_CHAR))
				SetView(Quaternion.Identity, Quaternion.FromMatrix(new Matrix3(Matrix4.LookAt(Vector3.Zero, -Vector3.UnitY, Vector3.UnitZ))));
			else if (e.KeyChar.Equals(VIEW_PERSPECTIVE_CHAR))
				mSelectedCamera = (mSelectedCamera == mNavigationCamera ? (Camera)mOrthoCamera : mNavigationCamera);
			else if (e.KeyChar.Equals(FOCUS_CHAR))
				FocusOnSelected();
            
			GLControl renderCanvas = sender as GLControl;
			if (renderCanvas != null)
				renderCanvas.Refresh();
		}
        
        // --------------------------------------------------------------------
        
        public void OnFrameTick()
        {
           OpenTK.Input.KeyboardState keyboardState = OpenTK.Input.Keyboard.GetState();

            bool keySDown = keyboardState.IsKeyDown(OpenTK.Input.Key.S);
            bool keyADown = keyboardState.IsKeyDown(OpenTK.Input.Key.A);
            bool keyWDown = keyboardState.IsKeyDown(OpenTK.Input.Key.W);
            bool keyDDown = keyboardState.IsKeyDown(OpenTK.Input.Key.D);

            if (keyDDown)
                MoveCamera(Vector3.UnitX * DragFactor * 10);
            if (keySDown)
                MoveCamera(Vector3.UnitZ * DragFactor * 10);
            if (keyWDown)
                MoveCamera(-Vector3.UnitZ * DragFactor * 10);
            if (keyADown)
                MoveCamera(-Vector3.UnitX * DragFactor * 10);
            
            if (keyADown || keyDDown || keySDown || keyWDown)
            {
                mBoundControl.Refresh();
            }
        }
        
		#endregion
		
		private void FocusOnSelected()
		{
			
			if (Selection.ActiveObject == null)
			{
				mCameraPivotTop.Transform.LocalPosition = mCameraPivotZMov.Transform.Position;
				mCameraPivotZMov.Transform.LocalPosition = Vector3.Zero;

			}
			else
			{

				mCameraPivotTop.Transform.LocalPosition = Selection.ActiveObject.Transform.Position;
				mCameraPivotZMov.Transform.LocalPosition = Vector3.UnitZ * 5f;
				
			}
		}

		private void SetView(Quaternion yRotation, Quaternion xzRotation)
		{
			mCameraPivotTop.Transform.LocalRotation = yRotation;
			mCameraPivotXZRot.Transform.LocalRotation = xzRotation;
		}
	}


}
