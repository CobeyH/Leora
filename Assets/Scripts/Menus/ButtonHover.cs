using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour
{
    [SerializeField]
    TMP_Text buttonText;

    [SerializeField]
    Color highlightColour;

    [SerializeField]
    Image[] decals;
    private Color defaultColour;
    private AudioManager audioManager;

    void Start()
    {
        defaultColour = buttonText.color;
        SetDecals(false);
        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
    }

    public void ButtonHovered()
    {
        buttonText.color = highlightColour;
        SetDecals(true);
        audioManager.Play("ButtonClick");
    }

    public void ButtonUnHovered()
    {
        buttonText.color = defaultColour;
        SetDecals(false);
    }

    public void SetDecals(bool isOn)
    {
        foreach (Image decal in decals)
        {
            decal.enabled = isOn;
        }
    }

}
