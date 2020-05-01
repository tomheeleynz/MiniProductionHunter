﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FirstPersonPlayerMovement : MonoBehaviour
{
    public PlayerControls _controls;

    [SerializeField] public float moveSpeed;
    [SerializeField] public GameObject fpCamera;
    [SerializeField] public GameObject bow;
    [SerializeField] public GameObject arrow;

    // Vectors
    Vector2 Move;
    Vector3 Rotate;

    // Rigid Body For Movement
    private Rigidbody rb;

    private void Awake()
    {
        _controls = new PlayerControls();
        
        // Movement
        _controls.Gameplay.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += ctx => Move = Vector2.zero;

        // Camera Rotation
        _controls.Gameplay.Rotate.performed += ctx => Rotate = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Rotate.canceled += ctx => Rotate = Vector2.zero;

        _controls.Gameplay.Fire.started += ctx => FireBow();
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fpCamera.GetComponent<FirstPersonCamera>().currentX -= Rotate.y * 2.0f;
        fpCamera.GetComponent<FirstPersonCamera>().currentY += Rotate.x * 2.0f;

        transform.rotation = Quaternion.Euler(0, fpCamera.GetComponent<FirstPersonCamera>().currentY, 0);
        //Vector3 MoveDirection = (transform.right * moveSpeed * Move.x) + (transform.forward * moveSpeed  * Move.y);
        //rb.velocity = new Vector3(MoveDirection.x, rb.velocity.y, MoveDirection.z);

    }
    private void FixedUpdate()
    {
        Vector3 MoveToPos = transform.position + new Vector3(moveSpeed * Move.x, 0, moveSpeed * Move.y);
        float terrainSampleHeight = Terrain.activeTerrain.SampleHeight(transform.position) + 2.5f;
        MoveToPos.y = terrainSampleHeight;
        rb.MovePosition(MoveToPos);    
    }
    private void FireBow()
    {
        GameObject arrowObj = Instantiate(arrow, transform.position + transform.forward * 10, Quaternion.identity) as GameObject;
        arrowObj.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
    }


}
