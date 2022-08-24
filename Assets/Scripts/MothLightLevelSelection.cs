using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MothLightLevelSelection : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D buttonLight;
    public Button button;
    public ParticleSystemForceField mothField;
    private AudioManager audioManager;

    void Start()
    {
        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
    }

    public void MouseEnter()
    {
        if (button.interactable)
        {
            audioManager.Play("ButtonHover");
            buttonLight.enabled = true;
            mothField.enabled = true;
        }
    }

    public void MouseLeave()
    {
        buttonLight.enabled = false;
        mothField.enabled = false;
    }

    public void MouseClick()
    {
        audioManager.Play("ButtonClick");
    }
}
