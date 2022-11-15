using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BeamColour : MonoBehaviour
{
    Light2D beams;

    void Awake()
    {
        beams = GetComponent<Light2D>();
    }

    public void UpdateColour(float intensity)
    {
        int intIntensity = (int) intensity;
        switch (intIntensity)
        {
            case 1:
                beams.color = new Color(251, 225, 70);
                break;
            case 2:
                beams.color = new Color(221, 252, 225);
                break;
            case 3:
                beams.color = new Color(191, 255, 253);
                break;
            default:
                Debug
                    .LogError("Intensity of " +
                    intensity +
                    " is outside the accepted range.");
                break;
        }
    }
}
