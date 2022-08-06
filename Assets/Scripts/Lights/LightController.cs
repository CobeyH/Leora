using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D myLight;
    public bool startOn = false;


    void Start()
    {
        myLight.enabled = startOn;
    }
    void OnMouseDown()
    {
        toggleLight();
    }

    void toggleLight()
    {
        myLight.enabled = !myLight.enabled;
    }
}
