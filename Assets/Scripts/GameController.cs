using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameLostMenu;

    public GameObject gameWonMenu;

    public GameObject pauseMenu;

    public GameObject dofVolume;

    private LevelProgressTracker tracker;

    AudioManager audioManager;

    void Awake()
    {
        tracker =
            GameObject
                .FindGameObjectWithTag("ProgressManager")
                .GetComponent<LevelProgressTracker>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        dofVolume.SetActive(false);
        GameObject audioObject =
            GameObject.FindGameObjectWithTag("AudioManager");
        audioManager = audioObject.GetComponent<AudioManager>();
        audioManager.PlayMusic();
        StartCoroutine(CheckLevelFailed());
        StartCoroutine(CheckLevelWon());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    IEnumerator CheckLevelFailed()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (tracker.IsLevelFailed())
            {
                gameLostMenu.GetComponent<MenuController>().ShowMenu();

                yield break;
            }
        }
    }

    IEnumerator CheckLevelWon()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (tracker.IsLevelComplete())
            {
                gameWonMenu.GetComponent<MenuController>().ShowMenu();

                yield break;
            }
        }
    }

    public void TogglePauseMenu()
    {
        MenuController menuController =
            pauseMenu.GetComponent<MenuController>();
        bool isGamePaused = menuController.gameObject.activeSelf;
        if (isGamePaused)
        {
            menuController.HideMenu();
            ResumeGame();
        }
        else
        {
            menuController.ShowMenu();
            PauseGame();
        }
    }

    public void PauseGame()
    {
        dofVolume.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        dofVolume.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        LevelLoader.RestartLevel();
    }

    public void LoadMenu()
    {
        LevelLoader.LoadLevel("LevelSelector");
    }
}
