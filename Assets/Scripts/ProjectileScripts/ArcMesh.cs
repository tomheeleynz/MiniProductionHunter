using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ArcMesh : MonoBehaviour
{
    Mesh mesh;
    public CharacterShooting parent;
    public float meshWidth;
    public GameObject launchAngle;
    public Camera cameraForAngle;

    public float velocity;
    public float angle;
    public int resolution;

    float g;
    float radianAngle;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void Start()
    {
        MakeArcMesh(CalculateArcArray());
        transform.position = parent.transform.position + parent.transform.forward;
    }

    private void OnValidate()
    {
        if (mesh != null && Application.isPlaying)
        {
            MakeArcMesh(CalculateArcArray());
        }
    }

    void MakeArcMesh(Vector3[] arcVertices)
    {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 6 * 2];

        for (int i = 0; i <= resolution; i++)
        {
            // Vertices
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVertices[i].y, arcVertices[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVertices[i].y, arcVertices[i].x);

            // Triangles
            if (i != resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
        }
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return (arcArray);
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return (new Vector3(x, y));
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.Aiming)
        {
            cameraForAngle = parent.fpCam;
        }
        else
        {
            cameraForAngle = parent.Cam;
        }

        OnValidate();

        angle = Mathf.Abs(cameraForAngle.transform.rotation.eulerAngles.x);
        if (angle >= 280)
        {
            angle = angle - 360;
        }

        velocity = parent.forceApplied;
        
        if (angle <= 0)
        {
            launchAngle.transform.localRotation = Quaternion.Euler(angle - 10.0f, 0, 180);
        }
        else
        {
            launchAngle.transform.localRotation = Quaternion.Euler(angle - 10.0f, 0, 0);
        }

    }
}
