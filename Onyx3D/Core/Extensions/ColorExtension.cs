using System.Drawing;
using OpenTK;

namespace Onyx3D
{
	public static class ColorExtension
	{
		public static Vector4 ToVector(this Color c)
		{
			return new Vector4(c.R / 255.0f, c.G / 255.0f, c.B / 255.0f, c.A / 255.0f);
		}
	}
}
