using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;



/// <summary>
/// This Script written with the Brackeys Mesh Generation Tutorial. 
/// </summary>

[RequireComponent(typeof(MeshFilter))]
public class PlaneCreator : MonoBehaviour
{

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] float NoiseStrenght = 0.3f;

    [SerializeField] int xSize = 10;
    [SerializeField] int zSize = 10;

    private Mesh mesh;

    Vector3[] vertices;
    int[] triangles;



    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        //HardCodedCreatePlane();
        //HardCodedUpdatePlane();

        CreatePlane();
        UpdatePlane();
    }

    void CreatePlane()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];


        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * NoiseStrenght, z * NoiseStrenght) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdatePlane()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(vertices[i], 0.2f);
                
            }
        }
        
    }



    private void HardCodedCreatePlane()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(1,0,1)
        };

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };
    }
    private void HardCodedUpdatePlane()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

}
