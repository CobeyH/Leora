using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuxRings : MonoBehaviour
{
    [SerializeField]
    GameObject ringPrefab;
    [SerializeField]
    Light2D controlledLight;

    SpriteRenderer[] luxRings;

    void Awake()
    {
        int intensity = (int)controlledLight.intensity;
        luxRings = new SpriteRenderer[intensity];
        for (int i = 0; i < intensity; i++)
        {
            GameObject newRing = Instantiate(ringPrefab, transform);
            float scale = (i * 0.3f + 0.8f) * 0.1f;
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
            if (controlledLight.enabled || controlledLight.intensity <= availableLux)
            {
                ringColor = Color.green;
            }
            else
            {
                ringColor = i > availableLux ? Color.red : Color.yellow;
            }
            ring.color = ringColor;
            i++;
        }
    }
}
