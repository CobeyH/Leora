using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        var foundLights = FindObjectsOfType<Light2D>();
        if (foundLights.Length <= 0)
        {
            return;
        }

        Vector2 netForce = Vector2.zero;
        int layer_mask = LayerMask.GetMask("Default");
        foreach (Light2D light in foundLights)
        {
            // Check if there is a clear line between the field and the light.
            // Also ignore global lights
            if (
                light.enabled &&
                light.lightType != Light2D.LightType.Global &&
                !Physics2D.Linecast(transform.position, light.transform.position, layer_mask)
            )
            {
                Vector2 vecToLight =
                    light.transform.position - transform.position;

                if (light.lightType == Light2D.LightType.Point && !MothsInBeam(light, vecToLight)) continue;
                // Make moths attracted in the direction of the light.
                // Moths are more attracted to stronger lights.
                // Moths are less attracted to far away lights.
                netForce +=
                    vecToLight.normalized *
                    (
                    light.intensity / (Mathf.Pow(vecToLight.magnitude, 2) + 1)
                    );
            }
        }
        netForce = netForce.normalized * Time.deltaTime * mothSpeed;
        transform.position += new Vector3(netForce.x, netForce.y, 0);
    }

    private bool MothsInBeam(Light2D light, Vector2 vecToLight)
    {
        // Make sure the point light area is pointing in the direction of the moths.
        float outerAngle = light.pointLightOuterAngle;
        if (outerAngle >= 360)
        {
            // Moths will always be in the beam of a 360 degree light;
            return true;
        }
        float lightRotation = getZRotation(light);
        Vector2 lightForward = new Vector2(Mathf.Cos(lightRotation + Mathf.PI / 2f), Mathf.Sin(lightRotation + Mathf.PI / 2f));
        float angleToLight = Vector2.Angle(vecToLight, -lightForward);
        if (angleToLight > outerAngle / 2)
        {
            return false;
        }
        return true;
    }

    private float getZRotation(Light2D light)
    {
        float zRotation = light.transform.eulerAngles.z;
        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }
        return zRotation * Mathf.PI / 180f;
    }
}
