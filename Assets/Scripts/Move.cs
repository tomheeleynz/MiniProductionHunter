using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Matching Player to Camera Rotation
        Quaternion characterRotaton = Camera.main.transform.rotation;
        characterRotaton.x = 0;
        characterRotaton.z = 0;
        transform.rotation = characterRotaton;


        // Moving Player with Game Controller
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(mH * 5.0f, rb.velocity.y, mV * 5.0f);
    }
}
