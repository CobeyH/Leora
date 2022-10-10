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

    [SerializeField]
    private GameObject luxRings;

    private Camera UICamera;
    private Camera mainCam;
    private RadialLightLimit lightLimit;
    private AudioManager audioManager;
    private bool routineRunning = false;

    void Awake()
    {
        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
        lightLimit = GameObject.Find("RadialLightLimit").GetComponent<RadialLightLimit>();
        mainCam = Camera.main;
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void Start()
    {
        myLight.enabled = startOn;
        lightBeams.enabled = startOn;
        myLight.intensity = Mathf.Round(myLight.intensity);
        isOn = startOn;

    }

    void OnMouseDown()
    {
        ToggleLight();
        audioManager.Play("LightOn");
    }

    public void ToggleLight()
    {
        // Light cannot be interacted with if the routine is running.
        if (routineRunning)
        {
            return;
        }

        audioManager.Play("LightOff");
        Vector3 uiElementPosition = UICamera.WorldToScreenPoint(lightLimit.transform.position);
        Vector3 startPos = mainCam.ScreenToWorldPoint(uiElementPosition);
        Vector3 endPos = transform.position;
        // Start a co-routine that will turn on/off the light. This involves the light limit to send a light projectile.
        if (!lightLimit)
        {
            SwitchLightState();
            return;
        }
        if (!isOn)
        {
            StartCoroutine(EnableLight(startPos, endPos));
        }
        else
        {
            StartCoroutine(EnableLight(endPos, startPos));
        }
    }

    IEnumerator EnableLight(Vector3 startPos, Vector3 endPos)
    {
        routineRunning = true;
        bool lightWasOn = isOn;
        if (lightWasOn)
        {
            HandleOnLight();
        }
        // If the light is being turned on, it must request light immediately.
        if (!lightWasOn)
        {
            if (!HandleOffLight())
                yield break;
        }
        GameObject luxProjectile = Instantiate(ProjectilePrefab);
        luxProjectile.transform.position = startPos;
        // Set start point for projectile in canvas coordinates
        Vector3 dir = endPos - startPos;

        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForFixedUpdate();
            luxProjectile.transform.position += dir / 15;
        }
        Destroy(luxProjectile);
        if (!lightWasOn)
        {
            SwitchLightState();
        }
        // On lights must be handled at the end 
        routineRunning = false;
        yield return null;
    }

    bool HandleOffLight()
    {
        if (!lightLimit.LuxAvailable((int)myLight.intensity))
        {
            // If there isn't enough lux, the light cannot turn on.
            routineRunning = false;
            return false;
        }
        lightLimit.ChangeAvailableLux((int)-myLight.intensity);
        return true;
    }

    void HandleOnLight()
    {
        lightLimit.ChangeAvailableLux((int)myLight.intensity);
        SwitchLightState();
    }

    void SwitchLightState()
    {
        isOn = !isOn;
        myLight.enabled = isOn;
        lightBeams.enabled = isOn;
        luxRings.SetActive(!isOn);
    }
}

