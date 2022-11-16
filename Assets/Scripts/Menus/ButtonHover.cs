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
        ToggleDecals();
        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
    }

    public void ButtonHovered()
    {
        buttonText.color = highlightColour;
        ToggleDecals();
        audioManager.Play("ButtonClick");
    }

    public void ButtonUnHovered()
    {
        buttonText.color = defaultColour;
        ToggleDecals();
    }

    public void ToggleDecals()
    {
        foreach (Image decal in decals)
        {
            decal.enabled = !decal.enabled;
        }
    }

}
