using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAttraction : MonoBehaviour
{
    public float mothSpeed = 2f;

    ParticleSystemForceField field;

    Rigidbody2D rigidBody;

    List<Light2D> lightsInScene;

    Vector2 centerOfLightMass = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        field = GetComponent<ParticleSystemForceField>();
        rigidBody = field.GetComponent<Rigidbody2D>();
        lightsInScene = new List<Light2D>();

        // Find all the global lights in the scene
        List<Light2D> allLightsInScene = new List<Light2D>(FindObjectsOfType<Light2D>());
        foreach (Light2D light in allLightsInScene)
        {
            if (light.lightType == Light2D.LightType.Point)
            {
                lightsInScene.Add(light);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsInScene.Count <= 0)
        {
            return;
        }

        // Calculate the net force applied to the moths by all lights
        int layer_mask = LayerMask.GetMask("Terrain");
        centerOfLightMass = Vector2.zero;
        float totalIntensity = 0f;
        List<Light2D> contributingLights = lightsInScene.FindAll(l => ShouldLightContribute(l, layer_mask));
        int highestIntensity = FindHighestIntensity(contributingLights);

        foreach (Light2D light in contributingLights)
        {
            if (light.intensity < highestIntensity) continue;
            centerOfLightMass += AddAttractionFromLight(light);
            totalIntensity += light.intensity;
        }
        if (totalIntensity == 0)
        {
            centerOfLightMass = rigidBody.transform.position;
        }
        else
        {
            centerOfLightMass /= totalIntensity;
        }

    }

    private int FindHighestIntensity(List<Light2D> lights)
    {
        int highestIntensity = 0;
        foreach (Light2D light in lights)
        {
            if (light.intensity > highestIntensity)
            {
                highestIntensity = (int)light.intensity;
            }
        }
        return highestIntensity;
    }

    private void FixedUpdate()
    {
        Vector2 travelDir = centerOfLightMass - (Vector2)rigidBody.transform.position;
        rigidBody.velocity = Vector2.ClampMagnitude(travelDir, 1) * mothSpeed;
    }

    private bool ShouldLightContribute(Light2D light, int layer_mask)
    {
        // Check if there is a clear line between the field and the light.
        // Also ignore global lights
        if (
            light.enabled &&
            !Physics2D
                .Linecast(transform.position,
                light.transform.position,
                layer_mask)
        )
        {
            Vector2 vecToLight = light.transform.position - transform.position;

            if (
                (
                light.lightType == Light2D.LightType.Point &&
                !MothsInBeam(light, vecToLight)
                ) ||
                !LightInRange(light, vecToLight)
            ) return false;

            return true;
        }
        return false;
    }

    Vector2 AddAttractionFromLight(Light2D light)
    {
        return light.transform.position * light.intensity;
    }

    private bool LightInRange(Light2D light, Vector2 vecToLight)
    {
        float distanceToLight = vecToLight.magnitude;
        if (
            distanceToLight > light.pointLightOuterRadius ||
            distanceToLight < light.pointLightInnerRadius
        )
        {
            return false;
        }
        return true;
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
        Vector2 lightForward =
            new Vector2(Mathf.Cos(lightRotation + Mathf.PI / 2f),
                Mathf.Sin(lightRotation + Mathf.PI / 2f));
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
