using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    List<ParticleSystem> contactingFlocks = new List<ParticleSystem>();
    void Start()
    {
        StartCoroutine(DestroyContactedMoths());
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (!col.gameObject.CompareTag("MothForces"))
        {
            return;
        }
        ParticleSystem touchingFlock = GetFlockFromCollider(col);
        if (touchingFlock != null)
            contactingFlocks.Add(touchingFlock);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("MothForces"))
        {
            return;
        }
        contactingFlocks.Remove(GetFlockFromCollider(col));
    }

    ParticleSystem GetFlockFromCollider(Collider2D col)
    {
        Transform mothFlockTrans = col.gameObject.transform.Find("Moth Particles");
        if (mothFlockTrans == null)
        {
            Debug.LogError("No child found named 'Moth Particles'");
            return null;
        }
        return mothFlockTrans.gameObject.GetComponent<ParticleSystem>();
    }

    IEnumerator DestroyContactedMoths()
    {
        while (true)
        {
            foreach (ParticleSystem partSys in contactingFlocks)
            {
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[500];
                partSys.GetParticles(particles);
                for (int i = 0; i < Mathf.Min(particles.Length, 10); i++)
                {
                    particles[i].remainingLifetime = 0;
                }
                partSys.SetParticles(particles);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
