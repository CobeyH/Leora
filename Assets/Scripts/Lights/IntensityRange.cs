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
        GameObject lightBase = gameObject.transform.parent.gameObject;
        rangeMultiplier = lightBase.GetComponent<LightBuilder>().lightData.rangeMultiplier;
    }

    void Update()
    {
        if (controlledLight.enabled && controlledLight.intensity > 0)
        {
            float intensity = controlledLight.intensity;
            controlledLight.pointLightOuterRadius = intensity * rangeMultiplier * 7;
            lightBeams.pointLightOuterRadius = controlledLight.pointLightOuterRadius * 1.25f;
            lightBeams.pointLightOuterAngle = controlledLight.pointLightOuterAngle;
        }
    }
}
