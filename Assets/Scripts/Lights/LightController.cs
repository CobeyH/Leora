using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Light2D myLight;
    public Light2D lightBeams;

    private LightData lightData;

    [SerializeField]
    GameObject ProjectilePrefab;

    [SerializeField]
    private GameObject luxRings;
    private LuxRings _ringController;

    private Camera UICamera;
    private Camera mainCam;
    private RadialLightLimit lightLimit;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
        lightLimit = GameObject.Find("RadialLightLimit").GetComponent<RadialLightLimit>();
        mainCam = Camera.main;
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        _ringController = luxRings.GetComponent<LuxRings>();
        lightData = transform.parent.gameObject.GetComponent<LightBuilder>().lightData;
        myLight.intensity = 0;
    }

    void Start()
    {
        myLight.enabled = lightData.startsOn;
        lightBeams.enabled = lightData.startsOn;
    }

    void OnMouseDown()
    {
        StartCoroutine(BrightenLight());
        audioManager.Play("LightOn");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(DimLight());
            audioManager.Play("LightOff");
        }
    }


    // public void ToggleLight()
    // {
    //     audioManager.Play("LightOff");
    //     Vector3 uiElementPosition = UICamera.WorldToScreenPoint(lightLimit.transform.position);
    //     Vector3 startPos = mainCam.ScreenToWorldPoint(uiElementPosition);
    //     Vector3 endPos = transform.position;

    //     if (!myLight.enabled)
    //     {
    //         StartCoroutine(EnableLight(startPos, endPos));
    //     }
    //     else
    //     {
    //         if (lightData.returnsLux)
    //         {
    //             StartCoroutine(EnableLight(endPos, startPos));
    //         }
    //         else
    //         {
    //             SwitchLightState();
    //         }
    //     }
    // }

    IEnumerator FireProjectile(Vector3 startPos, Vector3 endPos)
    {
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
    }

    void CalculateProjectileTarget(out Vector3 startPos, out Vector3 endPos)
    {
        Vector3 uiElementPosition = UICamera.WorldToScreenPoint(lightLimit.transform.position);
        startPos = mainCam.ScreenToWorldPoint(uiElementPosition);
        endPos = transform.position;
    }

    public IEnumerator BrightenLight()
    {
        if (!lightLimit.LuxAvailable(1) || myLight.intensity == lightData.maxIntensity)
        {
            // If there isn't enough lux, the light cannot increase in brightness.
            yield break;
        }
        if (myLight.intensity == 0)
        {
            SwitchLightState();
        }
        Vector3 startPos;
        Vector3 endPos;
        CalculateProjectileTarget(out startPos, out endPos);
        yield return FireProjectile(startPos, endPos);
        myLight.intensity += 1;
        lightLimit.ChangeAvailableLux(-1);
    }

    IEnumerator DimLight()
    {
        if (myLight.intensity == 0) yield break;
        myLight.intensity -= 1;
        if (myLight.intensity == 0)
        {
            SwitchLightState();
        }
        if (lightData.returnsLux)
        {
            Vector3 startPos;
            Vector3 endPos;
            CalculateProjectileTarget(out startPos, out endPos);
            yield return FireProjectile(endPos, startPos);
            lightLimit.ChangeAvailableLux(1);
        }
        else
        {
            // The ChangeAvailableLux function normally handles this. We have to explicitly do
            // it here because that function is skipped.
            lightLimit.UpdateAllRingColours();
        }
    }

    void SwitchLightState()
    {
        myLight.enabled = !myLight.enabled;
        lightBeams.enabled = myLight.enabled;
    }
}

