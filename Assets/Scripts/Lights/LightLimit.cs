using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightLimit : MonoBehaviour
{
    public int voltageLimit;
    public float fillSpeed;

    public static bool IsOverVoltage = false;

    private Slider voltageIndicator;
    private List<Light2D> lightsInScene;

    private AudioManager audioManager;
    [SerializeField]
    private ParticleSystem sparks;

    private bool wasOverVoltage;

    // Start is called before the first frame update
    void Start()
    {
        lightsInScene = new List<Light2D>(FindObjectsOfType<Light2D>());
        voltageIndicator = gameObject.GetComponent<Slider>();

        // Find all the global lights in the scene
        List<Light2D> globalLights = new List<Light2D>();
        foreach (Light2D light in lightsInScene)
        {
            if (light.lightType == Light2D.LightType.Global)
            {
                globalLights.Add(light);
            }
        }

        // Delete the global light from the list
        foreach (Light2D globalLight in globalLights)
        {
            lightsInScene.Remove(globalLight);
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
        IsOverVoltage = voltageUsed > voltageLimit;
        float indicatorTarget = Mathf.Clamp(voltageUsed / (float)voltageLimit, 0, 1);
        UpdateVoltageIndicator(indicatorTarget);

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
