using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadialLightLimit : MonoBehaviour
{
    [Tooltip("The maximum allow lux the player can use at one time")]
    public int totalLux;

    [SerializeField]
    GameObject orbPrefab;

    [SerializeField]
    TMP_Text textDisplay;

    [SerializeField]
    AnimationCurve ringDistribution;
    GameObject[] luxOrbs;
    int availableLux;

    void Start()
    {
        availableLux = totalLux;
        luxOrbs = new GameObject[totalLux];
        GenerateLuxOrbs();
        UpdateTextDisplay();
    }

    void GenerateLuxOrbs()
    {
        for (int i = 0; i < totalLux; i++)
        {
            GameObject newOrb = Instantiate(orbPrefab, transform);
            float t = (i + 1) / (float)totalLux;
            newOrb.transform.localScale *= ringDistribution.Evaluate(t);
            luxOrbs[i] = newOrb;
        }
    }

    public void ChangeAvailableLux(int addition)
    {
        for (int i = availableLux; i < availableLux + addition; i++)
        {
            // If it's a positive addition then we are enabling, otherwise we are disabling.
            luxOrbs[i].SetActive(addition > 0);
        }
        availableLux += addition;
        UpdateTextDisplay();
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
