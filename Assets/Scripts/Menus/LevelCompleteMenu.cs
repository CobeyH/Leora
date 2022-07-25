using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject CompletionMenuUI;
    // Update is called once per frame
    void Update()
    {

        if (ProgressBar.LevelComplete)
        {

            CompletionMenuUI.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        CompletionMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ProgressBar.LevelComplete = false;
    }
}
