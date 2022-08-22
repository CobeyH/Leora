using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private List<GameObject> checkpoints = new List<GameObject>();
    public GameObject checkpointPrefab;
    public ParticleSystem emitter;
    public Slider slider;
    public LevelProgressTracker tracker;
    public float fillSpeed;
    private float barWidth = 0;

    private int previousCheckpointsCount = 0;



    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        // The slider should scale to the number of moths in the game
        fillSpeed = slider.maxValue * 2;
        barWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelProgressTracker>();
        float[] checkpointRequirements = tracker.GetCheckPointRequirements();
        // Create checkpoint markers.
        foreach (float req in checkpointRequirements)
        {
            GameObject cp = Instantiate(checkpointPrefab, new Vector3(req * barWidth - barWidth / 2f, 0, 0), Quaternion.identity);
            cp.transform.SetParent(this.transform, false);
            checkpoints.Add(cp);
        }


    }

    void Update()
    {
        float targetValue = tracker.GetLevelProgress();
        if (slider.value < targetValue)
        {
            float previousValue = slider.value;
            slider.value += fillSpeed * Time.deltaTime;
            int currentCheckpointCount = tracker.GetCheckpointsCompleted();
            if (currentCheckpointCount > previousCheckpointsCount)
            {
                // Add fill to completed checkpoints
                checkpoints[currentCheckpointCount - 1].transform.GetChild(0).gameObject.SetActive(true);
                emitter.Emit(100);
            }
            previousCheckpointsCount = currentCheckpointCount;
        }
    }

}