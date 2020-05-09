using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float currentX = 0.0f;
    public float currentY = 0.0f;

    // Camera Tranforms
    public Transform cameraTransform;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        currentX = Mathf.Clamp(currentX, -50.0f, 80.0f);
        Debug.Log("Camera X Rotation: " + currentX);
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
        cameraTransform.rotation = rotation;
    }
}
