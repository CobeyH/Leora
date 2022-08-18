using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SparkController : MonoBehaviour
{
    public ParticleSystem sparks;

    public Light2D controlledLight;

    // Update is called once per frame
    void Update()
    {
        bool shouldSpark = LightLimit.IsOverVoltage && controlledLight.enabled;
        sparks.gameObject.SetActive (shouldSpark);
    }
}
