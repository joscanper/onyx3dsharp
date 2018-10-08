using OpenTK;
using System;
using System.Drawing;

public static class Vector3Extension
{
    public static float SqrDistance(this Vector3 v1, Vector3 v2)
    {
        Vector3 dir = v1 - v2;
        return dir.LengthSquared;
    }

    public static float CalculateAngleTo(this Vector3 v1, Vector3 v2, Vector3 normal)
    {
        v1.Normalize();
        v2.Normalize();
        float angle = (float)Math.Acos(Vector3.Dot(v1, v2));
        Vector3 cross = Vector3.Cross(v1, v2);
        if (Vector3.Dot(normal, cross) < 0)
        { // Or > 0
            angle = -angle;
        }
        return angle;
    }

    public static Color ToColor(this Vector3 v)
    {
        return Color.FromArgb((int)(v.X * 255.0f), (int)(v.Y * 255.0f), (int)(v.Z * 255.0f));
    }

    public static float Max(this Vector3 v)
    {
        return Math.Max(v.X, Math.Max(v.Y, v.Z));
    }

    public static float Min(this Vector3 v)
    {
        return Math.Min(v.X, Math.Max(v.Y, v.Z));
    }

    public static Vector3 Abs(this Vector3 v)
    {
        return new Vector3(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));
    }

    public static bool IsZero(this Vector3 v)
    {
        return v == Vector3.Zero;
    }
}

// --------------------------------------------------------------------
// --------------------------------------------------------------------

public static class Vector4Extension
{
    public static float SqrDistance(this Vector4 v1, Vector4 v2)
    {
        Vector4 dir = v1 - v2;
        return dir.LengthSquared;
    }

    public static Color ToColor(this Vector4 v)
    {
        return Color.FromArgb((int)(v.W * 255.0f), (int)(v.X * 255.0f), (int)(v.Y * 255.0f), (int)(v.Z * 255.0f));
    }

    public static Vector4 Abs(this Vector4 v)
    {
        return new Vector4(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z), Math.Abs(v.W));
    }

    public static bool IsZero(this Vector4 v)
    {
        return v == Vector4.Zero;
    }
}