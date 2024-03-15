using UnityEngine;


/// <summary>
/// 
/// To-Do:
/// 
/// - Add a real time editor for the generation.
/// - Try to generate the mesh without flipping its normals.
/// - Try to generate new points inside of the circle to make a quad grid. (Check out the Blender's "Grid Fill" generation.)
/// 
/// 
/// </summary>

[RequireComponent(typeof(MeshFilter))]
public class CircleGenerator : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    [SerializeField] int CircleResolution = 36;
    [SerializeField] float circleDiameter = 1f;

    [SerializeField] float NoiseStrenght = 0.25f;
    [SerializeField] float NoiseMagnitude = 2f;

    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        CreateCircle();
        UpdateCircle();
        FlipNormals(mesh);
    }

    private void CreateCircle()
    {
        vertices = new Vector3[CircleResolution + 1];
        vertices[0] = Vector3.zero;
        for (int i = 1; i <= CircleResolution; i++)
        {
            float angle = (i * 2 * Mathf.PI) / CircleResolution;
            float x = Mathf.Cos(angle) * circleDiameter / 2f;
            float z = Mathf.Sin(angle) * circleDiameter / 2f;

            //for fun
            float y = Mathf.PerlinNoise(x * NoiseStrenght, z * NoiseStrenght) * NoiseMagnitude;


            vertices[i] = new Vector3(x, y, z);
        }

        triangles = new int[CircleResolution * 3];
        for (int i = 0; i < CircleResolution; i++)
        {
            int index = i * 3;
            triangles[index] = 0;
            triangles[index + 1] = i + 1;
            triangles[index + 2] = i < CircleResolution - 1 ? i + 2 : 1;

        }
    }

    private void UpdateCircle()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
    }

    void FlipNormals(Mesh mesh)
    {
        // Reverse the triangles
        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Swap the first and last vertex in each triangle
            int temp = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = temp;
        }
        mesh.triangles = triangles;

        // Negate the normals
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        // ForFlipping the UV's
        // Vector2[] uvs = mesh.uv;
        // for (int i = 0; i < uvs.Length; i++)
        // {
        //     uvs[i] = new Vector2(uvs[i].x, 1f - uvs[i].y);
        // }
        // mesh.uv = uvs;

        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
    }
}
