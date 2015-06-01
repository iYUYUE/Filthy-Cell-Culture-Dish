// C#
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshHelper
{
    static List<Vector3> vertices;
//    static List<Vector3> normals;
    static List<Vector2> uvs;
    // [... all other vertex data arrays you need]

    static List<int> indices;
    static Dictionary<uint,int> newVectices;

    static int GetNewVertex(int i1, int i2)
    {
        // We have to test both directions since the edge
        // could be reversed in another triangle
        uint t1 = ((uint)i1 << 16) | (uint)i2;
        uint t2 = ((uint)i2 << 16) | (uint)i1;
        if (newVectices.ContainsKey(t2))
            return newVectices[t2];
        if (newVectices.ContainsKey(t1))
            return newVectices[t1];
        // generate vertex:
        int newIndex = vertices.Count;
        newVectices.Add(t1,newIndex);

        // calculate new vertex
//		float tempY = Random.Range(vertices[i1].y, vertices[i2].y);
		float tempY;
		float p1Y = vertices[i1].y;
		float p2Y = vertices[i2].y;
		float chord = Vector3.Distance(vertices[i1], vertices[i2]) / 10.0f;
		if (p1Y <= p2Y) {
			tempY = Random.Range((p1Y+p1Y+p2Y)/3.5f, (p1Y+p2Y+p2Y)/2.5f);
		} else {
			tempY = Random.Range((p1Y+p2Y+p2Y)/3.5f, (p1Y+p1Y+p2Y)/2.5f);
		}
		float deviation = Random.Range((0.0f-chord), chord);
		Vector3 tempVert = (vertices[i1] + vertices[i2]) * 0.5f;
		tempVert.y = tempY+deviation;
		
        vertices.Add(tempVert);
//		normals.Add((normals[i1] + normals[i2]).normalized);
        uvs.Add((uvs[i1] + uvs[i2]) * 0.5f);
        // [... all other vertex data arrays]

        return newIndex;
    }


    public static void Subdivide(Mesh mesh)
    {
        newVectices = new Dictionary<uint,int>();

        vertices = new List<Vector3>(mesh.vertices);
//		normals = new List<Vector3>(mesh.normals);
        uvs = new List<Vector2>(mesh.uv);
        // [... all other vertex data arrays]
        indices = new List<int>();

        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int i1 = triangles[i + 0];
            int i2 = triangles[i + 1];
            int i3 = triangles[i + 2];

            int a = GetNewVertex(i1, i2);
            int b = GetNewVertex(i2, i3);
            int c = GetNewVertex(i3, i1);
            indices.Add(i1);   indices.Add(a);   indices.Add(c);
            indices.Add(i2);   indices.Add(b);   indices.Add(a);
            indices.Add(i3);   indices.Add(c);   indices.Add(b);
            indices.Add(a );   indices.Add(b);   indices.Add(c); // center triangle
        }
        mesh.vertices = vertices.ToArray();
//		mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        // [... all other vertex data arrays]
        mesh.triangles = indices.ToArray();
        // since this is a static function and it uses static variables
        // we should erase the arrays to free them:
        newVectices = null;
        vertices = null;
//		normals = null;
        uvs = null;
        // [... all other vertex data arrays]

        indices = null;
    }
}