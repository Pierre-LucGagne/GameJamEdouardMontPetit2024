using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable Object/Player Data", fileName ="PlayerData")]

public class PlayerData : ScriptableObject
{
    // ----------------------
    // Class
    // ----------------------

    // ----------------------
    // Variables
    // ----------------------

    public List<ItemData> inventory;
}
