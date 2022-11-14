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
        int intIntensity = (int)intensity;
        switch (intIntensity)
        {
            case 1:
                beams.color = Color.red;
                break;
            case 2:
                beams.color = Color.green;
                break;
            case 3:
                beams.color = Color.blue;
                break;
            default:
                Debug.LogError("Intensity of " + intensity + " is outside the accepted range.");
                break;
        }
    }
}
