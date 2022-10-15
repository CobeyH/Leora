using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeZone : MonoBehaviour
{
    public int id;

    public float triggerTime;

    public GameObject UIPrefab;

    public IntEventChannelSO ZoneEvents;

    private Image ZoneUI;
    private float timeInside = 0;

    void Awake()
    {
        GameObject ZoneUIObject =
            Instantiate(UIPrefab, transform.position, Quaternion.identity);
        ZoneUIObject
            .transform
            .SetParent(GameObject.Find("SpriteOverlay").transform);

        ZoneUI = ZoneUIObject.GetComponent<Image>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MothForces"))
        {
            timeInside = 0;
            ZoneUI.fillAmount = 0;
        }
    }

    void OnTriggerStay2D()
    {
        timeInside += Time.deltaTime;
        ZoneUI.fillAmount = timeInside / triggerTime;
        if (timeInside > triggerTime)
        {
            ZoneEvents.RaiseEvent(id);
            timeInside = 0;
        }
    }

    void OnTriggerExit2D()
    {
        ZoneUI.fillAmount = 0;
    }
}
