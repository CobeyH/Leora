using System.Collections;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;

    public float triggerTime;

    public IntEventChannelSO ZoneEvents;

    private float timeInside;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MothForces"))
        {
            timeInside = 0;
        }
    }

    void OnTriggerStay2D()
    {
        timeInside += Time.deltaTime;
        if (timeInside > triggerTime)
        {
            ZoneEvents.RaiseEvent (id);
            timeInside = 0;
        }
    }
}
