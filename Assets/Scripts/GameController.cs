using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject

            gameLostMenu,
            gameWonMenu,
            pauseMenu;

    private MenuController

            lostMenuController,
            wonMenuController,
            pauseMenuController,
            settingMenuController;

    public GameObject dofVolume;

    private LevelProgressTracker tracker;

    AudioManager audioManager;

    void Awake()
    {
        tracker =
            GameObject
                .FindGameObjectWithTag("ProgressManager")
                .GetComponent<LevelProgressTracker>();
        pauseMenuController = pauseMenu.GetComponent<MenuController>();
        wonMenuController = gameWonMenu.GetComponent<MenuController>();
        settingMenuController =
            GameObject.Find("SettingsMenu").GetComponent<MenuController>();
        lostMenuController = gameLostMenu.GetComponent<MenuController>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        dofVolume.SetActive(false);
        GameObject audioObject =
            GameObject.FindGameObjectWithTag("AudioManager");
        audioManager = audioObject.GetComponent<AudioManager>();
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
        bool isSettingsOpen = settingMenuController.gameObject.activeSelf;
        if (isSettingsOpen)
        {
            HideSettings();
        }
        else if (IsGamePaused())
        {
            pauseMenuController.HideMenu();
            ResumeGame();
        }
        else
        {
            pauseMenuController.ShowMenu();
            PauseGame();
        }
    }

    public bool IsGamePaused()
    {
        return pauseMenuController.gameObject.activeSelf;
    }

    void HideSettings()
    {
        settingMenuController.HideMenu();
        pauseMenuController.ShowMenu();
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
