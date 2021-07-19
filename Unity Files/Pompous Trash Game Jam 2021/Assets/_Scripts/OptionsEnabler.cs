using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsEnabler : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] bool inMenu = false;
    void Awake()
    {
        LoadValues();
    }

    void Start()
    {
        // Required for when a scene starts, awake does not work for that on mixers
        LoadValues();
    }

    void LoadValues()
    {
        float soundVol = PlayerPrefs.GetFloat(OptionsMenuController.P_SoundVolume);
        mixer.SetFloat("AudioVolume", Mathf.Log10(soundVol) * 20);

        float musicVol = PlayerPrefs.GetFloat(OptionsMenuController.P_MusicVolume);
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);

        float graphicsQual = PlayerPrefs.GetFloat(OptionsMenuController.P_GraphicsQuality);
        if(graphicsQual != QualitySettings.GetQualityLevel())
        {
            PlayerPrefs.SetFloat(OptionsMenuController.P_GraphicsQuality, graphicsQual);
        }
    }
}
