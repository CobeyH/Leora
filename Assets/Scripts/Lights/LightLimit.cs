using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public struct PointLight
{
    public PointLight(LightController C, Light2D L)
    {
        controller = C;
        light = L;
    }

    public LightController controller;

    public Light2D light;
}

public class LightLimit : MonoBehaviour
{
    public int voltageLimit;

    public float fillSpeed;

    public static bool IsOverVoltage = false;

    private Slider voltageIndicator;

    private List<PointLight> pointLights = new List<PointLight>();

    private AudioManager audioManager;

    [SerializeField]
    private ParticleSystem sparks;

    private bool wasOverVoltage;

    // Start is called before the first frame update
    void Start()
    {
        List<Light2D> lightsInScene =
            new List<Light2D>(FindObjectsOfType<Light2D>());
        voltageIndicator = gameObject.GetComponent<Slider>();

        foreach (Light2D light in lightsInScene)
        {
            if (light.lightType == Light2D.LightType.Point)
            {
                PointLight nextLight =
                    new PointLight(light.GetComponent<LightController>(),
                        light);
                pointLights.Add (nextLight);
            }
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
        foreach (PointLight pl in pointLights)
        {
            if (pl.controller.isOn)
            {
                voltageUsed += pl.light.intensity;
            }
        }
        IsOverVoltage = voltageUsed > voltageLimit;
        float indicatorTarget =
            Mathf.Clamp(voltageUsed / (float) voltageLimit, 0, 1);
        UpdateVoltageIndicator (indicatorTarget);

        if (wasOverVoltage != IsOverVoltage)
        {
            Sparks();
        }
        wasOverVoltage = IsOverVoltage;
    }

    void UpdateVoltageIndicator(float target)
    {
        // If it's close to the correct value then stop updating.
        if (Mathf.Abs(target - voltageIndicator.value) < 0.01) return;
        int direction = target > voltageIndicator.value ? 1 : -1;
        voltageIndicator.value += direction * Time.deltaTime * fillSpeed;
    }

    void Sparks()
    {
        if (IsOverVoltage)
        {
            sparks.Play();
            audioManager.Play("Sparkles");
        }
        else
        {
            sparks.Stop();
            audioManager.Pause("Sparkles");
        }
    }
}
