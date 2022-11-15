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
                beams.color = new Color(0.9f, 0.8f, 0.38f);
                break;
            case 2:
                beams.color = new Color(0.8f, 0.36f, 0f);
                break;
            case 3:
                beams.color = new Color(0.25f, 0.3f, 0.4f);
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
