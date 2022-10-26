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
            controlledLight.pointLightOuterRadius = intensity * rangeMultiplier;
            lightBeams.transform.localScale =
                new Vector3(intensity * beamIntensity * 0.5f,
                    intensity * beamIntensity * 0.5f,
                    1);
        }
    }
}
