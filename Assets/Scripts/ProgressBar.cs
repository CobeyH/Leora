using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private float[] checkpointRequirements;
    private List<GameObject> checkpoints = new List<GameObject>();
    public GameObject checkpointPrefab;
    public ParticleSystem emitter;
    public Slider slider;
    public float fillSpeed;
    private float barWidth = 0;
    private int totalMoths = 0;
    public static bool LevelComplete = false;
    public static bool LevelSkippable = false;
    public static int butterflyCount = 0;

    List<Goal> goals = new List<Goal>();


    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        // The slider should scale to the number of moths in the game
        fillSpeed = slider.maxValue * 2;
        barWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    void Start()
    {
        // Create checkpoint markers.
        foreach (float req in checkpointRequirements)
        {
            GameObject cp = Instantiate(checkpointPrefab, new Vector3(req * barWidth - barWidth / 2f, 0, 0), Quaternion.identity);
            cp.transform.SetParent(this.transform, false);
            checkpoints.Add(cp);
        }

        // Get count of moths in scene.
        GameObject[] mothFlocks = GameObject.FindGameObjectsWithTag("Moths");
        foreach (GameObject flock in mothFlocks)
        {
            totalMoths += flock.GetComponent<MothSpawner>().getFlockSize();
            goals.Add(flock.GetComponent<Goal>());
        }

    }

    void Update()
    {
        float targetValue = 0;
        foreach (Goal goal in goals)
        {

            targetValue += goal.getMothsInGoal() / (float)totalMoths;
        }
        if (slider.value < targetValue)
        {
            float previousValue = slider.value;
            slider.value += fillSpeed * Time.deltaTime;
            int i = 0;
            foreach (float req in checkpointRequirements)
            {
                if (previousValue < req && slider.value >= req)
                {
                    butterflyCount++;
                    // Add fill to completed checkpoints
                    checkpoints[i].transform.GetChild(0).gameObject.SetActive(true);
                    emitter.Emit(100);
                    // Level can be skipped after the first checkpoint is reached.
                    LevelSkippable = true;
                    // Level automatically completes once the last checkpoint is reached.
                    if (i == checkpointRequirements.Length - 1)
                    {
                        LevelComplete = true;
                    }
                }
                i++;
            }
        }
    }

}