using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Transforms for Camera Position
    public Transform lookAt;
    public Transform cameraTransform;
    public Transform zoomInTransform;

    private Camera cam;

    public float distance = 7.0f;
    
    // Current X and Y of Camera
    public float currentX = 0.0f;
    public float currentY = 0.0f;

    // Camera Sensitivity
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;

    // Raycasting So I dont Collide with terrain
    private Ray ray;
    private RaycastHit hit;
    public Transform target;

    public Camera fpCam;

    // Boolean For Zooming
    //public bool bIsZoomed;

    void Start()
    {
        cameraTransform = transform;
        cam = Camera.main;
        fpCam = lookAt.GetComponent<CharacterShooting>().fpCam;
        //bIsZoomed = false;
    }

    void Update()
    {
        if (lookAt.GetComponent<CharacterShooting>().ADS == false)
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            currentX = Mathf.Clamp(currentX, 0.0f, 80.0f);
            Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
            cameraTransform.position = lookAt.position + rotation * dir;
            cameraTransform.LookAt(lookAt.position);
            GetComponent<Camera>().enabled = true;
        }
        else
        {
            currentX = Mathf.Clamp(currentX, -80.0f, 80.0f);
            fpCam.transform.Rotate(new Vector3(1, 0, 0), currentX, Space.Self);
            currentX = 0.0f;
            GetComponent<Camera>().enabled = false;
        }
    }
}
