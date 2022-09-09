using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool isGameOver = false;

    public GameObject gameLostMenu;

    public GameObject gameWonMenu;

    public GameObject pauseMenu;

    public GameObject dofVolume;

    AudioManager audioManager;

    void Start()
    {
        Time.timeScale = 1f;
        dofVolume.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        MenuController menuController =
            pauseMenu.GetComponent<MenuController>();
        bool isGamePaused = menuController.MenuUI.activeSelf;
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

    void HandleGameOver(GameObject menuObject)
    {
        if (!isGameOver && menuObject != null)
        {
            isGameOver = true;
            audioManager.Pause("Music");
            // End the game. Open menus
        }
    }

    public void LoseGame()
    {
        HandleGameOver (gameLostMenu);
    }

    public void WinGame()
    {
        HandleGameOver (gameWonMenu);
        audioManager.Play("Victory");
        int furthestLevel = PlayerPrefs.GetInt("progress");
        if (SceneManager.GetActiveScene().buildIndex == furthestLevel)
        {
            PlayerPrefs.SetInt("progress", furthestLevel + 1);
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
        SceneManager.LoadScene (levelIndex);
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
