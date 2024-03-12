using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemData_000", menuName ="Scriptable Object/ItemData")]

public class ItemData : ScriptableObject
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class UiSettings
    {
        public Sprite sprite;
        public string name;
    }

    // ----------------------
    // Variables
    // ----------------------

    [Header("Item Data")]
    public string itemName;
    public GameObject prefab;
    [Space(5)]

    [TextArea(2, 4)]
    public string description;
    [Space(10)]

    public UiSettings ui;
}
