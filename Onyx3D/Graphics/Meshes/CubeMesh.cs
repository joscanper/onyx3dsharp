using OpenTK;

namespace Onyx3D
{
    public class CubeMesh : Mesh
    {
        public CubeMesh()
        {
			
            Vertex v1 = new Vertex(new Vector3(-0.5f, 0.5f, -0.5f), Vector3.One, Vector3.Zero, new Vector2(0, 0));
            Vertex v2 = new Vertex(new Vector3(0.5f, 0.5f, -0.5f), Vector3.One, Vector3.Zero, new Vector2(1, 0));
            Vertex v3 = new Vertex(new Vector3(-0.5f, -0.5f, -0.5f), Vector3.One, Vector3.Zero, new Vector2(0, 1));
            Vertex v4 = new Vertex(new Vector3(0.5f, -0.5f, -0.5f), Vector3.One, Vector3.Zero, new Vector2(1, 1));

            Vertex v5 = new Vertex(new Vector3(-0.5f, 0.5f, 0.5f), Vector3.One, Vector3.Zero, new Vector2(0, 0));
            Vertex v6 = new Vertex(new Vector3(0.5f, 0.5f, 0.5f), Vector3.One, Vector3.Zero, new Vector2(1, 0));
            Vertex v7 = new Vertex(new Vector3(-0.5f, -0.5f, 0.5f), Vector3.One, Vector3.Zero, new Vector2(0, 1));
            Vertex v8 = new Vertex(new Vector3(0.5f, -0.5f, 0.5f), Vector3.One, Vector3.Zero, new Vector2(1, 1));

            v1.Color = new Vector3(1, 0, 0);
            v2.Color = new Vector3(0, 1, 0);
            v3.Color = new Vector3(0, 0, 1);
            v4.Color = new Vector3(0, 1, 1);
			v5.Color = new Vector3(1, 0, 1);
			v6.Color = new Vector3(1, 1, 0);
			v7.Color = new Vector3(1, 1, 1);
			v8.Color = new Vector3(0, 0, 0);

			AddFace(v1, v2, v3, v4);
			AddFace(v2, v6, v4, v8);
			AddFace(v6, v5, v8, v7);
			AddFace(v5, v1, v7, v3);
			AddFace(v5, v6, v1, v2);
			AddFace(v3, v4, v7, v8);
			
			GenerateVAO();
        }


	}


}
