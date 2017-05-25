using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class PlanetGenerator : MonoBehaviour {
    // Use this for initialization
    int i, j;

    void addTriangle(int[] t, int[] t2, int p0, int p1, int p2)
    {
        t[i++] = p0;
        t[i++] = p1;
        t[i++] = p2;
    }

    void Awake () {
        i = 0;
        j = 0;
        var g = new GameObject("mesh");
        MeshFilter filter = g.AddComponent<MeshFilter>();
        g.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        mesh.subMeshCount = 2;
        filter.sharedMesh = mesh;
        
        float radius = 1f;
        // Longitude |||
        int nbLong = 120;
        // Latitude ---
        int nbLat = 80;

        #region Vertices
        Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];
        float _pi = Mathf.PI;
        float _2pi = _pi * 2f;

        vertices[0] = Vector3.up * radius;
        for (int lat = 0; lat < nbLat; lat++)
        {
            float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= nbLong; lon++)
            {
                float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
            }
        }
        vertices[vertices.Length - 5] = Vector3.up * -radius;
        #endregion
        
        #region Normals		
        Vector3[] normales = new Vector3[vertices.Length];
        for (int n = 0; n < vertices.Length; n++)
            normales[n] = vertices[n].normalized;
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        uvs[0] = Vector2.up;
        uvs[uvs.Length - 1] = Vector2.zero;
        for (int lat = 0; lat < nbLat; lat++)
            for (int lon = 0; lon <= nbLong; lon++)
                uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
        #endregion

        #region Triangles
        int nbFaces = vertices.Length;
        int nbTriangles = nbFaces * 2;
        int nbIndexes = nbTriangles * 3;
        int[] triangles = new int[nbIndexes];
        int[] triangles2 = new int[6];

        //Top Cap
        for (int lon = 0; lon < nbLong; lon++)
        {
            addTriangle(triangles, triangles2, lon + 2, lon + 1, 0);
        }

        //Middle
        for (int lat = 0; lat < nbLat - 1; lat++)
        {
            for (int lon = 0; lon < nbLong; lon++)
            {
                int current = lon + lat * (nbLong + 1) + 1;
                int next = current + nbLong + 1;

                addTriangle(triangles, triangles2, current, current + 1, next + 1);
                addTriangle(triangles, triangles2, current, next + 1, next);
            }
        }

        //Bottom Cap
        for (int lon = 0; lon < nbLong; lon++)
        {
            addTriangle(triangles, triangles2, vertices.Length - 1, vertices.Length - (lon + 2) - 1, vertices.Length - (lon + 1) - 1);
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        //mesh.SetTriangles(triangles2, 1);
        
        mesh.RecalculateBounds();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
