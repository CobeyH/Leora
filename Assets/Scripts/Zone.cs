using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int id;
    public IntEventChannelSO ZoneEvents;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MothForces"))
        {
            Debug.Log("Valid collision");
        }
    }
}
