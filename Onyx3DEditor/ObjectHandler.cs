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
	private HandlerOperator mOperator = new ObjectHandlerTranslate();
	private float mScale;

	public float Scale
	{
		get { return mScale; }
	}
	
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
				mOperator.Handle( clickRay, mScale);
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

		if (!IsHandling)
			mScale = Math.Max(0.1f, Vector3.Distance(mCamera.Transform.Position, mObject.Transform.Position) * 0.25f);

		mOperator.Update(mObject, mScale);
		mOperator.DrawGizmos(mObject, mOnyx3DInstance.Gizmos, mScale);
	}

}


// --------------------------------------------------------------------

abstract class HandlerOperator
{

	protected Sphere mXAxisBound;
	protected Sphere mYAxisBound;
	protected Sphere mZAxisBound;

	protected Vector3 SelectedAxis = Vector3.Zero;
	

	public void CheckHandleStart(SceneObject obj, Ray ray)
	{
		CheckSelectedAxis(obj, ray);

		if (IsHandling)
			OnHandleStart(obj);
	}


	public bool IsHandling
	{
		get { return SelectedAxis != Vector3.Zero; }
	}

	public void StopHandling()
	{
		SelectedAxis = Vector3.Zero;
	}

	public virtual void Update(SceneObject obj, float scale)
	{
		Vector3 position = obj.Transform.Position;
		mXAxisBound.Set(position + Vector3.UnitX * scale, 0.25f);
		mYAxisBound.Set(position + Vector3.UnitY * scale, 0.25f);
		mZAxisBound.Set(position + Vector3.UnitZ * scale, 0.25f);
	}

	virtual public void DrawGizmos(SceneObject obj, GizmosManager gizmos, float scale)
	{

		mXAxisBound.Radius = scale * 0.15f;
		mYAxisBound.Radius = scale * 0.15f;
		mZAxisBound.Radius = scale * 0.15f;

		gizmos.DrawWireSphere(mXAxisBound, SelectedAxis == Vector3.UnitX ? Vector3.UnitY : Vector3.Zero);
		gizmos.DrawWireSphere(mYAxisBound, SelectedAxis == Vector3.UnitY ? Vector3.UnitY : Vector3.Zero);
		gizmos.DrawWireSphere(mZAxisBound, SelectedAxis == Vector3.UnitZ ? Vector3.UnitY : Vector3.Zero);

	}

	virtual public void CheckSelectedAxis(SceneObject obj, Ray ray)
	{
		if (mXAxisBound.IntersectsRay(ray))
			SelectedAxis = Vector3.UnitX;
		else if (mYAxisBound.IntersectsRay(ray))
			SelectedAxis = Vector3.UnitY;
		else if (mZAxisBound.IntersectsRay(ray))
			SelectedAxis = Vector3.UnitZ;
	}

	abstract public void Handle(Ray ray, float scale);
	virtual protected void OnHandleStart(SceneObject obj) { }
}

// --------------------------------------------------------------------

class ObjectHandlerTranslate : HandlerOperator
{

	public override void DrawGizmos(SceneObject obj, GizmosManager gizmos, float scale)
	{
		base.DrawGizmos(obj, gizmos, scale);
		gizmos.DrawAxis(obj.Transform.Position, scale);
	}

	public override void Handle(Ray ray, float scale)
	{
		SceneObject obj = Selection.ActiveObject;

		Ray axisRay = new Ray(obj.Transform.Position, SelectedAxis);

		Vector3 closestPointToAxis = axisRay.ClosestPointTo(ray);
		Vector3 axisPoint = (obj.Transform.Position + SelectedAxis  * scale);

		Vector3 offset = axisPoint - closestPointToAxis;
		if (Control.ModifierKeys.HasFlag(Keys.Control))
		{
			offset.X = (float)Math.Round(offset.X * 4f) / 4f;
			offset.Y = (float)Math.Round(offset.Y * 4f) / 4f;
			offset.Z = (float)Math.Round(offset.Z * 4f) / 4f;
		}

		//obj.Transform.LocalPosition -= offset;
		foreach (SceneObject selObj in Selection.Selected)
		{
			if (!Selection.Selected.Contains(selObj.Parent))
				selObj.Transform.LocalPosition -= offset;
		}

	}
}

// --------------------------------------------------------------------

class ObjectHandlerScale : HandlerOperator
{
	private Bounds mXYZAxisBound;

