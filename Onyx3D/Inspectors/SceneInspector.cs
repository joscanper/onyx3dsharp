using OpenTK;
using Onyx3D;
using System;
using System.ComponentModel;
using System.Drawing;

namespace Onyx3D
{
	public class SceneInspector : Inspector<Scene>
	{
		Color ambientColor;

		//Vector3Inspector scale;
		//Vector3Inspector rotation;

		public SceneInspector(Scene scene) : base(scene)
		{
			Vector3 ambient = scene.Lighting.Ambient;
			ambientColor = Color.FromArgb((int)(ambient.X * 255), (int)(ambient.Y * 255), (int)(ambient.Z * 255));
		}


		[Category("Lighting")]
		public Color AmbientLight
		{
			get { return ambientColor; }
			set { ambientColor = value; }
		}


		public override void Apply()
		{
			mObject.Lighting.Ambient = new Vector3(ambientColor.R / 255.0f, ambientColor.G / 255.0f, ambientColor.B / 255.0f);
		}

		public override int GetFieldCount()
		{
			return 2;
		}
	}
}
