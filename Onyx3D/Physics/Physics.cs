using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{
	public class Physics
	{

		public static bool RaycastScene(Ray ray, out RaycastHit hit, Scene scene)
		{
			SceneObject obj = scene.Root;
			return RaycastObject(ray, out hit, obj);
		}
		
		// --------------------------------------------------------------------

		public static bool RaycastObject(Ray ray, out RaycastHit hit, SceneObject obj)
		{
			hit = new RaycastHit();
			hit.Distance = float.MaxValue;
			if (obj == null)
				return false;
			
			return obj.IntersectRay(ray, out hit);
		}

		// --------------------------------------------------------------------

		public static bool RaycastEntity(Ray ray, out RaycastHit hit, EntityProxy obj)
		{
			obj.CalculateBounds();
			return RaycastObject(ray, out hit, obj.EntityRef.Root);
		}

		// --------------------------------------------------------------------

		public static bool RaycastTriangle(Ray ray, Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, out RaycastHit hit, bool ignoreBackfaces = true)
		{
			// Möller–Trumbore implementation
			const float EPSILON = 0.0000001f;
			hit = new RaycastHit();
			Vector3 edge1, edge2, h, s, q;
			float a, f, u, v;
			edge1 = vertex1 - vertex0;
			edge2 = vertex2 - vertex0;
			h = Vector3.Cross(ray.Direction, edge2);
			a = Vector3.Dot(edge1, h);
			if (a > -EPSILON && a < EPSILON)
				return false;
			if (ignoreBackfaces && a < 0f)
				return false;

			f = 1 / a;
			s = ray.Origin - vertex0;
			u = f * (Vector3.Dot(s, h));
			if (u < 0.0 || u > 1.0)
				return false;
			q = Vector3.Cross(s, edge1);
			v = f * Vector3.Dot(ray.Direction, q);
			if (v < 0.0 || u + v > 1.0)
				return false;

			// At this stage we can compute t to find out where the intersection point is on the line.
			float t = f * Vector3.Dot(edge2, q);
			if (t > EPSILON) // ray intersection
			{
				hit.Distance = t;
				hit.Point = ray.Origin + ray.Direction * t;
				hit.Object = null;
				return true;
			}
			else // This means that there is a line intersection but not a ray intersection.
				return false;
		}
	}
}

