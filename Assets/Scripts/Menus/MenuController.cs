using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameController gameController;

    public GameObject coupledMenu;

    private MenuController coupledMenuController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        if (coupledMenu != null)
            coupledMenuController = coupledMenu.GetComponent<MenuController>();
    }

    void Start()
    {
        // All menus should start invisible
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        if (coupledMenuController)
        {
            coupledMenuController.HideMenu();
        }
        gameObject.SetActive(true);
        gameController.PauseGame();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        gameController.ResumeGame();
    }

    public void ShowCoupledMenu()
    {
        HideMenu();
        coupledMenuController.ShowMenu();
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
