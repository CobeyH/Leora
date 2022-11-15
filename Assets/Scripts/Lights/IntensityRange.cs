using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntensityRange : MonoBehaviour
{
    public Light2D lightBeams;

    private Light2D controlledLight;

    private float rangeMultiplier = 0;

    private BeamColour beamColorController;

    private BeamColour lightColorController;

    private float prevIntensity = 0;

    void Start()
    {
        controlledLight = GetComponent<Light2D>();
        GameObject lightBase = gameObject.transform.parent.gameObject;
        rangeMultiplier =
            lightBase.GetComponent<LightBuilder>().lightData.rangeMultiplier;
        beamColorController = lightBeams.GetComponent<BeamColour>();
        lightColorController = controlledLight.GetComponent<BeamColour>();
    }

    void Update()
    {
        float intensity = controlledLight.intensity;
        if (
            controlledLight.enabled &&
            intensity > 0 &&
            intensity != prevIntensity
        )
        {
            UpdateRanges();
        }
    }

    void UpdateRanges()
    {
        float intensity = controlledLight.intensity;
        controlledLight.pointLightOuterRadius = intensity * rangeMultiplier * 7;
        lightBeams.pointLightOuterRadius =
            controlledLight.pointLightOuterRadius;
        lightBeams.pointLightOuterAngle = controlledLight.pointLightOuterAngle;
        beamColorController.UpdateColour (intensity);
        lightColorController.UpdateColour (intensity);
        lightBeams.intensity = controlledLight.intensity;
        prevIntensity = intensity;
    }
}
