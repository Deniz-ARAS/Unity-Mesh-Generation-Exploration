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

    [SerializeField , Range(0.1f, 10f)] float cubeSize = 1f;
    [SerializeField , Range(1, 10)] int cubeSubDivCount = 1;


    private float prevCubeSize;
    private int prevCubeSubDivCount;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void Start()
    {
        mesh = new Mesh();



        CreateCube();

        prevCubeSize = cubeSize;
        prevCubeSubDivCount = cubeSubDivCount;

        //Tried to create a different planes and offset them then combine but it did not work.
        //CreateSidePlane(true , 0,0);
        //CreateSidePlane(true, 1, 1);
        //CreateSidePlane(false, 0, 2);
        //CreateSidePlane(false, 1, 3);
    }

    private void Update()
    { 
        
        //Updates the mesh with the new one if the settings change in runtime.
        if (cubeSize != prevCubeSize || cubeSubDivCount != prevCubeSubDivCount)
        {
            CreateCube();

            prevCubeSize = cubeSize;
            prevCubeSubDivCount = cubeSubDivCount;
        }
    }


    void CreateCube()
    {
        int verticesPerSide = (cubeSubDivCount + 1) * (cubeSubDivCount + 1);
        vertices = new Vector3[verticesPerSide * 6];

        int vertexIndex = 0;
        float halfCubeSize = cubeSize * 0.5f;

        for (int face = 0; face < 6; face++)
        {
            for (int i = 0; i <= cubeSubDivCount; i++)
            {
                for (int j = 0; j <= cubeSubDivCount; j++)
                {
                    float normalizedI = (float)i / cubeSubDivCount;
                    float normalizedJ = (float)j / cubeSubDivCount;

                    Vector3 vertexPos = Vector3.zero;

                    switch (face)
                    {
                        case 0: // Front
                            vertexPos = new Vector3(normalizedI * cubeSize - halfCubeSize, normalizedJ * cubeSize - halfCubeSize, halfCubeSize);
                            break;
                        case 1: // Back
                            vertexPos = new Vector3(normalizedI * cubeSize - halfCubeSize, normalizedJ * cubeSize - halfCubeSize, -halfCubeSize);
                            break;
                        case 2: // Top
                            vertexPos = new Vector3(normalizedI * cubeSize - halfCubeSize, halfCubeSize, normalizedJ * cubeSize - halfCubeSize);
                            break;
                        case 3: // Bottom
                            vertexPos = new Vector3(normalizedI * cubeSize - halfCubeSize, -halfCubeSize, normalizedJ * cubeSize - halfCubeSize);
                            break;
                        case 4: // Left
                            vertexPos = new Vector3(-halfCubeSize, normalizedI * cubeSize - halfCubeSize, normalizedJ * cubeSize - halfCubeSize);
                            break;
                        case 5: // Right
                            vertexPos = new Vector3(halfCubeSize, normalizedI * cubeSize - halfCubeSize, normalizedJ * cubeSize - halfCubeSize);
                            break;
                    }

                    vertices[vertexIndex++] = vertexPos;
                }
            }
        }
        UpdateCube();
    }



    private void UpdateCube()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void CreateCubeNotWorking()
    {




        /// <summary>
        ///Code Below did not work. However I wanted to keep it in case.
        ///vertices = new Vector3[8];
        ///for (int x = 0, i = 0; x < 2; x++)
        ///{
        ///    for (int y = 0; y < 2; y++)
        ///    {
        ///        for( int z = 0; z < 2; z++)
        ///        {
        ///            vertices[i] = new Vector3(x, y, z);
        ///            i++;
        ///            //Debug.Log(x + "   " + y + "   " + z + "   ");
        ///        }
        ///    }
        ///}
        ///triangles = new int[12];
        ///int layerCount = 0;
        ///for (int z = 0; z < 12; z++)
        ///{
        ///    for ( int x = 0;x < 12; x++)
        ///    {
        ///        triangles[0] = 0;
        ///    }
        ///}
        /// </summary>



    }


    void CreateSidePlane(bool UseXSide, float offset, int faceCount)
    {
        vertices = new Vector3[128];

        int verticesPerSide = (cubeSubDivCount + 1) * (cubeSubDivCount + 1);
        int startIndex = faceCount * verticesPerSide;

        for (int i = 0, z = 0; z <= cubeSubDivCount; z++)
        {
            for (int x = 0; x <= cubeSubDivCount; x++, i++)
            {
                float normalizedX = (float)x / cubeSubDivCount * cubeSize;
                float normalizedZ = (float)z / cubeSubDivCount * cubeSize;

                if (UseXSide)
                {
                    vertices[startIndex + i] = new Vector3(normalizedX, normalizedZ, offset);
                }
                else
                {
                    vertices[startIndex + i] = new Vector3(offset, normalizedZ, normalizedX);
                }
                Debug.Log($"Adding vertex at index: {startIndex + i}, Position: {vertices[startIndex + i].ToString()}");
            }
        }

        //triangles = new int[cubeSubDivCount * cubeSubDivCount * 6];
        //int vert = 0;
        //int tris = 0;

        //for (int z = 0; z < cubeSubDivCount; z++)
        //{
        //    for (int x = 0; x < cubeSubDivCount; x++)
        //    {
        //        triangles[tris + 0] = vert + 0;
        //        triangles[tris + 1] = vert + cubeSubDivCount + 1;
        //        triangles[tris + 2] = vert + 1;
        //        triangles[tris + 3] = vert + 1;
        //        triangles[tris + 4] = vert + cubeSubDivCount + 1;
        //        triangles[tris + 5] = vert + cubeSubDivCount + 2;

        //        vert++;
        //        tris += 6;
        //    }
        //    vert++;
        //}
    }
    void CreatePlane()
    {
        vertices = new Vector3[(cubeSubDivCount + 1) * (cubeSubDivCount + 1)];

        for (int i = 0, z = 0; z <= cubeSubDivCount; z++)
        {
            for (int x = 0; x <= cubeSubDivCount; x++)
            {
                float normalizedX = (float)x / cubeSubDivCount * cubeSize;
                float normalizedZ = (float)z / cubeSubDivCount * cubeSize;

                vertices[i] = new Vector3(normalizedX, 0, normalizedZ);
                i++;
            }
        }

        triangles = new int[cubeSubDivCount * cubeSubDivCount * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < cubeSubDivCount; z++)
        {
            for (int x = 0; x < cubeSubDivCount; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + cubeSubDivCount + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + cubeSubDivCount + 1;
                triangles[tris + 5] = vert + cubeSubDivCount + 2;

                vert++;
                tris += 6;
            }
            vert++;
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
