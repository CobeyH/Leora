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
    Camera UICamera;

    private RadialLightLimit lightLimit;

    private AudioManager audioManager;
    private bool routineRunning = false;

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
        lightLimit = GameObject.Find("RadialLightLimit").GetComponent<RadialLightLimit>();
    }

    void OnMouseDown()
    {
        toggleLight();
        audioManager.Play("LightOn");
    }

    void toggleLight()
    {
        // Light cannot be interacted with if the routine is running.
        if (routineRunning)
        {
            return;
        }

        audioManager.Play("LightOff");
        Vector3 startPos = new Vector3(-10, 6, 0);
        Vector3 endPos = transform.position;
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
        // If the light is being turned on, it must request light immediately.
        if (!isOn)
        {
            if (!lightLimit.LuxAvailable((int)myLight.intensity))
            {
                // If there isn't enough lux, the light cannot turn on.
                routineRunning = false;
                yield break;
            }
            lightLimit.ChangeAvailableLux((int)-myLight.intensity);
        }
        GameObject luxProjectile = Instantiate(ProjectilePrefab);
        luxProjectile.transform.position = startPos;
        // Set start point for projectile in canvas coordinates
        Vector3 dir = endPos - startPos;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            luxProjectile.transform.position += dir / 100;
        }
        Destroy(luxProjectile);
        if (isOn)
        {
            lightLimit.ChangeAvailableLux((int)myLight.intensity);
        }
        isOn = !isOn;
        myLight.enabled = isOn;
        lightBeams.enabled = isOn;
        routineRunning = false;
        yield return null;
    }
}

