using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsMenuUI;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;
    public TMP_Dropdown graphicQualityDropdown;

    void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMusicVolume(float sliderVolume)
    {
        SetVolume(sliderVolume, musicMixer);
    }

    public void SetSFXVolume(float sliderVolume)
    {
        SetVolume(sliderVolume, sfxMixer);
    }

    void SetVolume(float sliderVolume, AudioMixer mixer)
    {
        float mixerVolume = Mathf.Log10(sliderVolume) * 20;
        mixer.SetFloat("volume", mixerVolume);
        PlayerPrefs.SetFloat("volume", sliderVolume);

    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }
    public void ShowSettings()
    {
        LoadSettings();
        SettingsMenuUI.SetActive(true);

    }
    public void HideSettings()
    {
        SettingsMenuUI.SetActive(false);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", Convert.ToInt32(isFullscreen));
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionIndex);
    }
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            volumeSlider.value = 1;
        }

        if (PlayerPrefs.HasKey("quality"))
        {
            graphicQualityDropdown.value = PlayerPrefs.GetInt("quality");
        }
        else
        {
            graphicQualityDropdown.value = 5;
        }
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            fullScreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        }
        else
        {
            fullScreenToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        }
        else
        {
            resolutionDropdown.value = resolutions.Length - 1;
        }
    }
}
