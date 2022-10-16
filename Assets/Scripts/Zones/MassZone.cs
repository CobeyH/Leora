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
        ZoneUI.color = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        float target = mothsEntered / (float)activationAmount;
        target = Mathf.Clamp(target, 0f, 1f);
        if (ZoneUI.fillAmount < target)
        {
            ZoneUI.fillAmount += Time.deltaTime;
            if (ZoneUI.fillAmount == 1f)
            {
                ZoneEvents.RaiseEvent(id);
            }
        }
        else if (ZoneUI.fillAmount > target)
        {
            ZoneUI.fillAmount -= Time.deltaTime;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        mothsEntered += GetMothCount(col);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        mothsEntered -= GetMothCount(col);
    }

    int GetMothCount(Collider2D col)
    {
        GameObject mothObject = col.gameObject.transform.Find("Moth Particles").gameObject;
        ParticleSystem moths = mothObject.GetComponent<ParticleSystem>();
        return moths.particleCount;
    }
}
