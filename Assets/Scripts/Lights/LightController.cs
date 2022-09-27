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
        Vector2 endPos = transform.position;
        GameObject luxProjectile = Instantiate(ProjectilePrefab);
        luxProjectile.transform.position = lightLimit.transform.position;
        Vector2 startPos = Camera.main.ScreenToWorldPoint(luxProjectile.transform.position);
        Vector3 dir = endPos - new Vector2(startPos.x, -startPos.y - 0.5f);
        float travelDistance = dir.magnitude;
        while (true)
        {
            Vector3 vectorOffset = dir * Time.deltaTime;
            luxProjectile.transform.position += vectorOffset;
            travelDistance -= vectorOffset.magnitude;
            if (travelDistance < 0.001f)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        Destroy(luxProjectile);

        yield return null;
    }
}
