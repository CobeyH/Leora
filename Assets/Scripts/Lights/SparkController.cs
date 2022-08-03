using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour
{
    public ParticleSystem sparks;

    // Update is called once per frame
    void Update()
    {
        sparks.gameObject.SetActive(LightLimit.IsOverVoltage);
    }
}
