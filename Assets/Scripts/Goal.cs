using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool IsDecoy = false;

    ParticleSystem partSys;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    int mothsInGoal;

    // Start is called before the first frame update
    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
        GameObject[] trapLights =
            GameObject.FindGameObjectsWithTag("TrapLight");
        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        var trigger = partSys.trigger;
        foreach (GameObject trap in trapLights)
        {
            trigger.AddCollider(trap.GetComponent<Collider2D>());
        }

        foreach (GameObject goal in goals)
        {
            trigger.AddCollider(goal.GetComponent<CircleCollider2D>());
        }
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter =
            partSys
                .GetTriggerParticles(ParticleSystemTriggerEventType.Enter,
                enter,
                out var enteredData);

        // If no new moths are in the goal, then no work needs to be done.
        if (numEnter <= 0)
        {
            return;
        }

        // Delete particles that enter the goal.
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];

            if (
                enteredData.GetCollider(i, 0).gameObject.tag != "TrapLight" &&
                !IsDecoy
            )
            {
                mothsInGoal++;
            }
            p.remainingLifetime = 0;
            enter[i] = p;
        }

        // Increment progress bar if new moths made it into goal.
        partSys
            .SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

    public int getMothsInGoal()
    {
        return mothsInGoal;
    }
}
