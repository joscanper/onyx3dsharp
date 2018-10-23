using System;
using OpenTK;

public class XmlUtils
{
    public static string Vector2ToString(Vector2 v)
    {
        return v.X + ", " + v.Y;
    }

    public static string Vector3ToString(Vector3 v)
    {
        return v.X + ", " + v.Y + ", " + v.Z;
    }

    public static string Vector4ToString(Vector4 v)
    {
        return v.X + ", " + v.Y + ", " + v.Z + ", " + v.W;
    }


    public static Vector2 StringToVector2(string vs)
    {
        Vector2 v = new Vector2();
        string[] values = vs.Split(',');
        v.X = (float)Convert.ToDouble(values[0]);
        v.Y = (float)Convert.ToDouble(values[1]);
        return v;
    }


    public static Vector3 StringToVector3(string vs)
    {
		if (vs == null)
			return Vector3.Zero;

        Vector3 v = new Vector3();
        string[] values = vs.Split(',');
        v.X = (float)Convert.ToDouble(values[0]);
        v.Y = (float)Convert.ToDouble(values[1]);
        v.Z = (float)Convert.ToDouble(values[2]);
        return v;
    }

    public static Vector4 StringToVector4(string vs)
    {
        Vector4 v = new Vector4();
        string[] values = vs.Split(',');
        v.X = (float)Convert.ToDouble(values[0]);
        v.Y = (float)Convert.ToDouble(values[1]);
        v.Z = (float)Convert.ToDouble(values[2]);
        v.W = (float)Convert.ToDouble(values[3]);
        return v;
    }
}
