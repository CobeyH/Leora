using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SacrificeZone : ZoneBase
{
    List<ParticleSystem> contactingFlocks = new List<ParticleSystem>();
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[500];
    void Start()
    {
        StartCoroutine(DestroyContactedMoths());
    }
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
            ZoneUI.fillAmount -= Time.deltaTime;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        ParticleSystem touchingFlock = GetFlockFromCollider(col);
        if (touchingFlock != null)
            contactingFlocks.Add(touchingFlock);
    }
    void OnTriggerExit2D(Collider2D col)
    {
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
        while (progress < activationReq)
        {
            foreach (ParticleSystem partSys in contactingFlocks)
            {

                int numParticlesAlive = partSys.GetParticles(particles);
                for (int i = 0; i < Mathf.Min(numParticlesAlive, 5); i++)
                {
                    particles[i].remainingLifetime = 0;
                    partSys.TriggerSubEmitter(0, ref particles[i]);
                    progress++;
                }
                partSys.SetParticles(particles, numParticlesAlive);

            }
            yield return new WaitForSeconds(1);
        }
    }
}

