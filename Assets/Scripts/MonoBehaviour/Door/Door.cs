using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class DoorAnimation
    {
        public string open;
        public string close;
    }

    // ----------------------
    // Variables
    // ----------------------

    [Header("Door Settings")]
    [SerializeField] bool locked;
    [Space(10)]

    [SerializeField] DoorAnimation trigger;
    [SerializeField] RandomAudio open;
    Animator animator;

    // ----------------------
    // Functions
    // ----------------------

    void Start()
    {
        // Set Values
        if(animator = GetComponent<Animator>())
        animator = GetComponent<Animator>();
    }

    void OpenDoor()
    {
        // Play Animation
        if(!locked && animator)
        {
            animator.SetTrigger(trigger.open);
            open.PlayAudio();
        }
    }

    void CloseDoor()
    {
        // Play Animation
        if(!locked && animator)
        {
            animator.SetTrigger(trigger.close);
            open.PlayAudio();
        }
    }

    public void UnlockDoor()
    {
        // Set Values
        locked = false;

        // Call Functions
        OpenDoor();
    }

    // ----------------------
    // Collision Detection
    // ----------------------

    private void OnTriggerEnter(Collider other)
    {
        // Call Functions
        if(other.GetComponent<PlayerController>())
        OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        // Call Functions
        if(other.GetComponent<PlayerController>())
        CloseDoor();
    }
}
