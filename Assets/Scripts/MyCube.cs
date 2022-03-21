using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class MyCube : MonoBehaviour
{
    Mesh mesh;
    Material material;
    Vector3[] vertices;
    int[] triangles;

    private void OnValidate()
    {
        Logic();
    }

    private void Logic()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = gameObject.name;
        }

        material = new Material(Shader.Find("Specular"));
        material.color = Color.blue;

        this.GetComponent<MeshFilter>().mesh = this.mesh;
        this.GetComponent<MeshRenderer>().material = this.material;

        ConstructMesh();
    }

    private void ConstructMesh()
    {
        SquareMeshPoistion();
        UpdateMeshData();
    }

    private void UpdateMeshData()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void SquareMeshPoistion()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0), //0
            new Vector3(0,1,0), //1

            new Vector3(1,0,0), //2
            new Vector3(1,1,0), //3

            new Vector3(1,0,1), //4
            new Vector3(1,1,1), //5

            new Vector3(0,0,1), //6
            new Vector3(0,1,1), //7
        };

        triangles = new int[]
        {
            0,1,2,
            1,3,2, //1

            2,3,4,
            3,5,4, //2

            4,5,6,
            5,7,6, //3

            6,7,0,
            7,1,0, //4

            0,2,4,
            4,6,0, //base

            3,1,5,
            1,7,5, //top
        };
    }
}