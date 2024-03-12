using UnityEngine.Events;
using UnityEngine;

public class Rewindable : MonoBehaviour
{
    // ----------------------
    // Variables
    // ----------------------

    [HideInInspector]
    public bool alreadyRewinded;
    public UnityEvent action;
}
