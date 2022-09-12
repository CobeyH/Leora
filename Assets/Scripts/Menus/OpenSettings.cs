using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    MenuController settingsMenu;

    MenuController pauseMenu;

    public void Awake()
    {
        settingsMenu =
            GameObject.Find("SettingsMenu").GetComponent<MenuController>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<MenuController>();
    }

    public void ShowSettings()
    {
        pauseMenu.HideMenu();
        settingsMenu.ShowMenu();
    }

    public void HideSettings()
    {
        settingsMenu.HideMenu();
        pauseMenu.ShowMenu();
    }
}
