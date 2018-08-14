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

			hit = new RaycastHit();
			hit.Distance = float.MaxValue;
			if (scene.Root == null)
				return false;

			List<Renderer> candidates = new List<Renderer>();
			Queue<SceneObject> objects = new Queue<SceneObject>();
			objects.Enqueue(scene.Root);
			SceneObject s;
			do
			{
				s = objects.Dequeue();
				List<Renderer> objRenderers = s.GetComponents<Renderer>();
				if (objRenderers.Count > 0)
				{
					for (int i = 0; i < objRenderers.Count; ++i)
					{
						if (objRenderers[i].Bounds.IntersectsRay(ray))
						{
							candidates.Add(objRenderers[i]);
						}
					}
				}

				for (int i = 0; i < s.ChildCount; i++)
				{
					objects.Enqueue(s.GetChild(i));
				}
			} while (objects.Count > 0);

			hit.Distance = float.MaxValue;
			RaycastHit objHit = new RaycastHit();
			foreach (Renderer renderer in candidates)
			{
				if (renderer.IntersectsRay(ray, out objHit) && objHit.Distance < hit.Distance)
				{
					hit = objHit;
				}
			}

			return hit.Object != null;
		}


		public static bool RaycastTriangle(Ray ray, Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, out RaycastHit hit)
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

