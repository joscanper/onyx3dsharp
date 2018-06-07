using OpenTK;
using Onyx3D;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Onyx3D
{
	public class LightInspector : Inspector<Light>
	{
		
		public LightInspector(Light light) : base(light)
		{
		}


		[Category("Light")]
		public LightType Type
		{
			get { return mObject.Type; }
			set { mObject.Type = value; }
		}

		[Category("Light")]
		public Color Color
		{
			get { return mObject.Color.ToColor(); }
			set { mObject.Color = value.ToVector(); }
		}

		[Category("Light")]
		public float Intensity
		{
			get { return mObject.Intensity; }
			set { mObject.Intensity = value; }
		}


		public override void Apply()
		{
		}

		public override int GetFieldCount()
		{
			return 5;
		}
	}
}
