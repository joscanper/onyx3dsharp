using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onyx3D
{
    public class Physics
    {

        public static bool Raycast(Ray ray, out RaycastHit hit, Scene scene)
        {
            SceneObject obj = scene.Root;
            hit = new RaycastHit();

            if (scene.Root == null)
                return false;

            Queue<SceneObject> objects = new Queue<SceneObject>();
            objects.Enqueue(scene.Root);
            SceneObject s;
            do
            {
                s = objects.Dequeue();
                List<MeshRenderer> objRenderers = s.GetComponents<MeshRenderer>();
                if (objRenderers.Count > 0)
                {
                    for (int i = 0; i < objRenderers.Count; ++i)
                    { 
                        if (objRenderers[i].Bounds.IntersectsRay(ray))
                        {
                            hit.Object = s;
                            return true;
                        }
                    }
                }

                for (int i = 0; i < s.ChildCount; i++)
                {
                    objects.Enqueue(s.GetChild(i));
                }
            } while (objects.Count > 0);

            return false;
        }
        
    }
}
