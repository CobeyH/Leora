using UnityEngine;
using UnityEngine.UI;
public class ZoneBase : MonoBehaviour
{
    public GameObject UIPrefab;
    [HideInInspector]
    public Image ZoneUI;
    public int id;
    public IntEventChannelSO ZoneEvents;
    public float activationReq;

    [HideInInspector]
    public float progress = 0;
    void Awake()
    {
        GameObject ZoneUIObject =
            Instantiate(UIPrefab, transform.position, Quaternion.identity);
        ZoneUIObject
            .transform
            .SetParent(GameObject.Find("SpriteOverlay").transform);

        ZoneUI = ZoneUIObject.GetComponent<Image>();
        ZoneUI.color = gameObject.GetComponent<SpriteRenderer>().color;
    }
}
