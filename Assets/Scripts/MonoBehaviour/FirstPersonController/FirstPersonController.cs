using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class InputReceiver
    {
        public Vector2 movePos;
        [Space(5)]

        public Vector2 lookPos;
        [Space(5)]

        public bool running;
    }

    // ----------------------
    // Variables
    // ----------------------

    [Header("Player Controller")]
    [SerializeField] Settings settings;
    [Space(15)]

    [Header("Camera")]
    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    [Header("Movement")]
    Rigidbody rb;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    float rotationX;
    float rotationY;
    [Space(15)]

    [Header("Input")]
    [SerializeField] InputReceiver input;

    // ----------------------
    // Functions
    // ----------------------

    // Start Functions
    // ----------------------

    void Start()
    {
        // Set Values
        rb = GetComponent<Rigidbody>();

        rotationX = cam.transform.rotation.x;
        rotationY = cam.transform.rotation.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update Functions
    // ----------------------

    void Update()
    {
        // Call Functions
        LookAroud();
    }

    void FixedUpdate()
    {
        // Call Functions
        MovePlayer();
    }

    // Movement Functions
    // ----------------------

    void LookAroud()
    {
        // Set Values
        float lookX = settings.player.sensX * input.lookPos.x * Time.deltaTime;
        float lookY = settings.player.sensX * input.lookPos.y * Time.deltaTime;

        rotationX -= lookY;
        rotationY += lookX;
        
        rotationX = Math.Clamp(rotationX, settings.player.lowestXAxis, settings.player.highestXAxis);

        Debug.Log(rotationX);
        Debug.Log(rotationY);

        // Set Camera Rotation
        cam.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); 
    }

    void MovePlayer()
    {
        // Set Values
        Vector3 moveX = orientation.right * input.movePos.x;
        Vector3 moveY = orientation.forward * input.movePos.y;

        Vector3 moveDir = (moveX + moveY).normalized;

        // Add Force to Player
        if(!input.running)
        rb.AddForce(moveDir * walkSpeed, ForceMode.Impulse);

        else
        rb.AddForce(moveDir * runSpeed, ForceMode.Impulse);
    }

    // ----------------------
    // Player Input
    // ----------------------

    public void OnMove(InputValue value)
    {
        // Set Values
        input.movePos = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        // Set Values
        input.lookPos = value.Get<Vector2>();
    }
}
