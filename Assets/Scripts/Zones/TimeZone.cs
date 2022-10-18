using UnityEngine;

public class TimeZone : ZoneBase
{
    bool activated = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MothForces"))
        {
            progress = 0;
            ZoneUI.fillAmount = 0;
        }
    }

    void OnTriggerStay2D()
    {
        progress += Time.deltaTime;
        Mathf.Clamp(progress, 0, activationReq);
        ZoneUI.fillAmount = progress / activationReq;
        if (progress >= activationReq && !activated)
        {
            ZoneEvents.RaiseEvent(id);
            activated = true;
        }
    }

    void OnTriggerExit2D()
    {
        ZoneUI.fillAmount = 0;
        activated = false;
    }
}
