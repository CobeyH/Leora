using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsMenuUI;
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void ShowSettings()
    {
        SettingsMenuUI.SetActive(true);
    }
    public void HideSettings()
    {
        SettingsMenuUI.SetActive(false);
    }
}
