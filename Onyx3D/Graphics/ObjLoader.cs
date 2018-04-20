using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using OpenTK;

namespace Onyx3D
{
	public static class ObjLoader
	{

		public class FaceIndices
		{
			public string VertexId = "";
			public int VertexIndex = -1;
			public int TextureCoordIndex = -1;
			public int NormalIndex = -1;
		}

		public static Mesh Load(string path)
		{
			
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
			}
			
			List<Vector4> vertices = new List<Vector4>();
			List<Vector3> textureVertices = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			List<FaceIndices> indices = new List<FaceIndices>();
			

			using (StreamReader streamReader = new StreamReader(path))
			{
				while (!streamReader.EndOfStream)
				{
					List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
					words.RemoveAll(s => s == string.Empty);

					if (words.Count == 0)
						continue;

					string type = words[0];
					words.RemoveAt(0);

					switch (type)
					{
						// vertex
						case "v":
							vertices.Add(new Vector4(float.Parse(words[0]), float.Parse(words[1]),
													float.Parse(words[2]), words.Count < 4 ? 1 : float.Parse(words[3])));
							break;

						case "vt":
							textureVertices.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]),
															words.Count < 3 ? 0 : float.Parse(words[2])));
							break;

						case "vn":
							normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
							break;

						// face
						case "f":
							foreach (string w in words)
							{
								if (w.Length == 0)
									continue;

								string[] comps = w.Split('/');

								FaceIndices fi = new FaceIndices();

								fi.VertexId = w;

								// subtract 1: indices start from 1, not 0
								fi.VertexIndex = int.Parse(comps[0]) - 1;

								if (comps.Length > 1 && comps[1].Length != 0)
									fi.TextureCoordIndex = int.Parse(comps[1]) - 1;

								if (comps.Length > 2)
									fi.NormalIndex = int.Parse(comps[2]) - 1;

								indices.Add(fi);
							}
							break;

						default:
							break;
					}
				}
			}

			List<int> meshIndices = new List<int>();
			Mesh m = new Mesh();
			Dictionary<String, int> vertmap = new Dictionary<string, int>();
			int index = 0;
			foreach(FaceIndices fi in indices){
				Vertex v;
				
				if (vertmap.ContainsKey(fi.VertexId))
				{
					meshIndices.Add(vertmap[fi.VertexId]);
				}
				else
				{
					v = new Vertex(vertices[fi.VertexIndex].Xyz);
					if (fi.NormalIndex >= 0) v.Normal = normals[fi.NormalIndex];
					if (fi.TextureCoordIndex >= 0) v.TexCoord = textureVertices[fi.TextureCoordIndex].Xy;
					m.Vertices.Add(v);
					vertmap.Add(fi.VertexId, index);
					meshIndices.Add(index);
					index++;
				}
			}

			m.Indices = meshIndices.ToArray();
			m.GenerateVAO();

			return m;
		}
	}
}
