using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class ZoneToggleLight : MonoBehaviour
{
    LightController lightController;
    public IntEventChannelSO zoneChannel;
    public List<int> subscribedZones = new List<int>();

    void Awake()
    {
        Light2D lightToToggle = gameObject.GetComponent<Light2D>();
        lightController = lightToToggle.GetComponent<LightController>();
    }

    void OnEnable()
    {
        zoneChannel.OnEventRaised += HandleLightToggle;
    }

    void HandleLightToggle(int zoneId)
    {
        if (subscribedZones.Contains(zoneId))
        {
            lightController.ToggleLight();
        }
    }
}
