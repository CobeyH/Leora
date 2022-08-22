using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressTracker : MonoBehaviour
{

    [SerializeField]
    private float[] checkpointRequirements;
    List<Goal> goals = new List<Goal>();
    public static bool LevelComplete = false;
    public static bool LevelSkippable = false;
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
            UpdateCompletedCheckpoints(currentProgress);
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
                return;
            }
            LevelSkippable = true;
            completedCheckpoints++;
        }
        LevelComplete = true;
    }


    public float GetLevelProgress()
    {
        return totalMothsInGoal / totalMoths;
    }

    public float[] GetCheckPointRequirements()
    {
        return checkpointRequirements;
    }

    public int GetCheckpointsCompleted()
    {
        return checkpointsCompleted;
    }
}
