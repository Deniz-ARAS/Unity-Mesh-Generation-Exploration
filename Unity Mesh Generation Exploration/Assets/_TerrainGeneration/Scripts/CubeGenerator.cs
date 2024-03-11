using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/// <summary>
/// Currently This does not work!
/// </summary>
public class CubeGenerator : MonoBehaviour
{


    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] float cubeSize = 1f;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        CreateCube();
        UpdateCube();
    }

    private void UpdateCube()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void CreateCube()
    {
        vertices = new Vector3[8];
        for (int x = 0, i = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                for( int z = 0; z < 2; z++)
                {

                    vertices[i] = new Vector3(x, y, z);
                    i++;
                    //Debug.Log(x + "   " + y + "   " + z + "   ");
                }
            }
        }
        triangles = new int[12];

        //side faces

        int layerCount = 0;
        for (int z = 0; z < 12; z++)
        {
            for ( int x = 0;x < 12; x++)
            {
                triangles[0] = 0;

                
            }
        }


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
    
}
