using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="Settings", menuName ="Scriptable Object/Settings")]

public class Settings : ScriptableObject
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class AudioSettings
    {
        public AudioMixer mixer;
        [Space(10)]

        public float masterV;
        [Space(5)]
        
        public float sfxV;
        public float musicV;
        public float voiceV;
    }

    [System.Serializable]
    public class PlayerSettings
    {
        [Range(10, 70)]
        public float sensY;

        [Range(10, 70)]
        public float sensX;
        [Space(5)]

        public float lowestXAxis = -90f;
        public float highestXAxis = 90f;
    }

    // ----------------------
    // Variables
    // ----------------------

    [Header("Settings")]
    public PlayerSettings player;
    [Space(15)]

    public AudioSettings audio;
}
