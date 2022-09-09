using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        // All menus should start invisible
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
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
}
