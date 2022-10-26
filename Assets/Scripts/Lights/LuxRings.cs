using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuxRings : MonoBehaviour
{
    [SerializeField]
    GameObject ringPrefab;
    [SerializeField]
    Light2D controlledLight;
    int maxIntensity;

    SpriteRenderer[] luxRings;
    private float innerRingScale = 1.3f, ringSpacing = 0.3f, globalRingScale = 0.1f;

    void Start()
    {
        maxIntensity = transform.parent.parent.gameObject.GetComponent<LightBuilder>().lightData.maxIntensity;
        luxRings = new SpriteRenderer[maxIntensity];
        for (int i = 0; i < maxIntensity; i++)
        {
            GameObject newRing = Instantiate(ringPrefab, transform);
            float scale = (i * ringSpacing + innerRingScale) * globalRingScale;
            newRing.transform.localScale = new Vector3(scale, scale, 1);
            luxRings[i] = newRing.GetComponent<SpriteRenderer>();
        }
    }

    public void UpdateRingsColour(int availableLux)
    {
        int i = 1;
        Color ringColor;
        foreach (SpriteRenderer ring in luxRings)
        {
            // In this case, the ring is already powered
            if (i <= controlledLight.intensity)
            {
                ringColor = Color.magenta;
            }
            else if (i <= controlledLight.intensity + availableLux)
            {
                ringColor = Color.green;
            }
            else { ringColor = Color.red; }
            ring.color = ringColor;
            i++;
        }
    }
}
