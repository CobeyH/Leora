using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool isGameOver = false;

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
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic();
        StartCoroutine(CheckLevelFailed());
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

    public void TogglePauseMenu()
    {
        MenuController menuController =
            pauseMenu.GetComponent<MenuController>();
        bool isGamePaused = menuController.gameObject.activeSelf;
        if (isGamePaused)
        {
            menuController.HideMenu();
            DisableDOF();
        }
        else
        {
            menuController.ShowMenu();
            EnableDOF();
        }
    }
    public void EnableDOF()
    {
        dofVolume.SetActive(true);
    }

    public void DisableDOF()
    {
        dofVolume.SetActive(false);
    }

    public void RestartLevel()
    {
        isGameOver = false;
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        // TODO: if this is the last level
        if (true)
        {
            LoadMenu();
        }
        else
        {
            // TODO: Load the next level
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
