using System.Collections;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;

    public ZoneType type;

    public IntEventChannelSO ZoneEvents;

    public enum ZoneType
    {
        TimeBased,
        PopulationBased
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MothForces"))
        {
            StartCoroutine(ZoneCountdown());
        }
    }

    IEnumerator ZoneCountdown()
    {
        ZoneEvents.RaiseEvent (id);
        yield break;
    }
}
