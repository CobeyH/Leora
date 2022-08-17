using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    ParticleSystem partSys;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    float mothsInGoal;
    GameObject[] trapLights;
    
    // Start is called before the first frame update
    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        trapLights = GameObject.FindGameObjectsWithTag("TrapLight");
        var trigger = partSys.trigger;
        foreach (GameObject trap in trapLights)
        {
            trigger.AddCollider(trap.GetComponent<Collider2D>());
        }
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = partSys.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var enteredData);
        
        // If no new moths are in the goal, then no work needs to be done.
        if (numEnter <= 0)
        {
            return;
        }

        // Delete particles that enter the goal.
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];

            if (LightLimit.IsOverVoltage || enteredData.GetCollider(i, 0).gameObject.tag != "TrapLight")
            {
                mothsInGoal += 1;
            } 
            p.remainingLifetime = 0;
            enter[i] = p;
        }

        // Increment progress bar if new moths made it into goal.

        partSys.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    }

    public float getMothsInGoal()
    {
        return mothsInGoal;
    }
}
