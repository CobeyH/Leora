using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MassZone : MonoBehaviour
{
    public int id;

    public int activationAmount;

    public GameObject UIPrefab;

    public IntEventChannelSO ZoneEvents;
    private int mothsEntered;

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
}
