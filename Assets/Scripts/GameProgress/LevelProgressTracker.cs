using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressTracker : MonoBehaviour
{
    [SerializeField]
    private float[] checkpointRequirements;

    List<ParticleTriggers> goals = new List<ParticleTriggers>();

    List<ParticleSystem> flocks = new List<ParticleSystem>();

    private bool LevelSkippable = false;

    private int checkpointsCompleted = 0;

    private int totalMoths = 0;

    private int totalMothsInGoal = 0;

    private float previousProgress = 0;

    // Start is called before the first frame update
    void Awake()
    {
        // Get count of moths in scene.
        GameObject[] mothFlocks = GameObject.FindGameObjectsWithTag("Moths");
        foreach (GameObject flock in mothFlocks)
        {
            ParticleTriggers nextGoal = flock.GetComponent<ParticleTriggers>();
            if (nextGoal.IsDecoy) continue;
            goals.Add(nextGoal);
            totalMoths += flock.GetComponent<MothLifetime>().getFlockSize();
            flocks.Add(flock.GetComponent<ParticleSystem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        int updatedMothsInGoal = 0;
        foreach (ParticleTriggers goal in goals)
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
                checkpointsCompleted = completedCheckpoints;
                break;
            }
            LevelSkippable = true;
            completedCheckpoints++;
        }
        checkpointsCompleted = completedCheckpoints;
        UpdatePlayerPrefScore();
    }
    void UpdatePlayerPrefScore()
    {
        Scene scene = SceneManager.GetActiveScene();
        // TODO: This is hacky. There must be a better way to do this that doesn't require substring
        int level = int.Parse(scene.name.Substring(5));
        int currentScore = GetCheckpointsCompleted();
        string scoreString = "Level" + level + "score";

        if (currentScore != 0)
        {
            UnlockNextLevel();
        }

        if (PlayerPrefs.HasKey(scoreString))
        {
            int prevScore = PlayerPrefs.GetInt(scoreString);

            // if player got higher score this round, overwrite that value
            if (GetCheckpointsCompleted() > prevScore)
            {
                PlayerPrefs.SetInt(scoreString, currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt(scoreString, currentScore);
        }
    }

    void UnlockNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int nextSceneIndex = scene.buildIndex;
        UnlockLevel(nextSceneIndex);
    }

    public static void UnlockLevel(int sceneIndex)
    {
        if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            return;
        }
        if (sceneIndex > PlayerPrefs.GetInt("furthestUnlock"))
        {
            PlayerPrefs.SetInt("furthestUnlock", sceneIndex);
        }
    }

    public float GetLevelProgress()
    {
        return totalMothsInGoal / (float)totalMoths;
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
        // If all the checkpoints are complete then the level is done.
        if (checkpointsCompleted == checkpointRequirements.Length) return true;

        // If the first checkpoint is complete the level cannot be complete.
        if (checkpointsCompleted < 1) return false;

        // If at least one checkpoint has been passed but it's impossible to get more, then terminate early.
        return !IsNextCheckpointReachable();
    }

    public bool IsLevelSkippable()
    {
        return LevelSkippable;
    }

    public bool IsLevelFailed()
    {
        return !IsCheckpointReachable(0);
    }

    public bool IsCheckpointReachable(int checkpointIndex)
    {
        if (checkpointRequirements == null || checkpointRequirements.Length == 0
        ) return true;

        // It's impossible to reach a checkpoint that doesn't exist.
        if (checkpointIndex >= checkpointRequirements.Length) return false;
        int mothsAlive = 0;

        foreach (ParticleSystem flock in flocks)
        {
            if (flock != null)
            {
                mothsAlive += flock.particleCount;
            }
        }

        return (totalMothsInGoal + mothsAlive) / (float)totalMoths >=
        checkpointRequirements[checkpointIndex];
    }

    // Checks if it's possible to reach the next checkpoint with the moths remaining.
    public bool IsNextCheckpointReachable()
    {
        return IsCheckpointReachable(checkpointsCompleted);
    }
}
