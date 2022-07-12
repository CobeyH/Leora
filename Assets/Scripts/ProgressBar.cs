using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public float fillSpeed;
    private float targetValue = 0;

    Goal goal;


    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        goal = GameObject.Find("Moth Particles").GetComponent<Goal>();
        // The slider should scale to the number of moths in the game
        fillSpeed = slider.maxValue * 2;
    }

    void Update()
    {
        targetValue = goal.getMothsInGoal();
        if (slider.value < targetValue)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
        else if (slider.value > targetValue)
        {
            slider.value -= fillSpeed * Time.deltaTime;
        }
        // Debug.Log(targetValue);
    }

    public void IncrementProgress(float newProgress)
    {
        targetValue = slider.value + newProgress;
    }


}