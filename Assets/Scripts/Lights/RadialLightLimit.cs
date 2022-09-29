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

    [SerializeField]
    Gradient ringColors;
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