	private Vector3 mScale;
	private Vector3 mInitAxisPoint;

	protected override void OnHandleStart(SceneObject obj)
	{
		mScale = obj.Transform.LocalScale;

	}

	public override void DrawGizmos(SceneObject obj, GizmosManager gizmos, float scale)
	{
		base.DrawGizmos(obj, gizmos, scale);

		gizmos.DrawBox(mXYZAxisBound.Center, Vector3.One * 0.1f, Vector3.One);

		gizmos.DrawLine(obj.Transform.Position, obj.Transform.Position + Vector3.UnitX * scale, Vector3.UnitX);
		gizmos.DrawLine(obj.Transform.Position, obj.Transform.Position + Vector3.UnitY * scale, Vector3.UnitY);
		gizmos.DrawLine(obj.Transform.Position, obj.Transform.Position + Vector3.UnitZ * scale, Vector3.UnitZ);
		gizmos.DrawBox(obj.Transform.Position + Vector3.UnitX * scale, Vector3.One * 0.1f * scale, Vector3.UnitX);
		gizmos.DrawBox(obj.Transform.Position + Vector3.UnitY * scale, Vector3.One * 0.1f * scale, Vector3.UnitY);
		gizmos.DrawBox(obj.Transform.Position + Vector3.UnitZ * scale, Vector3.One * 0.1f * scale, Vector3.UnitZ);
	}

	public override void Handle( Ray ray, float scale)
	{
		SceneObject obj = Selection.ActiveObject;

		Vector3 newScale = mScale;
		Vector3 closestPointToAxis = GetClosestPointToAxis(obj, ray, SelectedAxis);
		if (SelectedAxis == Vector3.One)
		{
			float scaleY = closestPointToAxis.Y / mInitAxisPoint.Y;
			newScale = new Vector3(scaleY);
		}
		else
		{
			newScale = new Vector3(
				SelectedAxis == Vector3.UnitX ? closestPointToAxis.X / mInitAxisPoint.X : 1.0f,
				SelectedAxis == Vector3.UnitY ? closestPointToAxis.Y / mInitAxisPoint.Y : 1.0f,
				SelectedAxis == Vector3.UnitZ ? closestPointToAxis.Z / mInitAxisPoint.Z : 1.0f);
		}

		//foreach (SceneObject selObj in Selection.Selected)
		//	selObj.Transform.LocalScale = mScale * newScale;

		obj.Transform.LocalScale = mScale * newScale;
	}


	public override void Update(SceneObject obj, float scale)
	{
		base.Update(obj, scale);

		mXYZAxisBound.SetMinMax(obj.Transform.Position - Vector3.One * 0.1f, obj.Transform.Position + Vector3.One * 0.1f);
	}

	public override void CheckSelectedAxis(SceneObject obj, Ray ray)
	{
		if (mXYZAxisBound.IntersectsRay(ray))
			SelectedAxis = Vector3.One;

		base.CheckSelectedAxis(obj, ray);

		if (SelectedAxis != Vector3.Zero)
		{
			if (SelectedAxis == Vector3.One)
				mInitAxisPoint = obj.Transform.Position;
			else
				mInitAxisPoint = GetClosestPointToAxis(obj, ray, SelectedAxis);
		}
	}
	
	private Vector3 GetClosestPointToAxis(SceneObject obj, Ray ray, Vector3 axis)
	{
		Ray axisRay = new Ray(obj.Transform.Position, axis);
		return axisRay.ClosestPointTo(ray);
	}

}

// --------------------------------------------------------------------

class ObjectHandlerRotate : HandlerOperator
{

	private Quaternion mRotation;

	private Vector3 closestPoint;

	private Vector3 hitPoint;
	private Vector3 initDir;
	private Vector3 hitDir;

	protected override void OnHandleStart(SceneObject obj)
	{

		mRotation = obj.Transform.LocalRotation;
	}

