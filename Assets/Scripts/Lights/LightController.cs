using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Light2D myLight;
    public Light2D lightBeams;

    [HideInInspector]
    public LightData lightData;

    [SerializeField]
    GameObject ProjectilePrefab;

    [SerializeField]
    private GameObject luxRings;
    private LuxRings _ringController;

    private Camera UICamera;
    private Camera mainCam;
    private RadialLightLimit lightLimit;
    private AudioManager audioManager;
    private int _requestedIntensity = 0;
    private bool _processing = false;

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
        myLight.intensity = lightData.startsOn ? lightData.maxIntensity : 0;
        _requestedIntensity = (int)myLight.intensity;
    }

    void Start()
    {
        myLight.enabled = lightData.startsOn;
        lightBeams.enabled = lightData.startsOn;
        SetLightColour();
    }

    void SetLightColour()
    {
        if (lightData.isRepulsive)
        {
            myLight.color = new Color(0.4f, 0f, 0.4f, 1.0f);
        }
    }

    void Update()
    {
        if (_processing) return;
        if (myLight.intensity < _requestedIntensity)
        {
            _processing = true;
            StartCoroutine(BrightenLight());
        }
        else if (myLight.intensity > _requestedIntensity)
        {
            _processing = true;
            StartCoroutine(DimLight());
        }
    }

    void OnMouseDown()
    {
        _requestedIntensity = Mathf.Min(_requestedIntensity + 1, lightData.maxIntensity);
        audioManager.Play("LightOn");
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _requestedIntensity = Mathf.Max(_requestedIntensity - 1, 0);
            audioManager.Play("LightOff");
        }
    }

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
        if (!lightLimit.LuxAvailable(1))
        {
            // If there isn't enough lux, the light cannot increase in brightness.
            _processing = false;
            _requestedIntensity -= 1;
            yield break;
        }
        Vector3 startPos;
        Vector3 endPos;
        CalculateProjectileTarget(out startPos, out endPos);
        yield return FireProjectile(startPos, endPos);
        myLight.intensity += 1;
        lightLimit.ChangeAvailableLux(-1);
        if (myLight.intensity == 1)
        {
            SwitchLightState();
        }
        _processing = false;
    }

    IEnumerator DimLight()
    {
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
        _processing = false;
    }

    void SwitchLightState()
    {
        myLight.enabled = !myLight.enabled;
        lightBeams.enabled = myLight.enabled;
    }
}

