using OpenTK;
using Onyx3D;
using System.ComponentModel;

namespace Onyx3DEditor
{
	public class TransformInspector  : Inspector<Transform>
	{
		Vector3Inspector position;
		Vector3Inspector scale;

		public TransformInspector(Transform t) : base(t)
		{
			position = new Vector3Inspector(t.LocalPosition);
			scale = new Vector3Inspector(t.LocalScale);
		}

		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Position {
			get { return position; }
			set { mObject.LocalPosition = position.Object; }
		}

		[TypeConverter(typeof(Vector3Converter))]
		public Vector3Inspector Scale
		{
			get { return scale; }
			set { mObject.LocalScale = scale.Object; }
		}


		public override void Apply()
		{
			mObject.LocalPosition = Position.Object;
			mObject.LocalScale = Scale.Object;
		}
	}
}
