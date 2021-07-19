// Author: Gerad Paris
// Purpose: Handles options menu interaction with game related variables, stores changes in playerprefs to be loaded whenever user re-opens game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Linq;

public class OptionsMenuController : MonoBehaviour
{
    #region Playerprefs names 
    static public string P_GraphicsQuality = "graphicsQual";
    static public string P_ScreenResolution = "screenRes";
    static public string P_Fullscreen = "fullscreen";
    static public string P_SoundVolume = "soundVol";
    static public string P_MusicVolume = "musicVol";
    static public string P_SavedValues = "savedValues";
    #endregion

    #region Variables
    Resolution[] avaliableResolutions;
    [SerializeField] Dropdown graphicsQualDropdown;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider soundVolSlider;
    [SerializeField] Slider musicVolSlider;
    [SerializeField] AudioMixer mixer;

    bool inGameMenu;
    int currentResInd = -1;
    GameObject playerObj;
    #endregion

    void Awake()
    {
        #region Resolution Loading
        avaliableResolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        
        for (int i = 0; i < avaliableResolutions.Length; i++)
        {
            string option = avaliableResolutions[i].width + " x " + avaliableResolutions[i].height;
            options.Add(option);

            if (avaliableResolutions[i].width == Screen.currentResolution.width && avaliableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResInd = i;
            }
        }
        #endregion

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResInd;
        resolutionDropdown.RefreshShownValue();

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            // check for main game scene
            inGameMenu = true;
        }
        else
        {
            inGameMenu = false;
        }

        if (!(PlayerPrefs.HasKey(P_GraphicsQuality) && PlayerPrefs.HasKey(P_ScreenResolution) && PlayerPrefs.HasKey(P_Fullscreen) && PlayerPrefs.HasKey(P_SoundVolume)
            && PlayerPrefs.HasKey(P_MusicVolume)))
        {
            InitializeValues();
        }
        else
        {
            LoadValues();
        }
    }

    void InitializeValues()
    {
        // Initializes values into playerprefs
        PlayerPrefs.SetInt(P_GraphicsQuality, QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt(P_ScreenResolution, currentResInd);
        PlayerPrefs.SetInt(P_Fullscreen, 1);
        PlayerPrefs.SetFloat(P_SoundVolume, 1f);
        PlayerPrefs.SetFloat(P_MusicVolume, 1f);
        PlayerPrefs.SetInt(P_SavedValues, 1);
        LoadValues();
    }

    void LoadValues()
    {
        // Initializes saved values into the UI
        graphicsQualDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt(P_GraphicsQuality));
        resolutionDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt(P_ScreenResolution));
        fullscreenToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt(P_Fullscreen) != 0);

        float soundVol = PlayerPrefs.GetFloat(P_SoundVolume);
        soundVolSlider.SetValueWithoutNotify(soundVol);
        mixer.SetFloat("AudioVolume", Mathf.Log10(soundVol) * 20);

        float musicVol = PlayerPrefs.GetFloat(P_MusicVolume);
        musicVolSlider.SetValueWithoutNotify(musicVol);
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
    }

    public void SelectGraphicsQuality(int graphicsIndex)
    {
        QualitySettings.SetQualityLevel(graphicsIndex);
        PlayerPrefs.SetInt(P_GraphicsQuality, graphicsIndex);
    }

    public void SelectScreenResolution(int resolutionIndex)
    {
        Resolution res = avaliableResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt(P_ScreenResolution, resolutionIndex);
    }

    public void ToggleFullscreen(bool toggled)
    {
        Screen.fullScreen = toggled;
        PlayerPrefs.SetInt(P_Fullscreen, (toggled ? 1 : 0));
    }

    public void SetSoundVolume(float newSoundVolume)
    {
        mixer.SetFloat("AudioVolume", Mathf.Log10(newSoundVolume) * 20);
        PlayerPrefs.SetFloat(P_SoundVolume, newSoundVolume);
    }

    public void SetMusicVolume(float newMusicVolume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(newMusicVolume) * 20);
        PlayerPrefs.SetFloat(P_MusicVolume, newMusicVolume);
    }

    public void SetDefaultValues()
    {
        InitializeValues();
        LoadValues();
    }
}