	public override void DrawGizmos(SceneObject obj, GizmosManager gizmos, float scale)
	{
		base.DrawGizmos(obj, gizmos, scale);

		Vector3 up = Vector3.UnitY;
		Vector3 right = Vector3.UnitX;
		Vector3 forward = Vector3.UnitZ;

		gizmos.DrawCircle(obj.Transform.Position, 0.5f, Vector3.UnitX, up);
		gizmos.DrawCircle(obj.Transform.Position, 0.5f, Vector3.UnitY, right);		
		gizmos.DrawCircle(obj.Transform.Position, 0.5f, Vector3.UnitZ, forward);

		gizmos.DrawLine(obj.Transform.Position + right * 0.5f, obj.Transform.Position + right, Vector3.UnitZ);
		gizmos.DrawLine(obj.Transform.Position + up * 0.5f, obj.Transform.Position + up, Vector3.UnitY);
		gizmos.DrawLine(obj.Transform.Position + forward * 0.5f, obj.Transform.Position + forward, Vector3.UnitX);

		gizmos.DrawWireSphere(obj.Transform.Position + right, 0.05f, Vector3.UnitZ);
		gizmos.DrawWireSphere(obj.Transform.Position + up, 0.05f, Vector3.UnitY);
		gizmos.DrawWireSphere(obj.Transform.Position + forward, 0.05f, Vector3.UnitX);

		

		//gizmos.DrawLine(obj.Transform.Position, closestPoint, Vector3.One);

		gizmos.DrawWireSphere(hitPoint, 0.25f, Vector3.One);

		gizmos.DrawLine(obj.Transform.Position, obj.Transform.Position + hitDir * 2, Vector3.UnitX);
	}

	public override void Update(SceneObject obj, float scale)
	{
		base.Update(obj, scale);
		Vector3 position = obj.Transform.Position;
		
		mXAxisBound.Set(position + Vector3.UnitY, 0.25f);
		mYAxisBound.Set(position + Vector3.UnitZ, 0.25f);
		mZAxisBound.Set(position + Vector3.UnitX, 0.25f);
		
	}

	public override void Handle(Ray ray, float scale)
	{
		SceneObject obj = Selection.ActiveObject;

		Ray axisRay = new Ray(obj.Transform.Position, SelectedAxis);
		closestPoint = axisRay.ClosestPointTo(ray);
		Vector3 dirToClosest = (closestPoint - obj.Transform.Position) * SelectedAxis;

		hitPoint = GetAxisPlaneHitPoint(obj, ray);
		hitDir = (hitPoint - obj.Transform.Position);


		//Vector3 axisPoint = (obj.Transform.Position + SelectedAxis);
		//Quaternion prevAngle = obj.Transform.LocalRotation;

		
		float angleOffset = initDir.CalculateAngleTo(hitDir, SelectedAxis); //dirToClosest.Length * 0.5f * Vector3.Dot(SelectedAxis, dirToClosest);
		Vector4 rotation = obj.Transform.GetRotationMatrix() * (new Vector4(SelectedAxis * angleOffset, 1));

		//obj.Transform.LocalRotation = mRotation * Quaternion.FromEulerAngles(rotation.Xyz);

		foreach (SceneObject selObj in Selection.Selected)
			selObj.Transform.LocalRotation = mRotation * Quaternion.FromEulerAngles(rotation.Xyz);

		mRotation = obj.Transform.LocalRotation;
		initDir = hitDir;
	}

	private Vector3 GetAxisPlaneHitPoint(SceneObject obj, Ray ray)
	{
		Vector3 axis = SelectedAxis;// obj.Transform.LocalToWorld(SelectedAxis).Normalized();
		Plane p1 = new Plane((obj.Transform.Position * axis).Length, axis);
		
		float dist;
		if (p1.IntersectsRay(ray, out dist))
		{
			return ray.Origin + ray.Direction.Normalized() * dist;
		}
		/*
		Plane p2 = new Plane((obj.Transform.Position * axis).Length, -axis);
		if (p2.IntersectsRay(ray, out dist))
		{
			return ray.Origin + ray.Direction.Normalized() * dist;
		}
		*/
		return Vector3.Zero;
	}

	public override void CheckSelectedAxis(SceneObject obj, Ray ray)
	{

		base.CheckSelectedAxis(obj, ray);

		Vector3 hitPoint = GetAxisPlaneHitPoint(obj, ray);
		initDir = (hitPoint - obj.Transform.Position);

		/*
		if (SelectedAxis == Vector3.UnitX) SelectedAxis = Vector3.UnitY;
		else if (SelectedAxis == Vector3.UnitY) SelectedAxis = Vector3.UnitZ;
		else if (SelectedAxis == Vector3.UnitZ) SelectedAxis = Vector3.UnitX;
		*/
	}

}

