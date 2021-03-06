﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;


public class FirstPersonPlayerMovement : MonoBehaviour
{
    public PlayerControls _controls;

    [SerializeField] public float moveSpeed;
    [SerializeField] public GameObject fpCamera;
    [SerializeField] public GameObject bow;
    [SerializeField] public GameObject arrow;
    [SerializeField] public float m_FieldOfView;
    [SerializeField] public Transform firingPosition;
    [SerializeField] public GameObject crossHair;

    private Camera cam;
    private float currentValue;
    private float lastValue;
    private float bowForce = 40;
    private float bowForceMultiplier;
    private float cameraAngle;
    private float lerpTimer;

    // Vectors
    [HideInInspector]
    public Vector2 Move;
    Vector3 Rotate;

    // Rigid Body For Movement
    private Rigidbody rb;

    private float bowStuff;

    private void Awake()
    {
        Cursor.visible = false;
        _controls = new PlayerControls();
        
        // Movement
        _controls.Gameplay.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += ctx => Move = Vector2.zero;

        // Camera Rotation
        _controls.Gameplay.Rotate.performed += ctx => Rotate = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Rotate.canceled += ctx => Rotate = Vector2.zero;

        _controls.Gameplay.Fire.performed += ctx => DrawBow(ctx.ReadValue<float>());
        _controls.Gameplay.Fire.canceled += ctx => FireBow();

        _controls.Gameplay.Interactable.started += ctx => InteractWithStuff();
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
        cameraAngle = fpCamera.GetComponent<FirstPersonCamera>().currentX;

        fpCamera.GetComponent<FirstPersonCamera>().currentX -= Rotate.y * 2.0f;
        fpCamera.GetComponent<FirstPersonCamera>().currentY += Rotate.x * 2.0f;

        transform.rotation = Quaternion.Euler(0, fpCamera.GetComponent<FirstPersonCamera>().currentY, 0);
        fpCamera.GetComponent<Camera>().fieldOfView = m_FieldOfView;

        // Set Bow Rotation
        Vector3 bowAngle = bow.transform.rotation.eulerAngles;
        bow.transform.rotation = Quaternion.Euler(fpCamera.GetComponent<FirstPersonCamera>().currentX, bowAngle.y, bowAngle.z);
    }

    private void FixedUpdate()
    {
        Vector3 MoveDirection = (transform.right * moveSpeed * Move.x) + (transform.forward * moveSpeed * Move.y);
        MoveDirection += transform.position;
        float terrainSampleHeight = Terrain.activeTerrain.SampleHeight(MoveDirection) + 2.5f;

        if (terrainSampleHeight > 3.0f) //Not below water line
        {
            MoveDirection.y = terrainSampleHeight;
            rb.MovePosition(MoveDirection);
        }
        
    }

    private void DrawBow(float currentValue)
    {
        AudioSource DrawSound = bow.GetComponent<AudioSource>();

        m_FieldOfView = 50;

        crossHair.SetActive(true);

        if (currentValue > lastValue) {
            bowForceMultiplier = currentValue;
            //bow.GetComponent<Animator>().enabled = true;
        }
        else {
            //bow.GetComponent<Animator>().enabled = false;
        }

        Debug.Log("Force Multiplier Value: " + bowForceMultiplier);

        //if (deltaValue > 0.05) {
        //    bow.GetComponent<Animator>().enabled = true;

        //    if (!DrawSound.isPlaying)
        //    {
        //        DrawSound.Play();+
        //    }
        //}
        //else 
        //{
        //    bow.GetComponent<Animator>().enabled = false;
        //    DrawSound.Stop();
        //}

        lastValue = currentValue;
    }

    private void FireBow()
    {
        m_FieldOfView = 60;
        crossHair.SetActive(false);

        //bow.GetComponent<Animator>().enabled = true;

        Debug.Log("Bow Force Multiplier On Shoot: " + bowForceMultiplier);
        GameObject shootingArrow = Instantiate(arrow, firingPosition.position, Quaternion.identity) as GameObject;

        shootingArrow.GetComponent<Rigidbody>().AddForce((transform.forward/Mathf.Cos(Mathf.Deg2Rad * cameraAngle)) * (bowForce * bowForceMultiplier));
    }

    private void InteractWithStuff()
    {
        Debug.Log("Interacting");

        GetComponent<Interaction>().OnInteract();
    }

}
