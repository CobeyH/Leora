using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SparkController : MonoBehaviour
{
    public ParticleSystem sparks;

    public Light2D controlledLight;

    private LightController controller;

    void Awake()
    {
        controller = controlledLight.GetComponent<LightController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldSpark = LightLimit.IsOverVoltage && controller.isOn;
        sparks.gameObject.SetActive (shouldSpark);
    }
}
