using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBuilder : MonoBehaviour
{
    public GameObject lightEmitter;
    public LightData lightData = new LightData();
    private LightData _oldLightData = new LightData();
    [HideInInspector]
    public Light2D light2D;

    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        CheckActivationType();
        CheckAreaOfEffect();
        CheckStartOn();
    }

    void CheckActivationType()
    {
        if (lightData.activation != _oldLightData.activation)
        {
            switch (lightData.activation)
            {
                case ActivationType.AlwaysOn:
                    SetLightCollider(false);
                    break;
                case ActivationType.PlayerControlled:
                    SetLightCollider(true);
                    break;
                default:
                    Debug.LogError("Unsupported light type selected");
                    break;
            }
            _oldLightData.activation = lightData.activation;
        }
    }

    void CheckAreaOfEffect()
    {
        if (lightData.area != _oldLightData.area)
        {
            GameObject leaves = lightEmitter.transform.Find("Leaves").gameObject;
            Light2D light2D = lightEmitter.GetComponent<Light2D>();
            switch (lightData.area)
            {
                case AreaOfEffect.Directional:
                    leaves.SetActive(true);
                    light2D.pointLightOuterAngle = 90;
                    light2D.pointLightInnerAngle = 90;
                    break;
                case AreaOfEffect.Point:
                    leaves.SetActive(false);
                    light2D.pointLightInnerAngle = 360;
                    light2D.pointLightInnerAngle = 360;
                    break;
                default:
                    Debug.LogError("Unsupported area of effect");
                    break;
            }
            _oldLightData.area = lightData.area;
        }
    }

    void CheckStartOn()
    {
        lightEmitter.GetComponent<Light2D>().enabled = lightData.startsOn;
    }

    void SetLightCollider(bool isOn)
    {
        CircleCollider2D col = lightEmitter.GetComponent<CircleCollider2D>();
        col.enabled = isOn;
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 1; i <= lightData.maxIntensity; i++)
        {
            Gizmos.DrawWireSphere(lightEmitter.transform.position, i * lightData.rangeMultiplier);
        }
    }
}
