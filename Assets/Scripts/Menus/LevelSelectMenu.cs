using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public MenuEventChannelSO ToggleMenuChannel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SendSettingsEvent();
        }
    }

    void SendSettingsEvent()
    {
        if (ToggleMenuChannel != null)
        {
            ToggleMenuChannel.RaiseEvent(MenuType.SettingsMenu);
        }
    }
}
