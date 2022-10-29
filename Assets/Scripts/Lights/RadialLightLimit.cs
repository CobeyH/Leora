using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadialLightLimit : MonoBehaviour
{

    [SerializeField]
    GameObject orbPrefab;

    [SerializeField]
    TMP_Text textDisplay;

    [SerializeField]
    AnimationCurve ringDistribution;

    [SerializeField]
    Gradient ringColors;
    GameObject[] luxOrbs;
    LuxRings[] rings;
    int totalLux;
    int availableLux;

    void Awake()
    {
        rings = GameObject.FindObjectsOfType<LuxRings>();
    }

    void Start()
    {
        totalLux = GameObject.FindObjectOfType<GlobalConfig>().luxLimit;
        availableLux = totalLux;
        luxOrbs = new GameObject[totalLux];
        GenerateLuxOrbs();
        UpdateTextDisplay();
        ChangeAvailableLux(0);
    }

    void GenerateLuxOrbs()
    {
        for (int i = 0; i < totalLux; i++)
        {
            GameObject newOrb = Instantiate(orbPrefab, transform);
            float t = (i + 1) / (float)totalLux;
            newOrb.GetComponent<SpriteRenderer>().color = ringColors.Evaluate(t);
            newOrb.transform.localScale *= ringDistribution.Evaluate(t);
            luxOrbs[i] = newOrb;
        }
    }

    public bool LuxAvailable(int request)
    {
        return availableLux >= request;
    }

    public void ChangeAvailableLux(int addition)
    {
        int additionSign = (int)Mathf.Sign(addition);
        int startIndex = addition > 0 ? availableLux : availableLux - 1;
        for (int i = startIndex; i != startIndex + addition; i += additionSign)
        {
            // If it's a positive addition then we are enabling, otherwise we are disabling.
            luxOrbs[i].SetActive(addition > 0);
            availableLux += additionSign;
        }
        UpdateTextDisplay();
        UpdateAllRingColours();
    }

    public void UpdateAllRingColours()
    {

        foreach (LuxRings ring in rings)
        {
            ring.UpdateRingsColour(availableLux);
        }
    }

    void UpdateTextDisplay()
    {
        textDisplay.text = availableLux.ToString();
    }

    int GetAvailableLux()
    {
        return availableLux;
    }

}
