using System;
using System.Windows.Forms;
using Onyx3D;
using OpenTK;

class ObjectHandler
{
	public Action OnTransformModified;


	public enum HandlerAxisAction
	{
		None,
		Translate,
		Scale,
		Rotate
	};
	
	private Onyx3DInstance mOnyx3DInstance;
	private GLControl mRenderCanvas;
	private Camera mCamera;
	private SceneObject mObject;
	private Vector3 mScale;
	private HandlerOperator mOperator = new ObjectHandlerTranslate();

	
	public bool IsHandling
	{
		get { return mOperator.IsHandling; }
	}
	
	public ObjectHandler(Onyx3DInstance onyx3d, GLControl renderCanvas, Camera cam)
	{
		mOnyx3DInstance = onyx3d;
		mRenderCanvas = renderCanvas;
		mCamera = cam;

		mRenderCanvas.MouseMove += OnMouseMove;
		mRenderCanvas.MouseUp += OnMouseUp;
	}

	public void HandleObject(SceneObject obj)
	{
		mObject = obj;
	}

	private void OnMouseUp(object sender, MouseEventArgs e)
	{
		if (IsHandling)
		{
			OnTransformModified?.Invoke();
			mOperator.StopHandling();
		}
	}

	private void OnMouseMove(object sender, MouseEventArgs e)
	{
		if (mObject == null)
			return;

		if (e.Button == MouseButtons.Left)
		{
			Ray clickRay = mCamera.ScreenPointToRay(e.X, e.Y, mRenderCanvas.Width, mRenderCanvas.Height);
			
			if (IsHandling)
				mOperator.Handle(mObject, clickRay);
			else
				mOperator.CheckHandleStart(mObject, clickRay);
		}
	}

	public void SetAxisAction(HandlerAxisAction action)
	{
		switch (action)
		{
			case HandlerAxisAction.Scale:
				mOperator = new ObjectHandlerScale();
				break;
			case HandlerAxisAction.Translate:
				mOperator = new ObjectHandlerTranslate();
				break;
			case HandlerAxisAction.Rotate:
				mOperator = new ObjectHandlerRotate();
				break;
		}
	}


	public void Update()
	{
		if (mObject == null)
			return;

		mOperator.Update(mObject);
		mOperator.DrawGizmos(mObject, mOnyx3DInstance.Gizmos);
	}

}


// --------------------------------------------------------------------

abstract class HandlerOperator
{

	private Bounds mXAxisBound;
	private Bounds mYAxisBound;
	private Bounds mZAxisBound;

	private Vector3 mSelectedAxis = Vector3.Zero;

	public Vector3 SelectedAxis
	{
		get { return mSelectedAxis; }
	}

	public void CheckHandleStart(SceneObject obj, Ray ray)
	{
		if (mXAxisBound.IntersectsRay(ray))
			mSelectedAxis = Vector3.UnitX;
		else if (mYAxisBound.IntersectsRay(ray))
			mSelectedAxis = Vector3.UnitY;
		else if (mZAxisBound.IntersectsRay(ray))
			mSelectedAxis = Vector3.UnitZ;

		if (IsHandling)
			OnHandleStart(obj);
	}

	public bool IsHandling
	{
		get { return mSelectedAxis != Vector3.Zero; }
	}

	public void StopHandling()
	{
		mSelectedAxis = Vector3.Zero;
	}

	public void Update(SceneObject obj)
	{
		Vector3 position = obj.Transform.Position;
		mXAxisBound.SetMinMax(position + Vector3.UnitX - Vector3.One * 0.1f, position + Vector3.UnitX + Vector3.One * 0.1f);
		mYAxisBound.SetMinMax(position + Vector3.UnitY - Vector3.One * 0.1f, position + Vector3.UnitY + Vector3.One * 0.1f);
		mZAxisBound.SetMinMax(position + Vector3.UnitZ - Vector3.One * 0.1f, position + Vector3.UnitZ + Vector3.One * 0.1f);
	}

