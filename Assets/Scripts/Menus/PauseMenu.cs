using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public string menuScene;

    private AudioManager audioManager;

    void Start()
    {
        audioManager =
    GameObject
        .FindGameObjectWithTag("AudioManager")
        .GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                // Make sure settings menu isn't oppened.
                if (PauseMenuUI.activeSelf)
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        audioManager.Play("PauseResume");
        HidePauseMenu();
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioManager.ResumeSounds();
    }

    public void HidePauseMenu()
    {
        PauseMenuUI.SetActive(false);
    }

    void Pause()
    {
        audioManager.Play("PauseResume");
        ShowPauseMenu();
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioManager.PauseAllSounds();
    }

    public void ShowPauseMenu()
    {
        PauseMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Resume();
        LevelLoader.StartLevelLoadCoroutine(menuScene);
        GameObject audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().PlayMainTheme();
    }

    public void RestartLevel()
    {
        LevelLoader.RestartLevel();
    }
}
