using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D myLight;


    void OnMouseDown()
    {
        toggleLight();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            toggleLight();
        }
    }

    void toggleLight()
    {
        myLight.enabled = !myLight.enabled;
    }
}
