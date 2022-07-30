using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MothLightLevelSelection : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light;
    public Button button;
    
    public void MouseEnter()
    {
        if (button.interactable)
        {
            light.enabled = true;
        }
    }

    public void MouseLeave()
    {
        light.enabled = false;
    }
}
