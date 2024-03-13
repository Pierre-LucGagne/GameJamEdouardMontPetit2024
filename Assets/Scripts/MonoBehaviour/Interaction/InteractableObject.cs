using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    // ----------------------
    // Variables
    // ----------------------

    [Header("Interactable Object")]
    public bool interactOnce;
    public bool alreadyInteracted;
    [Space(5)]

    public UnityEvent interactAction;
    [Space(10)]

    public string requiredItem;
    [Space(5)]

    [TextArea(1,3)]
    public string cantInteractMessage;

    // ----------------------
    // Functions
    // ----------------------

    public void ChangeScene(string sceneName)
    {
        // Call Functions
        LevelManager.instance.LoadAsyncScene(sceneName);
    }
}