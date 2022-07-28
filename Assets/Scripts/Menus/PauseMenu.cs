using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public string menuScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        HidePauseMenu();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void HidePauseMenu()
    {
        PauseMenuUI.SetActive(false);
    }

    void Pause()
    {
        ShowPauseMenu();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ShowPauseMenu()
    {
        PauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(menuScene);
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
