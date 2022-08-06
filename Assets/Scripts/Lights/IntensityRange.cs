using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntensityRange : MonoBehaviour
{
    public Light2D controlledLight;
    private float rangeMultiplier = 0;

    void Start()
    {
        rangeMultiplier = controlledLight.pointLightOuterRadius / controlledLight.intensity;
    }
    void Update()
    {
        controlledLight.pointLightOuterRadius = controlledLight.intensity * rangeMultiplier;
    }
}
