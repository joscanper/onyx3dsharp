
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Onyx3D
{
    public class QuadRenderer : MeshRenderer
    {
        public void GenerateQuad(float w, float h)
        {
            if (Mesh == null)
                Mesh = new Mesh();
            else
                Mesh.Clear();

            Vertex v1 = new Vertex(new Vector3(-w * 0.5f, 0, -h * 0.5f),Vector3.One, Vector3.UnitY, new Vector2(0,0));
            Vertex v2 = new Vertex(new Vector3(w * 0.5f, 0, -h * 0.5f), Vector3.One, Vector3.UnitY, new Vector2(1, 0));
            Vertex v3 = new Vertex(new Vector3(w * 0.5f, 0, h * 0.5f), Vector3.One, Vector3.UnitY, new Vector2(1, 1));
            Vertex v4 = new Vertex(new Vector3(-w * 0.5f, 0, h * 0.5f), Vector3.One, Vector3.UnitY, new Vector2(0, 1));

            Mesh.Vertices.Add(v1);
            Mesh.Vertices.Add(v2);
            Mesh.Vertices.Add(v3);
            Mesh.Vertices.Add(v4);

            Mesh.Indices = new uint[]
            {
                0,
                3,
                2,
                0,
                2,
                1
            };

            Mesh.GenerateVAO();
        }
        
    }
}

