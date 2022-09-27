using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Light2D myLight;

    public Light2D lightBeams;

    public bool startOn = false;

    [HideInInspector]
    public bool isOn;

    [SerializeField]
    GameObject ProjectilePrefab;

    private AudioManager audioManager;

    void Start()
    {
        myLight.enabled = startOn;
        lightBeams.enabled = startOn;
        myLight.intensity = Mathf.Round(myLight.intensity);
        isOn = startOn;

        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
    }

    void Update()
    {
        if (LightLimit.IsOverVoltage)
        {
            myLight.enabled = false;
            lightBeams.enabled = false;
        }
        else
        {
            myLight.enabled = isOn;
            lightBeams.enabled = isOn;
        }
    }

    void OnMouseDown()
    {
        toggleLight();
        audioManager.Play("LightOn");
    }

    void toggleLight()
    {
        audioManager.Play("LightOff");
        isOn = !isOn;
        myLight.enabled = isOn;
        lightBeams.enabled = isOn;
    }

    IEnumerator ProjectileTravel()
    {
        // TODO:
        // Create projectile
        // Move projectile towards target
        // Cause effect when close enough to target
        yield return null;
    }
}
