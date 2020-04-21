using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Player Controller Generated Script
    public PlayerControls _controls;
    [HideInInspector] public CharacterShooting player;
    public ControllerSetup controllers;
    
    public Dictionary<int, Vector2> moveMap = new Dictionary<int, Vector2>();
    public Dictionary<int, Vector2> rotationMap = new Dictionary<int, Vector2>();

    // Vectors for JoyStick Axis
    Vector2 Move;
    Vector2 Rotation;
    Vector2 Move2;
    Vector2 Rotation2;

    float DrawBow;
    Vector2 Fire;
    public float moveSpeed;
    public float jumpForce;
    public bool invertCamera = false;
    
    // Device ID
    public int deviceID;

    // PlayerID
    public int playerID;

    // Getting RigidBody off Player
    private Rigidbody rb;

    // Getting Third Person Camera
    public GameObject thirdPersonCamera;


    // Getting Animator 
    private Animator animator;

    PlayerManager manager;

    void Awake()
    {
        player = GetComponentInParent<CharacterShooting>();

        // Getting PlayerControls Script
        _controls = new PlayerControls();

        _controls.devices = InputSystem.devices;

        // Left Stick For Movement
        _controls.Gameplay.Move.performed += ctx => MoveFunc(ctx, "performed");
        _controls.Gameplay.Move.canceled += ctx => MoveFunc(ctx, "cancelled");

        // Sprinting
        _controls.Gameplay.LStickDown.performed += ctx => RunningFunc(ctx);
        _controls.Gameplay.LStickDown.canceled += ctx => RunningFunc(ctx);

        // Crouching
        _controls.Gameplay.Crouch.performed += ctx => CrouchFunc(ctx);
        _controls.Gameplay.Crouch.canceled += ctx => CrouchFunc(ctx);
        
        _controls.Gameplay.LStickDown.canceled += ctx => moveSpeed = 1;

        // Right Stick For Camera Rotation
        _controls.Gameplay.Rotate.performed += ctx => RotationFunc(ctx, "performed");
        _controls.Gameplay.Rotate.canceled += ctx => RotationFunc(ctx, "cancelled");

        // Fire Button
        _controls.Gameplay.Fire.performed += ctx => TakeShotFunc(ctx, "performed");
        _controls.Gameplay.Fire.canceled += ctx => TakeShotFunc(ctx, "cancelled");

        // Trigger for Draw Bow
        _controls.Gameplay.DrawBow.performed += ctx => DrawBowFunc(ctx, "performed");
        //_controls.Gameplay.DrawBow.performed += ctx => player.shooting = true;
        _controls.Gameplay.DrawBow.canceled += ctx => DrawBowFunc(ctx, "cancelled");

        // Jump Button
        _controls.Gameplay.Jump.performed += ctx => JumpFunc(ctx);

        // Getting RigidBody for Movement
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        controllers = ControllerSetup.instance;
        manager = PlayerManager.instance;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    private void MoveFunc(InputAction.CallbackContext ctx, string method)
    {
        //if (ctx.control.device.deviceId == deviceID)
        //{
        //    if (deviceID == controllers.takenIDs[0])
        //    {
        //        if (method == "performed")
        //        {
        //            Move = ctx.ReadValue<Vector2>();
        //        }
        //        else if (method == "cancelled")
        //        {
        //            Move = Vector2.zero;
        //        }
        //    }
        //    else if (deviceID == controllers.takenIDs[1])
        //    {
        //        if (method == "performed")
        //        {
        //            Move2 = ctx.ReadValue<Vector2>();
        //        }
        //        else if (method == "cancelled")
        //        {
        //            Move2 = Vector2.zero;
        //        }
        //    }
        //}

        if (ctx.control.device.deviceId == deviceID)
        { 
            if (method == "performed")
            {
                animator.SetBool("bIsWalking", true);
                manager.moveMap[playerID] = ctx.ReadValue<Vector2>();
            }
            else if (method == "cancelled")
            {
                animator.SetBool("bIsWalking", false);
                manager.moveMap[playerID] = Vector2.zero;
            }
        }
    }


    private void RotationFunc(InputAction.CallbackContext ctx, string method)
    {
        if (ctx.control.device.deviceId == deviceID)
        {
            if (method == "performed")
            {
                Rotation = ctx.ReadValue<Vector2>();
            }
            else if (method == "cancelled")
            {
                Rotation = Vector2.zero;
            }
        }
    }

    private void CrouchFunc(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device.deviceId == deviceID)
        {
            if (player.crouching == true)
            {
                player.crouching = false;
            }
            else
            {
                player.crouching = true;
            }
        }
      
    }

    private void RunningFunc(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device.deviceId == deviceID)
        {
            if (player.running == true)
            {
                player.running = false;
            }
            else
            {
                player.running = true;
            }
        }
    }

    private void JumpFunc(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device.deviceId == deviceID)
        {
            float distToGround = GetComponent<Collider>().bounds.extents.y;
            if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f)) //If touching ground
            {
                player.jumping = true;
                rb.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    private void TakeShotFunc(InputAction.CallbackContext ctx, string method)
    {
        if (ctx.control.device.deviceId == deviceID)
        {
            if (method == "performed")
            {
                player.shooting = true;
            }
            if (method == "cancelled")
            {
                player.TakeShot();
                player.shooting = false;
            }
        }
    }

    private void DrawBowFunc(InputAction.CallbackContext ctx, string method)
    {
        if (ctx.control.device.deviceId == deviceID)
        {
            if (method == "performed")
            {
                animator.SetBool("bDrawingBow", true);
                DrawBow = ctx.ReadValue<float>();
                //player.shooting = true;
                player.ADS = true;
            }
            else if (method == "cancelled")
            {
               // player.shooting = false;
                player.ADS = false;
                animator.SetBool("bDrawingBow", false);
                player.shooting = false;
            }
        }
    }

    private void OnDisable()
    {
        _controls.Gameplay.Disable();
    }

    void Update()
    {
        if (player.crouching == true)
        {

            player.running = false;

            moveSpeed = 0.7f;

        }
        else if (player.running == true)

        {

            moveSpeed = 1.6f;

        }
        else
        {
            moveSpeed = 1.0f;
        }

        // Movement Code
        if (!invertCamera)
        {
            thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentX += Rotation.y * 2.0f;
            thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentY += Rotation.x * 2.0f;
        }
        else
        { 
            thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentX -= Rotation.y * 2.0f;
            thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentY += Rotation.x * 2.0f;
        }

        transform.rotation = Quaternion.Euler(0, thirdPersonCamera.GetComponent<ThirdPersonCamera>().currentY, 0);

        // Calculating Move Direction
        Vector3 moveDirection = (transform.right * manager.moveMap[playerID].x * 5.0f) + (transform.forward * 5.0f * manager.moveMap[playerID].y);
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        
    }

}