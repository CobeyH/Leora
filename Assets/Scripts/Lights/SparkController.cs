using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour
{
    public ParticleSystem sparks;
    public UnityEngine.Rendering.Universal.Light2D light;

    // Update is called once per frame
    void Update()
    {
        bool shouldSpark = LightLimit.IsOverVoltage && light.enabled;
        sparks.gameObject.SetActive(shouldSpark);
    }
}
