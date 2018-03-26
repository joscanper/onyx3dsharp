using OpenTK;
using Onyx3D;
using System;
using System.ComponentModel;

namespace Onyx3DEditor
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
			set { mObject.LocalPosition = position.Object; }
		}

		[Category("Transform")]
		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Scale
		{
			get { return scale; }
			set { mObject.LocalScale = scale.Object; }
		}

		[Category("Transform")]
		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Rotation
		{
			get { return rotation; }
			set { mObject.LocalRotation = mObject.LocalRotation.FromDegEulerAngles(rotation.Object); }
		}

		
		public override void Apply()
		{
			mObject.LocalPosition = Position.Object;
			mObject.LocalScale = Scale.Object;
			mObject.LocalRotation = mObject.LocalRotation.FromDegEulerAngles(Rotation.Object);
		}
	}
}
