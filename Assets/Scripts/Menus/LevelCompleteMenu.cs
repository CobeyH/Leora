using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject CompletionMenuUI;
    public GameObject LevelSkipButton;
    public TMP_Text timeSpentDisplay;
    // Update is called once per frame
    void Update()
    {

        if (ProgressBar.LevelComplete)
        {
            timeSpentDisplay.SetText(ConvertTime(Time.time));
            ProgressBar.LevelComplete = false;
            CompletionMenuUI.SetActive(true);
            // Unlock the next level
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex > PlayerPrefs.GetInt("furthestUnlock"))
            {
                PlayerPrefs.SetInt("furthestUnlock", nextSceneIndex);
            }
        }
        else if (ProgressBar.LevelSkippable)
        {
            LevelSkipButton.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        CompletionMenuUI.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        ProgressBar.LevelComplete = false;
    }

    public void HideSkipButton()
    {
        ProgressBar.LevelSkippable = false;
        LevelSkipButton.SetActive(false);
    }

    public string ConvertTime(float deltaTime)
    {
        float minutes = Mathf.Floor(deltaTime / 60);
        float seconds = Mathf.Floor(deltaTime % 60);

        string secondsString;
        string minutesString;

        if (seconds < 10)
        {
            secondsString = "0" + seconds.ToString();
        } else
        {
            secondsString = seconds.ToString();
        }

        if (minutes < 10)
        {
            minutesString = "0" + minutes.ToString();
        }
        else
        {
            minutesString = minutes.ToString();
        }

        return minutesString + ":" + secondsString;

    }
}
