using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    MenuController settingsMenu;
    MenuController pauseMenu;

    public void Awake()
    {
        settingsMenu = GameObject.Find("SettingsMenu").GetComponent<MenuController>();
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<MenuController>();
    }

    public void ShowSettings()
    {
        settingsMenu.ShowMenu();
        pauseMenu.HideMenu();
    }

    public void HideSettings()
    {

        settingsMenu.HideMenu();
        pauseMenu.ShowMenu();
    }
}
