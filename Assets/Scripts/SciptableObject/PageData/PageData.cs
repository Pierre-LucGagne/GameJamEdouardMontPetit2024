using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PageData_000", menuName ="Scriptable Object/Page Data")]

public class PageData : ScriptableObject
{
    // ----------------------
    // Variables
    // ----------------------

    [Header("Page Data")]
    public string title;

    [TextArea(2,10)]
    public string text;
}
