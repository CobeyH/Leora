using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject CompletionMenuUI;
    public GameObject LevelSkipButton;
    // Update is called once per frame
    void Update()
    {

        if (ProgressBar.LevelComplete)
        {
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
}
