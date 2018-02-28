using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Onyx3D;
using OpenTK;

class ObjectHandler
{
	private Bounds mXAxisBound;
	private Bounds mYAxisBound;
	private Bounds mZAxisBound;

	private int mSelectedAxis = 0;

	private Onyx3DInstance mOnyx3DInstance;
	private GLControl mRenderCanvas;
	private Camera mCamera;

	private Vector3 mPosition;

	public ObjectHandler(Onyx3DInstance onyx3d, GLControl renderCanvas, Camera cam)
	{
		mOnyx3DInstance = onyx3d;
		mRenderCanvas = renderCanvas;
		mCamera = cam;

		mRenderCanvas.MouseDown += OnMouseDown;
		mRenderCanvas.MouseUp += OnMouseUp;
	}

	private void OnMouseUp(object sender, MouseEventArgs e)
	{
		mSelectedAxis = 0;

	}

	private void OnMouseDown(object sender, MouseEventArgs e)
	{
		
		if (mSelectedAxis == 0)
		{
			Ray myClickRay = mCamera.ScreenPointToRay(e.X, e.Y, mRenderCanvas.Width, mRenderCanvas.Height);

			if (mXAxisBound.IntersectsRay(myClickRay))
				mSelectedAxis = 1;
			else if (mYAxisBound.IntersectsRay(myClickRay))
				mSelectedAxis = 2;
			else if (mZAxisBound.IntersectsRay(myClickRay))
				mSelectedAxis = 3;
		}
	}

	public void Update(Vector3 position)
	{
		mPosition = position;
		mXAxisBound.SetMinMax(position + Vector3.UnitX - Vector3.One * 0.1f, position + Vector3.UnitX + Vector3.One * 0.1f);
		mYAxisBound.SetMinMax(position + Vector3.UnitY - Vector3.One * 0.1f, position + Vector3.UnitY + Vector3.One * 0.1f);
		mZAxisBound.SetMinMax(position + Vector3.UnitZ - Vector3.One * 0.1f, position + Vector3.UnitZ + Vector3.One * 0.1f);
	}

	public void Render()
	{
		mOnyx3DInstance.Gizmos.DrawAxis(mPosition);
		mOnyx3DInstance.Gizmos.DrawBox(mXAxisBound, Vector3.Zero, mSelectedAxis == 1 ? Vector3.UnitY : Vector3.Zero);
		mOnyx3DInstance.Gizmos.DrawBox(mYAxisBound, Vector3.Zero, mSelectedAxis == 2 ? Vector3.UnitY : Vector3.Zero);
		mOnyx3DInstance.Gizmos.DrawBox(mZAxisBound, Vector3.Zero, mSelectedAxis == 3 ? Vector3.UnitY : Vector3.Zero);
	}

}

