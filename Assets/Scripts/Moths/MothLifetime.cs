using UnityEngine;

public class MothLifetime : MonoBehaviour
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

    // Destroy the particle system and force field when all the moths are dead.
    void Update()
    {
        if (emitter.particleCount == 0)
        {
            Transform trans = emitter.gameObject.transform;
            GameObject parent = trans.gameObject;
            Destroy(parent);
        }
    }

    public int getFlockSize()
    {
        return flockSize;
    }
}
