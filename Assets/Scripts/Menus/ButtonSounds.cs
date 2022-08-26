using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {

        audioManager =
     GameObject
        .FindGameObjectWithTag("AudioManager")
        .GetComponent<AudioManager>();
    }

    public void PlayHoverSound()
    {
        audioManager.Play("ButtonHover");
    }

}
