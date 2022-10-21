using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject lightEmitter;
    public LightData lightData = new LightData();
    private LightData _oldLightData = new LightData();
    private Light2D light2D;


    private void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

    void _OnValidate()
    {
        UnityEditor.EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        CheckActivationType();
        CheckAreaOfEffect();
        lightEmitter.GetComponent<Light2D>().enabled = lightData.startsOn;
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
            switch (lightData.area)
            {
                case AreaOfEffect.Directional:
                    leaves.SetActive(true);
                    lightEmitter.GetComponent<Light2D>().pointLightOuterAngle = 90;
                    break;
                case AreaOfEffect.Point:
                    leaves.SetActive(false);
                    lightEmitter.GetComponent<Light2D>().pointLightOuterAngle = 360;
                    break;
                default:
                    Debug.LogError("Unsupported area of effect");
                    break;
            }
            _oldLightData.area = lightData.area;
        }
    }

    void SetLightCollider(bool isOn)
    {
        CircleCollider2D col = lightEmitter.GetComponent<CircleCollider2D>();
        col.enabled = isOn;
    }
}
