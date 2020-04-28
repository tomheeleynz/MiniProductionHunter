using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayerScript : MonoBehaviour
{
    Vector3 center = new Vector3(0,0,0);
    Vector3 axis =  Vector3.up;
    float radius = 20.0f;
    float radiusSpeed = 0.1f;
    float rotationSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = (transform.position - center).normalized * radius + center;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if( radius==0.0f)
            {
                radius = 20.0f;
            }
            else
            {
                radius = 0.0f;
            }
            transform.position = (transform.position - center).normalized * radius + center;
        }
        transform.RotateAround(center, axis, rotationSpeed * Time.deltaTime);
        var desiredPosition = (transform.position - center).normalized * radius + center;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }
}
