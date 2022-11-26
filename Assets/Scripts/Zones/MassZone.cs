using UnityEngine;

public class MassZone : ZoneBase
{
    void Update()
    {
        float target = progress / activationReq;
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
            ZoneUI.fillAmount -= Time.deltaTime * 2;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        progress += GetMothCount(col);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        progress -= GetMothCount(col);
    }

    int GetMothCount(Collider2D col)
    {
        GameObject mothObject = col.gameObject.transform.Find("Moth Particles").gameObject;
        ParticleSystem moths = mothObject.GetComponent<ParticleSystem>();
        return moths.particleCount;
    }
}
