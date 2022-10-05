using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameController gameController;
    public MenuEventChannelSO ToggleMenuChannel;
    public MenuType Type;

    public GameObject coupledMenu;

    private MenuController coupledMenuController;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        if (coupledMenu != null)
            coupledMenuController = coupledMenu.GetComponent<MenuController>();
        ToggleMenuChannel.OnEventRaised += ToggleMenu;
    }

    void OnDestroy()
    {
        ToggleMenuChannel.OnEventRaised -= ToggleMenu;
    }

    void ToggleMenu(MenuType menu)
    {
        if (menu == Type)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void RaisePauseEvent()
    {
        if (ToggleMenuChannel != null)
        {
            ToggleMenuChannel.RaiseEvent(MenuType.PauseMenu);
        }
    }

    public void RaiseSettingsEvent()
    {
        if (ToggleMenuChannel != null)
        {
            ToggleMenuChannel.RaiseEvent(MenuType.SettingsMenu);
        }
    }

    void Start()
    {
        // All menus should start invisible
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        gameController.RestartLevel();
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