	virtual public void DrawGizmos(SceneObject obj, GizmosManager gizmos)
	{
		gizmos.DrawBox(mXAxisBound, Vector3.Zero, mSelectedAxis == Vector3.UnitX ? Vector3.UnitY : Vector3.Zero);
		gizmos.DrawBox(mYAxisBound, Vector3.Zero, mSelectedAxis == Vector3.UnitY ? Vector3.UnitY : Vector3.Zero);
		gizmos.DrawBox(mZAxisBound, Vector3.Zero, mSelectedAxis == Vector3.UnitZ ? Vector3.UnitY : Vector3.Zero);
	}

	abstract public void Handle(SceneObject obj, Ray ray);
	virtual protected void OnHandleStart(SceneObject obj) { }
}

// --------------------------------------------------------------------

class ObjectHandlerTranslate : HandlerOperator
{

	public override void DrawGizmos(SceneObject obj, GizmosManager gizmos)
	{
		base.DrawGizmos(obj, gizmos);
		gizmos.DrawAxis(obj.Transform.Position);
	}

	public override void Handle(SceneObject obj, Ray ray)
	{
		Ray axisRay = new Ray(obj.Transform.Position, SelectedAxis);
		Vector3 closestPointToAxis = axisRay.ClosestPointTo(ray);
		Vector3 axisPoint = (obj.Transform.Position + SelectedAxis);

		Vector3 offset = axisPoint - closestPointToAxis;
		obj.Transform.LocalPosition -= offset;

	}
}

// --------------------------------------------------------------------

class ObjectHandlerScale : HandlerOperator
{

	private Vector3 mScale;

	protected override void OnHandleStart(SceneObject obj)
	{
		mScale = obj.Transform.LocalScale;
	}

	public override void DrawGizmos(SceneObject obj, GizmosManager gizmos)
	{
		base.DrawGizmos(obj, gizmos);
		gizmos.DrawAxis(obj.Transform.Position);
	}

	public override void Handle(SceneObject obj, Ray ray)
	{
		Ray axisRay = new Ray(obj.Transform.Position, SelectedAxis);
		Vector3 closestPointToAxis = axisRay.ClosestPointTo(ray);
		Vector3 axisPoint = (obj.Transform.Position + SelectedAxis);
		Vector3 scale = new Vector3(
			SelectedAxis == Vector3.UnitX ? closestPointToAxis.X / axisPoint.X : 1.0f,
			SelectedAxis == Vector3.UnitY ? closestPointToAxis.Y / axisPoint.Y : 1.0f,
			SelectedAxis == Vector3.UnitZ ? closestPointToAxis.Z / axisPoint.Z : 1.0f);

		obj.Transform.LocalScale = mScale * scale;
	}

}

// --------------------------------------------------------------------

class ObjectHandlerRotate : HandlerOperator
{

	private Vector3 mScale;

	protected override void OnHandleStart(SceneObject obj)
	{
		mScale = obj.Transform.LocalScale;
	}

	public override void DrawGizmos(SceneObject obj, GizmosManager gizmos)
	{
		base.DrawGizmos(obj, gizmos);

		gizmos.DrawCircle(0.5f, obj.Transform.Position, Vector3.UnitY, Vector3.UnitY);
		gizmos.DrawCircle(0.5f, obj.Transform.Position, Vector3.UnitZ, Vector3.UnitZ);		
		gizmos.DrawCircle(0.5f, obj.Transform.Position, Vector3.UnitX, Vector3.UnitX);
	}

	public override void Handle(SceneObject obj, Ray ray)
	{
		Ray axisRay = new Ray(obj.Transform.Position, SelectedAxis);
		Vector3 closestPointToAxis = axisRay.ClosestPointTo(ray);
		Vector3 axisPoint = (obj.Transform.Position + SelectedAxis);
		Vector3 scale = new Vector3(
			SelectedAxis == Vector3.UnitX ? closestPointToAxis.X / axisPoint.X : 1.0f,
			SelectedAxis == Vector3.UnitY ? closestPointToAxis.Y / axisPoint.Y : 1.0f,
			SelectedAxis == Vector3.UnitZ ? closestPointToAxis.Z / axisPoint.Z : 1.0f);

		obj.Transform.LocalScale = mScale * scale;
	}

}

