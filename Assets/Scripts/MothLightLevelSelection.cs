using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MothLightLevelSelection : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D buttonLight;
    public Button button;
    public ParticleSystemForceField mothField;

    public void MouseEnter()
    {
        if (button.interactable)
        {
            buttonLight.enabled = true;
            mothField.enabled = true;
        }
    }

    public void MouseLeave()
    {
        buttonLight.enabled = false;
        mothField.enabled = false;
    }
}
