using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    // ----------------------
    // Variables
    // ----------------------

    [Header("Change State")]
    [SerializeField] FutureStateData stateData; 

    // ----------------------
    // Functions
    // ----------------------

    void Start()
    {
        // Call Functions
        SelectState();
    }

    void SelectState()
    {
        // Set Values
        int index = stateData.stateIndex;

        // Look At Current Object Children
        if(stateData.stateIndex >= transform.childCount)
        index = transform.childCount -1;

        // Deactivate everything Except selected Children
        for(int i = 0;i < transform.childCount;i++)
        {
            if(i == index)
            transform.GetChild(i).gameObject.SetActive(true); 

            else
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void AddIndex()
    {
        // Set Values
        stateData.stateIndex++;
        int index = stateData.stateIndex;

        // Look At Current Object Children
        if(stateData.stateIndex >= transform.childCount)
        index = transform.childCount -1;

        // Deactivate everything Except selected Children
        for(int i = 0;i < transform.childCount;i++)
        {
            if(i == index)
            transform.GetChild(i).gameObject.SetActive(true); 

            else
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
