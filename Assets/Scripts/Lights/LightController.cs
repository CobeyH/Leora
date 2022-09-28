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
        StartCoroutine(EnableLight());
    }

    IEnumerator EnableLight()
    {
        Vector3 startPos = new Vector3(-10, 6, 0);
        Vector3 endPos = transform.position;
        GameObject luxProjectile = Instantiate(ProjectilePrefab);
        luxProjectile.transform.position = startPos;
        // Set start point for projectile in canvas coordinates
        Vector3 dir = endPos - startPos;

        for (int i = 0; i < 250; i++)
        {
            // Translate back to screen coordinates
            luxProjectile.transform.position += dir / 250;
            yield return new WaitForFixedUpdate();
        }
        Destroy(luxProjectile);

        yield return null;
    }
}

