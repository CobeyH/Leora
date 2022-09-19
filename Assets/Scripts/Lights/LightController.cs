using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D myLight;

    public Light2D lightBeams;

    public bool startOn = false;

    private AudioManager audioManager;

    void Start()
    {
        myLight.enabled = startOn;
        lightBeams.enabled = startOn;
        myLight.intensity = Mathf.Round(myLight.intensity);

        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
    }

    void OnMouseDown()
    {
        toggleLight();
        audioManager.Play("LightOn");
    }

    void toggleLight()
    {
        myLight.enabled = !myLight.enabled;
        lightBeams.enabled = myLight.enabled;
        audioManager.Play("LightOff");
    }
}
