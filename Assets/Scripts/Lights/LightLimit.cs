using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightLimit : MonoBehaviour
{
    public int voltageLimit;

    public TMP_Text limitDisplay;

    public static bool IsOverVoltage = false;

    private List<Light2D> lightsInScene;

    private AudioManager audioManager;

    private bool voltageChange;

    // Start is called before the first frame update
    void Start()
    {
        lightsInScene = new List<Light2D>(FindObjectsOfType<Light2D>());

        // Find all the global lights in the scene
        List<Light2D> globalLights = new List<Light2D>();
        foreach (Light2D light in lightsInScene)
        {
            if (light.lightType == Light2D.LightType.Global)
            {
                globalLights.Add (light);
            }
        }

        // Delete the global light from the list
        foreach (Light2D globalLight in globalLights)
        {
            lightsInScene.Remove (globalLight);
        }

        audioManager =
    GameObject
        .FindGameObjectWithTag("AudioManager")
        .GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float voltageUsed = 0;
        foreach (Light2D light in lightsInScene)
        {
            if (light.enabled)
            {
                voltageUsed += light.intensity;
            }
        }
        limitDisplay.text =
            voltageUsed.ToString() + " / " + voltageLimit.ToString();
        if (voltageUsed > voltageLimit)
        {
            IsOverVoltage = true;
            limitDisplay.color = new Color(255, 0, 0, 255);
        }
        else
        {
            IsOverVoltage = false;
            limitDisplay.color = new Color(255, 255, 255, 255);
        }

        if (voltageChange != IsOverVoltage)
        {
            SparkSound();
        }
        voltageChange = IsOverVoltage;
    }

    void SparkSound()
    {
        if (IsOverVoltage)
        {
            audioManager.Play("Sparkles");
        } else
        {
            audioManager.Pause("Sparkles");
        }
    }
}
