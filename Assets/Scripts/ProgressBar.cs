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
    private float targetValue = 0;
    private float barWidth = 0;
    private int totalMoths = 0;

    Goal goal;


    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        goal = GameObject.Find("Moth Particles").GetComponent<Goal>();
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
        }

    }

    void Update()
    {
        targetValue = goal.getMothsInGoal() / (float)totalMoths;
        if (slider.value < targetValue)
        {
            float previousValue = slider.value;
            slider.value += fillSpeed * Time.deltaTime;
            int i = 0;
            foreach (float req in checkpointRequirements)
            {
                if (previousValue < req && slider.value >= req)
                {
                    checkpoints[i].transform.GetChild(0).gameObject.SetActive(true);
                    emitter.Emit(100);
                }
                i++;
            }
        }
    }

    public void IncrementProgress(float newProgress)
    {
        targetValue = slider.value + newProgress;
    }


}