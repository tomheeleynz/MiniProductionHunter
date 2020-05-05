using System.Collections;
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
    private Camera cam;
    private float currentValue;
    private float lastValue;
    

    // Vectors
    Vector2 Move;
    Vector3 Rotate;

    // Rigid Body For Movement
    private Rigidbody rb;

    private float bowStuff;

    private void Awake()
    {
        _controls = new PlayerControls();
        
        // Movement
        _controls.Gameplay.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += ctx => Move = Vector2.zero;

        // Camera Rotation
        _controls.Gameplay.Rotate.performed += ctx => Rotate = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Rotate.canceled += ctx => Rotate = Vector2.zero;

        _controls.Gameplay.Fire.performed += ctx => DrawBow(ctx.ReadValue<float>());
        _controls.Gameplay.Fire.canceled += ctx => FireBow();
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
        //bow.GetComponent<Animator>().enabled = true;
        //GameObject arrowObj = Instantiate(arrow, transform.position + transform.forward * 10, Quaternion.identity) as GameObject;
        //arrowObj.GetComponent<Rigidbody>().AddForce(transform.forward * 10);


        float deltaValue = currentValue - lastValue;

        if (deltaValue > 0.1)
        {
            bow.GetComponent<Animator>().enabled = true;
        }
        else {
            bow.GetComponent<Animator>().enabled = false;
        }

        lastValue = currentValue;
    }

    private void FireBow()
    {
        bow.GetComponent<Animator>().enabled = true;
        GameObject shootingArrow = Instantiate(arrow, firingPosition.position, Quaternion.identity) as GameObject;
        shootingArrow.GetComponent<Rigidbody>().AddForce(transform.forward * 20);
    }

}
