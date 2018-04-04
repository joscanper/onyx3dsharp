using OpenTK;
using Onyx3D;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Onyx3D
{
	public class LightInspector : Inspector<Light>
	{
		Color lightColor;

		//Vector3Inspector scale;
		//Vector3Inspector rotation;

		public LightInspector(Light light) : base(light)
		{
			lightColor = Color.FromArgb((int)(light.Color.X * 255), (int)(light.Color.Y * 255), (int)(light.Color.Z * 255));
		}


		[Category("Light")]
		public Color Color
		{
			get { return lightColor; }
			set { lightColor = value; }
		}

		[Category("Light")]
		public float Intensity
		{
			get { return mObject.Intensity; }
			set { mObject.Intensity = value; }
		}


		public override void Apply()
		{
			mObject.Color = new Vector4(lightColor.R / 255.0f, lightColor.G / 255.0f, lightColor.B / 255.0f, 1.0f);
		}

		public override int GetFieldCount()
		{
			return 2;
		}
	}
}
