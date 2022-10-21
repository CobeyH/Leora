using UnityEngine;

public class LightBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject lightEmitter;
    public LightData lightData;
    private LightData _oldLightData;

    void OnValidate()
    {
        CheckActivationType();
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

    void SetLightCollider(bool isOn)
    {
        CircleCollider2D col = lightEmitter.GetComponent<CircleCollider2D>();
        Debug.Log(isOn);
        col.enabled = isOn;
    }
}
