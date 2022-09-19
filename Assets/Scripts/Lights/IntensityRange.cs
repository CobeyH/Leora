using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntensityRange : MonoBehaviour
{
    public Light2D controlledLight;

    public Light2D lightBeams;

    public float beamIntensity = 1;

    private float rangeMultiplier = 0;

    void Start()
    {
        rangeMultiplier =
            controlledLight.pointLightOuterRadius / controlledLight.intensity;
    }

    void Update()
    {
        if (LightLimit.IsOverVoltage)
        {
            controlledLight.pointLightOuterRadius = 0;
            lightBeams.transform.localScale = Vector3.zero;
        }
        else
        {
            float intensity = controlledLight.intensity;
            controlledLight.pointLightOuterRadius = intensity * rangeMultiplier;
            lightBeams.transform.localScale =
                new Vector3(intensity * beamIntensity,
                    intensity * beamIntensity,
                    1);
        }
    }
}
