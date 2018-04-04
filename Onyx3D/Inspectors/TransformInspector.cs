using OpenTK;
using Onyx3D;
using System;
using System.ComponentModel;

namespace Onyx3D
{
	public class TransformInspector  : Inspector<Transform>
	{
		Vector3Inspector position;
		Vector3Inspector scale;
		Vector3Inspector rotation;

		public TransformInspector(Transform t) : base(t)
		{
			position = new Vector3Inspector(t.LocalPosition);
			scale = new Vector3Inspector(t.LocalScale);
			rotation = new Vector3Inspector(t.LocalRotation.ToDegEulerAngles());
			
		}
		
		

		[Category("Transform")]
		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Position {
			get { return position; }
			set { position = value; }
		}

		[Category("Transform")]
		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		[Category("Transform")]
		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		
		public override void Apply()
		{
			mObject.LocalPosition = Position.Object;
			mObject.LocalScale = Scale.Object;
			mObject.LocalRotation = mObject.LocalRotation.FromDegEulerAngles(Rotation.Object);
		}

		public override int GetFieldCount()
		{
			return 4;
		}
	}
}
