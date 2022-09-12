using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameController gameController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Start()
    {
        // All menus should start invisible
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        gameController.PauseGame();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        gameController.ResumeGame();
    }

    public void RestartLevel()
    {
        gameController.RestartLevel();
    }

    public void ResumeLevel()
    {
        gameController.TogglePauseMenu();
    }

    public void LoadMenu()
    {
        gameController.LoadMenu();
    }

    public void LoadNextLevel()
    {
        LevelLoader.LoadNextLevel();
    }
}
