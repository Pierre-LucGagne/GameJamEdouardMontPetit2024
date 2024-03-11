using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Object/Player Data", fileName ="PlayerData")]

public class PlayerData : ScriptableObject
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class Item
    {
        public string name;
        public int count;
    }

    // ----------------------
    // Variables
    // ----------------------

    public List<Item> items;
}
