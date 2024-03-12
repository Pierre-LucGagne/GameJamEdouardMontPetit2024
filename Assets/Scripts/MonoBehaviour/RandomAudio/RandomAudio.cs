using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    // ----------------------
    // Variables
    // ----------------------

    [Header("Audio Settings")]
    [SerializeField] AudioClip[] audios;
    [Space(10)]

    [Range(0,1)]
    [SerializeField] float minVolume = .5f;
    [Range(0,1)]
    [SerializeField] float maxVolume = 1;

    AudioSource source;

    // ----------------------
    // Functions
    // ----------------------

    void Start()
    {
        // Set Values
        source = GetComponent<AudioSource>();
    }

    // Audio Functions
    // ----------------------

    int RandomInt(int minInt, int maxInt)
    {
        // Set Values
        int randomInt = Random.Range(minInt, maxInt);
        
        // Return Value
        return randomInt;
    }

    float RandomFloat(float minFloat, float maxFloat)
    {
        // Set Values
        float randomFloat = Random.Range(minFloat, maxFloat);
        
        // Return Value
        return randomFloat;
    }

    public void PlayAudio()
    {
        if(audios.Length > 0)
        {
            // Set Values
            source.clip = audios[RandomInt(0, audios.Length)];

            source.volume = RandomFloat(minVolume, maxVolume);

            // Play Audio
            source.Play();
        }

        else
        Debug.Log("Add Audio in : " + name + " RandomAudio Script.");
    }
}
