
using OpenTK;

namespace Onyx3D
{

    public static class AssimpExtension
    {
        public static Matrix4 ToOnyx3D(this Assimp.Matrix4x4 m)
        {
            return new Matrix4(
                m.A1, m.A2, m.A3, m.A4,
                m.B1, m.B2, m.B3, m.B4,
                m.C1, m.C2, m.C3, m.C4,
                m.D1, m.D2, m.D3, m.D4);
        }

        public static Vector3 ToOnyx3D(this Assimp.Vector3D v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static Onyx3D.DefaultMaterial ToOnyx3D(this Assimp.Material material)
        {
            Onyx3D.DefaultMaterial newMaterial = new Onyx3D.DefaultMaterial();

            // TODO - Load the material;

            return newMaterial;
        }

        public static Onyx3D.Mesh ToOnyx3D(this Assimp.Mesh mesh)
        {
            Onyx3D.Mesh newMesh = new Onyx3D.Mesh();

            newMesh.Indices = mesh.GetIndices();
            for (int vi = 0; vi < mesh.VertexCount; ++vi)
            {
                Vertex newVertex = new Vertex();
                newVertex.Position = mesh.Vertices[vi].ToOnyx3D();

                if (mesh.HasTextureCoords(0))
                {
                    Assimp.Vector3D texCoord = mesh.TextureCoordinateChannels[0][vi];
                    newVertex.TexCoord = texCoord.ToOnyx3D().Xy;
                }
                if (mesh.HasNormals)
                {
                    newVertex.Normal = mesh.Normals[vi].ToOnyx3D();
                    if (mesh.HasTangentBasis)
                    {
						newVertex.Bitangent = mesh.BiTangents[vi].ToOnyx3D();
						newVertex.Tangent = mesh.Tangents[vi].ToOnyx3D();	
                    }
                }
                

                newMesh.Vertices.Add(newVertex);
            }

            newMesh.GenerateVAO();
            return newMesh;
        }
    }
}
