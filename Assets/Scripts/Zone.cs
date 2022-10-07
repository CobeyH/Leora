using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public IntEventChannelSO ZoneEvents;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MothForces"))
        {
            ZoneEvents.RaiseEvent(id);
        }
    }
}
