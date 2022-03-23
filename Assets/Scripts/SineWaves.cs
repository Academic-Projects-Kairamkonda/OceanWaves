using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaves : MonoBehaviour
{
    // Public Properties
    public int Dimension = 10;
    public float UVScale = 2f;
    public Octave[] Octaves;

    // Mesh
    protected MeshFilter MeshFilter;
    protected Mesh Mesh;

    void Start()
    {
        GenerateMesh();
    }

    void Update()
    {
        CalculateMeshData();
    }

    /// <summary>
    /// Generates the quad on the provided data
    /// </summary>
    private void GenerateMesh()
    {
        // Mesh Setup
        Mesh = new Mesh();
        Mesh.name = gameObject.name;

        Mesh.vertices = GenerateVertices();
        Mesh.triangles = GenerateTries();
        Mesh.uv = GenerateUVs();
        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();

        MeshFilter = gameObject.AddComponent<MeshFilter>();
        MeshFilter.mesh = Mesh;
    }

    /// <summary>
    /// Generate the vertices for the triangles
    /// </summary>
    /// <returns>each position of the vertex in array</returns>
    private Vector3[] GenerateVertices()
    {
        var verts = new Vector3[(Dimension + 1) * (Dimension + 1)];

        //equaly distributed verts
        for(int x = 0; x <= Dimension; x++)
            for(int z = 0; z <= Dimension; z++)
                verts[index(x, z)] = new Vector3(x, 0, z);

        return verts;
    }

    /// <summary>
    /// Generate the triangles with the x and z coordinates
    /// </summary>
    /// <returns> vertice of mesh</returns>
    private int[] GenerateTries()
    {
        var tries = new int[Mesh.vertices.Length * 6];

        //two triangles are one tile
        for(int x = 0; x < Dimension; x++)
        {
            for(int z = 0; z < Dimension; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);
                tries[index(x, z) * 6 + 3] = index(x, z);
                tries[index(x, z) * 6 + 4] = index(x, z + 1);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }

        return tries;
    }

    /// <summary>
    /// UV  texture maps to vertices of the mesh
    /// </summary>
    /// <returns></returns>
    private Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[Mesh.vertices.Length];

        //always set one uv over n tiles than flip the uv and set it again
        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                var vec = new Vector2((x / UVScale) % 2, (z / UVScale) % 2);
                uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
            }
        }

        return uvs;
    }

    private int index(int x, int z)
    {
        return x * (Dimension + 1) + z;
    }

    private int index(float x, float z)
    {
        return index((int)x, (int)z);
    }


    /// <summary>
    /// Adds the sine wave data to mesh to animate
    /// </summary>
    private void CalculateMeshData()
    {
         var verts=CalculateSineWave();

        Mesh.vertices = verts;
        Mesh.RecalculateNormals();
    }

    /// <summary>
    /// Calculate sine waves and adds the amplitude to the wave
    /// </summary>
    /// <returns> vertices to mesh</returns>
    private Vector3[] CalculateSineWave()
    {
        var verts = Mesh.vertices;

        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                var y = 0f;
                for (int o = 0; o < Octaves.Length; o++)
                {
                    if (Octaves[o].alternate)
                    {
                        var perl = Mathf.PerlinNoise
                            (
                            (x * Octaves[o].frequency.x) / Dimension,
                            (z * Octaves[o].frequency.y) / Dimension
                            ) * Mathf.PI * 2f;
                        y += Mathf.Sin(perl + Octaves[o].speed.magnitude * Time.time) * Octaves[o].amplitude;
                    }
                    else
                    {
                        var perl = Mathf.PerlinNoise
                            (
                            (x * Octaves[o].frequency.x + Time.time * Octaves[o].speed.x) / Dimension,
                            (z * Octaves[o].frequency.y + Time.time * Octaves[o].speed.y) / Dimension
                            ) - 0.5f;
                        y += perl * Octaves[o].amplitude;
                    }
                }

                verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        return verts;
    }

    /// <summary>
    /// Octaves add more depth to waves
    /// </summary>
    [Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 frequency;
        public float amplitude;
        public bool alternate;
    }
}
