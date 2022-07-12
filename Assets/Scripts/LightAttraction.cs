using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const float minDistance = 0.0001f;
}

public class LightAttraction : MonoBehaviour
{
    public float mothSpeed = 2f;

    private ParticleSystemForceField field;

    // Start is called before the first frame update
    void Start()
    {
        field = GetComponent<ParticleSystemForceField>();
    }

    // Update is called once per frame
    void Update()
    {
        var foundLights = FindObjectsOfType<UnityEngine.Rendering.Universal.Light2D>();
        if (foundLights.Length <= 0)
        {
            return;
        }

        Vector3 netForce = Vector3.zero;
        foreach (UnityEngine.Rendering.Universal.Light2D light in foundLights)
        {
            // Check if there is a clear line between the field and the light.
            // Also ignore global lights
            if (
                light.enabled &&
                light.lightType != UnityEngine.Rendering.Universal.Light2D.LightType.Global &&
                !Physics.Linecast(transform.position, light.transform.position)
            )
            {
                Vector3 vecToLight =
                    light.transform.position - transform.position;

                // Don't move towards light that the moths are on top off to avoid occilations.
                if (vecToLight.magnitude <= Constants.minDistance)
                {
                    continue;
                }

                // Make moths attracted in the direction of the light.
                // Moths are more attracted to stronger lights.
                netForce +=
                    Vector3.Normalize(vecToLight) *
                    (
                    light.intensity / (Mathf.Pow(vecToLight.magnitude, 2) + 1)
                    );
                // Debug.Log(Mathf.Pow(vecToLight.magnitude, 2));
            }
        }
        if (netForce.magnitude > Constants.minDistance)
        {
            transform.position +=
                Vector3.Normalize(netForce) * Time.deltaTime * mothSpeed;
        }
    }
}
