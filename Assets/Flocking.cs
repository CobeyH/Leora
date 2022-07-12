using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    private ParticleSystem moths;

    // Start is called before the first frame update
    void Start()
    {
        moths = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.Particle[] updatedMoths = new ParticleSystem.Particle[moths.particleCount + 1];
        int l = GetComponent<ParticleSystem>().GetParticles(updatedMoths);
        Collider2D[] test = Physics2D.OverlapCircleAll(updatedMoths[0].position, 4);
        Debug.Log(test.Length);
        for (int i = 0; i < l; i++)
        {
            updatedMoths[i].velocity = new Vector3(2, 0, 0);
        }

        moths.SetParticles(updatedMoths, l);
    }
}
