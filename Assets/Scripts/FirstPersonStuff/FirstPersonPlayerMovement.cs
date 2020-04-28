using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FirstPersonPlayerMovement : MonoBehaviour
{
    public PlayerControls _controls;

    [SerializeField] public float moveSpeed;

    // Vectors
    Vector2 Move;

    // Rigid Body For Movement
    private Rigidbody rb;

    private void Awake()
    {
        _controls = new PlayerControls();
        
        // Movement
        _controls.Gameplay.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Move.canceled += ctx => Move = Vector2.zero;
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MoveDirection = (transform.right * moveSpeed * Move.x) + (transform.forward * moveSpeed  * Move.y);
    }


}
