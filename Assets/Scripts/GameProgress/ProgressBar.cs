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
    public Slider slider;
    public LevelProgressTracker tracker;
    public float fillSpeed;
    private float barWidth = 0;

    private int checkpointsFilled = 0;
    private float currentValue = 0;



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
            checkpointFills.Add(cp.transform.GetChild(0).gameObject.GetComponent<Image>());
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
            float reqStart = checkpointsFilled == 0 ? 0 : checkpointReqs[checkpointsFilled - 1];
            float reqEnd = checkpointsFilled == checkpointReqs.Length - 1 ? 1 : checkpointReqs[checkpointsFilled];
            // Add fill to completed checkpoints
            float newFill = Mathf.Min((targetValue - reqStart) / (float)reqEnd, 1);
            checkpointFills[checkpointsFilled].fillAmount = newFill;
            Debug.Log(reqStart + " " + reqEnd + " " + newFill);

            if (newFill == 1)
            {
                checkpointsFilled++;
            }
        }
    }

}