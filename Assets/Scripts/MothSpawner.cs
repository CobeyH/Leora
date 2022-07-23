using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSpawner : MonoBehaviour
{
    ParticleSystem emitter;
    [SerializeField]
    private int flockSize = 0;
    // Start is called before the first frame update
    void Start()
    {
        emitter = gameObject.GetComponent<ParticleSystem>();
        emitter.Emit(flockSize);
    }

    public int getFlockSize()
    {
        return flockSize;
    }
}
