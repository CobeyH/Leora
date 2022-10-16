using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MassZone : MonoBehaviour
{
    public int id;

    public int activationAmount;

    public GameObject UIPrefab;

    public IntEventChannelSO ZoneEvents;
    [HideInInspector]
    public int mothsEntered = 0;

    private Image ZoneUI;

    void Awake()
    {
        GameObject ZoneUIObject =
            Instantiate(UIPrefab, transform.position, Quaternion.identity);
        ZoneUIObject
            .transform
            .SetParent(GameObject.Find("SpriteOverlay").transform);

        ZoneUI = ZoneUIObject.GetComponent<Image>();
    }

    void OnTriggerStay2D()
    {
        ZoneUI.fillAmount = mothsEntered / (float)activationAmount;
    }
}
