using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Onyx3D
{
    public class CubeMesh : Mesh
    {
        public CubeMesh()
        {
            Vertex v1 = new Vertex(new Vector3(-0.5f, 0.5f, -0.5f));
            Vertex v2 = new Vertex(new Vector3(0.5f, 0.5f, -0.5f));
            Vertex v3 = new Vertex(new Vector3(-0.5f, -0.5f, -0.5f));
            Vertex v4 = new Vertex(new Vector3(0.5f, -0.5f, -0.5f));

            Vertex v5 = new Vertex(new Vector3(-0.5f, 0.5f, 0.5f));
            Vertex v6 = new Vertex(new Vector3(0.5f, 0.5f, 0.5f));
            Vertex v7 = new Vertex(new Vector3(-0.5f, -0.5f, 0.5f));
            Vertex v8 = new Vertex(new Vector3(0.5f, -0.5f, 0.5f));

            v1.Color = new Vector3(1, 0, 0);
            v2.Color = new Vector3(0, 1, 0);
            v3.Color = new Vector3(0, 0, 1);
            v4.Color = new Vector3(0, 1, 1);

            Vertices.Add(v1);
            Vertices.Add(v2);
            Vertices.Add(v3);

            Vertices.Add(v2);
            Vertices.Add(v4);
            Vertices.Add(v3);

            GenerateVAO();
        }
    }
}
