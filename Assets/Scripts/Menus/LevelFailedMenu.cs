using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedMenu : MonoBehaviour
{
    public GameObject FailedMenuUI;

    // Display menu when the level is failed.
    void Update()
    {

    }
    public void ShowMenu()
    {
        FailedMenuUI.SetActive(true);
    }

    public void HideMenu()
    {
        FailedMenuUI.SetActive(false);
    }
}
