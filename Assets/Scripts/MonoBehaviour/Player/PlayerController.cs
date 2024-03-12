using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class ModaleSettings
    {

    }

    [System.Serializable]
    public class CursorSettings
    {
        public Transform objectTrans;
        [Space(5)]

        public Color baseColor;
        public Color hoverColor;
        [Space(5)]

        public Vector3 baseSize;
        public Vector3 hoverSize;
        [Space(5)]

        public float transitionTime;

        [HideInInspector]
        public bool hovering;
    }

    [System.Serializable]
    public class InventorySettings
    {
        public Transform canvas;
        public GameObject prefab;
        [Space(5)]

        public float fadeDuration;
    }

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
    [SerializeField] PlayerData data;
    [SerializeField] Settings settings;
    [Space(15)]

    [Header("Camera")]
    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    [Header("Movement")]
    Rigidbody rb;

    [SerializeField] float walkSpeed;
    [SerializeField] float walkStepInterval;
    [Space(5)]

    [SerializeField] float runSpeed;
    [SerializeField] float runStepInterval;
    [Space(10)]

    [SerializeField] RandomAudio footstep;
    float stepInterval;

    float rotationX;
    float rotationY;

    [Header("Interaction")]
    [Range(.15f, 1.5f)]
    [SerializeField] float rayLength = .5f;
    [SerializeField] InteractableObject interactableObject;

    [Header("UI Settings")]
    [SerializeField] Notifications notifCanvas;
    [SerializeField] OpenPage modalePage;
    [SerializeField] CursorSettings cursor;
    [SerializeField] InventorySettings inventory;

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

        // Call Functions
        SetInventory();
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
        Footstep();
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
            {
                interactableObject = hitObject.GetComponent<InteractableObject>();
                
                if(!cursor.hovering)
                StartCoroutine("ShowHoverInteraction");
            }

            else
            {
                // Set Values
                interactableObject = null;

                // Call Functions
                if(cursor.hovering)
                StartCoroutine("HideHoverInteraction");
            }
        }

        else
        {
            // Set Values
            interactableObject = null;

            // Call Functions
            if(cursor.hovering)
            StartCoroutine("HideHoverInteraction");
        }
    }

    IEnumerator ShowHoverInteraction()
    {
        // Stop Coroutines
        StopCoroutine("HideHoverInteraction");

        // Set Values
        cursor.hovering = true;

        Image sprite = cursor.objectTrans.GetComponent<Image>();

        cursor.objectTrans.localScale = cursor.baseSize;
        sprite.color = cursor.baseColor;

        // Play Animations
        cursor.objectTrans.DOScale(cursor.hoverSize, cursor.transitionTime).SetEase(Ease.InOutCirc);
        sprite.DOColor(cursor.hoverColor, cursor.transitionTime);

        yield return new WaitForSeconds(cursor.transitionTime);

        cursor.objectTrans.localScale = cursor.hoverSize;
        sprite.color = cursor.hoverColor;
    }

    IEnumerator HideHoverInteraction()
    {
        // Stop Coroutines
        StopCoroutine("ShowHoverInteraction");

        // Set Values
        cursor.hovering = false;

        Image sprite = cursor.objectTrans.GetComponent<Image>();

        cursor.objectTrans.localScale = cursor.hoverSize;
        sprite.color = cursor.hoverColor;

        // Play Animations
        cursor.objectTrans.DOScale(cursor.baseSize, cursor.transitionTime).SetEase(Ease.InOutCirc);
        sprite.DOColor(cursor.baseColor, cursor.transitionTime);

        yield return new WaitForSeconds(cursor.transitionTime);

        cursor.objectTrans.localScale = cursor.baseSize;
        sprite.color = cursor.baseColor;
    }

    // Inventory Functions
    // ----------------------

    void SetInventory()
    {
        // Instantiate All Items
        for(int i = 0;i < data.inventory.Count;i++)
        {
            // Create UI Element Inside the desired Canvas
            var instance = Instantiate(inventory.prefab);
            instance.transform.SetParent(inventory.canvas);

            // Set Values
            CanvasGroup group = instance.GetComponent<CanvasGroup>();
            Image sprite = instance.transform.Find("Background/Sprite").GetComponent<Image>();
            TextMeshProUGUI label = instance.transform.Find("Label").GetComponent<TextMeshProUGUI>();

            // Set UI Element
            group.alpha = 0;
            group.DOFade(1, inventory.fadeDuration).SetEase(Ease.InOutCirc);
            
            sprite.sprite = data.inventory[i].ui.sprite;
            label.text = data.inventory[i].ui.name;
        }
    }

    public void AddItem(ItemData item)
    {
        // Add Item to List
        data.inventory.Add(item);

        // Create UI Element Inside the desired Canvas
        var instance = Instantiate(inventory.prefab);
        instance.transform.SetParent(inventory.canvas);

        // Set Values
        CanvasGroup group = instance.GetComponent<CanvasGroup>();
        Image sprite = instance.transform.Find("Background/Sprite").GetComponent<Image>();
        TextMeshProUGUI label = instance.transform.Find("Label").GetComponent<TextMeshProUGUI>();

        // Set UI Element
        group.alpha = 0;
        group.DOFade(1, inventory.fadeDuration).SetEase(Ease.InOutCirc);
          
        sprite.sprite = item.ui.sprite;
        label.text = item.ui.name;
    }

    public void RemoveItem(ItemData item)
    {
        // Search Inside the Inventory
        for(int i = 0;i < data.inventory.Count;i++)
        {
            if(data.inventory[i] == item)
            {
                // Remove From List
                data.inventory.RemoveAt(i);

                // Remove From UI Inventory
                CanvasGroup uiElement = inventory.canvas.GetChild(i).GetComponent<CanvasGroup>();
                uiElement.DOFade(0, inventory.fadeDuration).SetEase(Ease.InOutCirc);

                StartCoroutine(DeleteObject(uiElement.transform.gameObject, inventory.fadeDuration));

                return;
            }
        }
    }

    bool CheckInventory(string itemName)
    {
        for(int i = 0;i < data.inventory.Count;i++)
        {
            // Check If items match
            if(itemName == data.inventory[i].itemName)
            return true;
        }

        return false;
    }

    IEnumerator DeleteObject(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);

        // Destroy Given Object
        Destroy(gameObject);
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

    void Footstep()
    {
        // Set Values
        if(input.movePos != Vector2.zero)
        {
            if(stepInterval <= 0)
            {
                footstep.PlayAudio();

                if(input.running)
                stepInterval += runStepInterval;

                else
                stepInterval += walkStepInterval;
            }

            stepInterval -= Time.fixedDeltaTime;
        }
    }

    // ----------------------
    // Player Input
    // ----------------------

    public void OnInteract()
    {
        if(modalePage.modaleIsOpen)
        modalePage.CloseModale();

        else if(interactableObject != null)
        {
            if(interactableObject.requiredItem == "" || CheckInventory(interactableObject.requiredItem))
            {
                if(interactableObject.interactOnce)
                {
                    if(!interactableObject.alreadyInteracted)
                    interactableObject.interactAction.Invoke();
                }

                else
                interactableObject.interactAction.Invoke();

                interactableObject.alreadyInteracted = true;
            }

            else
            notifCanvas.SetValues(interactableObject.cantInteractMessage);
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