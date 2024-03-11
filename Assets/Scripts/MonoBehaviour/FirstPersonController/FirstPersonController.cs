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

    [Header("Interaction")]
    [Range(.15f, 1.5f)]
    [SerializeField] float rayLength = .5f;
    [SerializeField] InteractableObject interactableObject;

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
        CheckForInteraction();
        MovePlayer();
    }

    // Interaction
    // ----------------------

    void CheckForInteraction()
    {
        // Create Ray Cast
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        // Ray Cast Conditions
        if(Physics.Raycast(ray, out hit, rayLength))
        {
            // Set Values
            GameObject hitObject = hit.transform.gameObject;

            if(hitObject.GetComponent<InteractableObject>() != null)
            interactableObject = hitObject.GetComponent<InteractableObject>();

            else
            interactableObject = null;
        }

        else
        interactableObject = null;
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

    public void OnInteract()
    {
        // Interact with Object if currently looking at one
        if(interactableObject != null)
        {
            interactableObject.gameObject.SetActive(false);
            interactableObject = null;
        }
    }

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

    // VVV Change Code if you have Time VVV
    public void OnRun(InputValue value)
    {
        // Set Values
        float floatValue = value.Get<float>();

        if(floatValue > 0)
        input.running = true;

        else
        input.running = false;
    }
}
