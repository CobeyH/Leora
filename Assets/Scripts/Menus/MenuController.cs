using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuUI;

    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        // All menus should start invisible
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        MenuUI.SetActive(true);
    }

    public void HideMenu()
    {
        MenuUI.SetActive(false);
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
