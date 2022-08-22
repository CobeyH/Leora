using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressTracker : MonoBehaviour
{
    [SerializeField]
    private float[] checkpointRequirements;

    List<Goal> goals = new List<Goal>();

    List<ParticleSystem> flocks = new List<ParticleSystem>();

    private bool LevelSkippable = false;

    private bool LevelComplete = false;

    private int checkpointsCompleted = 0;

    private int totalMoths = 0;

    private int totalMothsInGoal = 0;

    private float previousProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get count of moths in scene.
        GameObject[] mothFlocks = GameObject.FindGameObjectsWithTag("Moths");
        foreach (GameObject flock in mothFlocks)
        {
            totalMoths += flock.GetComponent<MothSpawner>().getFlockSize();
            goals.Add(flock.GetComponent<Goal>());
            flocks.Add(flock.GetComponent<ParticleSystem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        int updatedMothsInGoal = 0;
        foreach (Goal goal in goals)
        {
            updatedMothsInGoal += goal.getMothsInGoal();
        }
        totalMothsInGoal = updatedMothsInGoal;
        float currentProgress = GetLevelProgress();
        if (currentProgress > previousProgress)
        {
            UpdateCompletedCheckpoints (currentProgress);
        }
        previousProgress = currentProgress;
    }

    void UpdateCompletedCheckpoints(float progress)
    {
        int completedCheckpoints = 0;
        foreach (float req in checkpointRequirements)
        {
            if (req > progress)
            {
                checkpointsCompleted = completedCheckpoints;
                return;
            }
            LevelSkippable = true;
            completedCheckpoints++;
        }

        checkpointsCompleted = completedCheckpoints;
        LevelComplete = true;
    }

    public float GetLevelProgress()
    {
        return totalMothsInGoal / (float) totalMoths;
    }

    public float[] GetCheckPointRequirements()
    {
        return checkpointRequirements;
    }

    public int GetCheckpointsCompleted()
    {
        return checkpointsCompleted;
    }

    public bool IsLevelComplete()
    {
        return LevelComplete;
    }

    public bool IsLevelSkippable()
    {
        return LevelSkippable;
    }

    public bool LevelIsCompletable()
    {
        if (checkpointRequirements == null || checkpointRequirements.Length == 0
        )
        {
            return true;
        }
        int mothsAlive = 0;
        foreach (ParticleSystem flock in flocks)
        {
            mothsAlive += flock.particleCount;
        }
        return (totalMothsInGoal + mothsAlive) / (float) totalMoths >=
        checkpointRequirements[0];
    }
}
