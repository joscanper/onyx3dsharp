using System;
using System.Collections;

using OpenTK;
namespace Onyx3D
{
	public class CylinderMesh : Mesh
	{
		public CylinderMesh()
		{
			float radius = 0.5f;
			int segments = 20;
			int rings = 2;
			float angleInterval = (float)((360.0f / segments) * Math.PI / 180.0f);

			Vertex[] currentRing = new Vertex[segments];
			Vertex[] previousRing = null;

			for (int ring = 0; ring < rings; ring++)
			{
				for (int i = 0; i < segments; i++)
				{
					float angle = angleInterval * i;
					float x = (float)Math.Cos(angle) * radius;
					float z = (float)Math.Sin(angle) * radius;
					Vector3 pos = new Vector3(x, 0.5f - ring, z);
					Vertex v = new Vertex(pos, new Vector3(1, 0, 0));
					currentRing[i] = v;
					Console.WriteLine(pos);
				}

				if (previousRing != null)
				{
					for (int i = 0; i < segments; i++)
					{

						if (i < segments - 1)
							AddFace(previousRing[i], previousRing[i + 1], currentRing[i], currentRing[i + 1], Vector3.Zero);
						else
							AddFace(previousRing[i], previousRing[0], currentRing[i], currentRing[0], Vector3.Zero);
					}
				}
				else
				{
					// Top cap
					Vertex top = new Vertex(new Vector3(0, 0.5f, 0), Vector3.Zero);
					for (int i = 0; i < segments; i++)
					{
						Vertices.Add(top);
						if (i == segments-1)
							Vertices.Add(currentRing[0]);
						else
							Vertices.Add(currentRing[i+1]);
						Vertices.Add(currentRing[i]);
					}
				}

				

				previousRing = (Vertex[])currentRing.Clone();
			}

			// Bottom cap
			Vertex bottom = new Vertex(new Vector3(0, -0.5f, 0), Vector3.Zero);
			for (int i = 0; i < segments; i++)
			{
				Vertices.Add(bottom);
				
				Vertices.Add(currentRing[i]);
				if (i == segments - 1)
					Vertices.Add(currentRing[0]);
				else
					Vertices.Add(currentRing[i + 1]);
			}

			GenerateVAO();
		}
	}
}