using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class AudioSettings
    {
        public Slider master;
        [Space(5)]

        public Slider music;
        public Slider sfx;
        public Slider voice;
    }

    // ----------------------
    // Variables
    // ----------------------

    [Header("Menu")]
    [SerializeField] Settings settings;
    [Space(15)]

    [SerializeField] AudioSettings audioSettings;

    // ----------------------
    // Functions
    // ----------------------

    void Start()
    {
        // Set Values

        // Call Functions
        ApplySettings();
    }

    void ApplySettings()
    {
        // Audio Settings
        float masterSliderV = settings.audio.masterV;
        float musicSliderV = settings.audio.musicV;
        float sfxSliderV = settings.audio.sfxV;
        float voiceSliderV = settings.audio.voiceV;

        if(audioSettings.master != null)
        audioSettings.master.value = masterSliderV;

        if(audioSettings.music != null)
        audioSettings.music.value = musicSliderV;

        if(audioSettings.sfx != null)
        audioSettings.sfx.value = sfxSliderV;

        if(audioSettings.voice != null)
        audioSettings.voice.value = voiceSliderV;

        ApplyAudioSettings();
    }

    public void QuitGame()
    {
        // Call Quit Game In LevelManager
        LevelManager.instance.StartCoroutine("QuitGame");
    }

    // Settings
    // ----------------------

    public void ApplyAudioSettings()
    {
        // Set Values
        if(audioSettings.master)
        {
            float volume = audioSettings.master.value;
            settings.audio.masterV = audioSettings.master.value;
            settings.audio.mixer.SetFloat("masterV", Mathf.Log10(settings.audio.masterV) * 20);
        }
        
        if(audioSettings.music)
        {
            float volume = audioSettings.music.value;
            settings.audio.musicV = audioSettings.music.value;
            settings.audio.mixer.SetFloat("musicV", Mathf.Log10(settings.audio.musicV) * 20);
        }

        if(audioSettings.sfx)
        {
            float volume = audioSettings.sfx.value;
            settings.audio.sfxV = audioSettings.sfx.value;
            settings.audio.mixer.SetFloat("sfxV", Mathf.Log10(settings.audio.sfxV) * 20);
        }

        if(audioSettings.voice)
        {
            float volume = audioSettings.voice.value;
            settings.audio.voiceV = audioSettings.voice.value;
            settings.audio.mixer.SetFloat("voiceV", Mathf.Log10(settings.audio.voiceV) * 20);
        }
    }


}