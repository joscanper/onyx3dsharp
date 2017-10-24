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
			v5.Color = new Vector3(1, 0, 1);
			v6.Color = new Vector3(1, 1, 0);
			v7.Color = new Vector3(1, 1, 1);
			v8.Color = new Vector3(0, 0, 0);

			AddFace(v1, v2, v3, v4, new Vector3(0, 0, 1));
			AddFace(v2, v6, v4, v8, new Vector3(1, 0, 0));
			AddFace(v6, v5, v8, v7, new Vector3(0, 0, -1));
			AddFace(v5, v1, v7, v3, new Vector3(-1, 0, 0));
			AddFace(v5, v6, v1, v2, new Vector3(0, -1, 0));
			AddFace(v3, v4, v7, v8, new Vector3(0, 1, 0));
			
			GenerateVAO();
        }


		private void AddFace(Vertex v1, Vertex v2, Vertex v3, Vertex v4, Vector3 n)
		{

			v1.Normal = n;
			v2.Normal = n;
			v3.Normal = n;
			v4.Normal = n;

			Vertices.Add(v1);
			Vertices.Add(v2);
			Vertices.Add(v3);
			Vertices.Add(v2);
			Vertices.Add(v4);
			Vertices.Add(v3);

		}
	}


}
