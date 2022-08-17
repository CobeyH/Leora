using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLight : MonoBehaviour
{

    ParticleSystem partSys;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    
    float mothsInGoal;

    // Start is called before the first frame update
    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        // find traps, and add them to trigger components
    }

    void OnParticleTrigger()
    {

        // get the particles which matched the trigger conditions this frame
        int numEnter = partSys.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // If no new moths are in the goal, then no work needs to be done.
        if (numEnter <= 0)
        {
            return;
        }
        // Delete particles that enter the goal.
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.remainingLifetime = 0;
            enter[i] = p;
        }

        // Increment progress bar if new moths made it into goal.
        mothsInGoal += numEnter;

        partSys.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
