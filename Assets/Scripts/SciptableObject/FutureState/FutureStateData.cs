using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="FutureState_000",menuName ="Scriptable Object/FutureState")]

public class FutureStateData : ScriptableObject
{
    // ----------------------
    // Variables
    // ----------------------

    [Header("Future State")]
    public int stateIndex;

    // ----------------------
    // Functions
    // ----------------------

    public void AddIndex()
    {
        // Set Values
        stateIndex++;
    }    
}
