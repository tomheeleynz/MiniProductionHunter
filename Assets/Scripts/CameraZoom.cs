using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject player;
    public GameObject thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = player.transform.forward;
        transform.rotation = Quaternion.Euler(thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentX, thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentY, 0);
    }
}
