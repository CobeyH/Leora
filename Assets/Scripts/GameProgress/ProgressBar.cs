using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private List<GameObject> checkpoints = new List<GameObject>();

    private List<Image> checkpointFills = new List<Image>();

    public GameObject checkpointPrefab;

    public ParticleSystem emitter;

    private LevelProgressTracker tracker;

    public float fillSpeed = 0.3f;

    private float barWidth;

    private int checkpointsFilled = 0;

    private float currentValue = 0;

    void Awake()
    {
        barWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }

    void Start()
    {
        tracker =
            GameObject
                .FindGameObjectWithTag("ProgressManager")
                .GetComponent<LevelProgressTracker>();
        float[] checkpointRequirements = tracker.GetCheckPointRequirements();

        // Create checkpoint markers.
        int i = 0;
        foreach (float req in checkpointRequirements)
        {
            float spacing = barWidth / (checkpointRequirements.Length - 1);
            GameObject cp =
                Instantiate(checkpointPrefab,
                new Vector3(i * spacing - barWidth / 2f, 0, 0),
                Quaternion.identity);
            cp.transform.SetParent(this.transform, false);
            checkpoints.Add (cp);
            checkpointFills
                .Add(cp.transform.GetChild(0).gameObject.GetComponent<Image>());
            i++;
        }
    }

    void Update()
    {
        float targetValue = tracker.GetLevelProgress();
        float[] checkpointReqs = tracker.GetCheckPointRequirements();
        if (checkpointsFilled == checkpointReqs.Length)
        {
            return;
        }
        if (currentValue < targetValue)
        {
            currentValue += fillSpeed * Time.deltaTime;
            float reqStart =
                checkpointsFilled == 0
                    ? 0
                    : checkpointReqs[checkpointsFilled - 1];
            float reqEnd =
                checkpointsFilled == checkpointReqs.Length - 1
                    ? 1
                    : checkpointReqs[checkpointsFilled];

            // Add fill to completed checkpoints
            float newFill = Mathf.InverseLerp(reqStart, reqEnd, targetValue);
            checkpointFills[checkpointsFilled].fillAmount = newFill;

            if (newFill == 1)
            {
                checkpointsFilled++;
            }
        }
    }
}
