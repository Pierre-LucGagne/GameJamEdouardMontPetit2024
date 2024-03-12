using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Socket : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class Sockets
    {
        public Transform objectsParent;
        [Space(5)]

        public Vector3 localPosition;
        public Quaternion localRotation;
        [Space(5)]

        [HideInInspector]
        public ItemData currentItem;
        public ItemData requiredItem;
    }

    // ----------------------
    // Variables
    // ----------------------

    int currentIndex;

    [Header("Socket Settings")]
    [SerializeField] UnityEvent whenCompleted;
    [Space(5)]

    [SerializeField] ItemData[] acceptedObjects;
    [Space(10)]

    [SerializeField] Sockets[] sockets;

    // ----------------------
    // Functions
    // ----------------------

    public void DecideInteraction(int index)
    {
        // Set Values
        currentIndex = index;

        // Call Functions
        if(sockets.Length >= index && index >= 0)
        {
            if(sockets[currentIndex].currentItem)
            RemoveObjectFromSocket();

            else
            AddObjectToSocket();
        }

        else
        Debug.Log("Outside Bounds");
    }

    void AddObjectToSocket()
    {
        // Item Data
        PlayerData playerData = PlayerController.current.data;
        ItemData item = null;

        for(int inventoryIndex = 0;inventoryIndex < playerData.inventory.Count;inventoryIndex++)
        {
            for(int objectIndex = 0;objectIndex < acceptedObjects.Length;objectIndex++)
            {
                if(playerData.inventory[inventoryIndex] == acceptedObjects[objectIndex])
                {
                    item = playerData.inventory[inventoryIndex];
                    break;
                }
            }

            if(item)
            break;
        }

        if(item)
        {
            // Set Values
            sockets[currentIndex].currentItem = item;

            // Instantiate
            var instance = Instantiate(item.prefab);
            instance.transform.SetParent(sockets[currentIndex].objectsParent);

            instance.transform.localPosition = sockets[currentIndex].localPosition;
            instance.transform.localRotation = sockets[currentIndex].localRotation;

            // Call Functions
            PlayerController.current.RemoveItem(item);
            VerifySockets();
            Debug.Log(1);
        }

        else
        Debug.Log("nope");
    }

    void RemoveObjectFromSocket()
    {
        // Set Values
        ItemData item = sockets[currentIndex].currentItem;
        sockets[currentIndex].currentItem = null;

        // Destroy Child
        Destroy(sockets[currentIndex].objectsParent.GetChild(0).gameObject);

        // Add Object to Player
        PlayerController.current.AddItem(item);
    }

    void VerifySockets()
    {
        Debug.Log(2);
        // Set Values
        bool isCompleted = true;

        // Check All Sockets
        for(int i = 0;i < sockets.Length;i++)
        {
            if(sockets[i].currentItem != sockets[i].requiredItem)
            {
                isCompleted = false;
                break;
            }
        }

        if(isCompleted)
        {
            whenCompleted.Invoke();
            Debug.Log(3);
        }
    }
}
