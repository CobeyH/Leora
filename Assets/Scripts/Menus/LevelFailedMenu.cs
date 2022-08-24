using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedMenu : MonoBehaviour
{
    public GameObject FailedMenuUI;

    private LevelProgressTracker tracker;

    private bool forceContinue = false;

    void Start()
    {
        tracker =
            GameObject
                .FindGameObjectWithTag("ProgressManager")
                .GetComponent<LevelProgressTracker>();
    }

    // Display menu when the level is failed.
    void Update()
    {
        if (!tracker.LevelIsCompletable())
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        if (!forceContinue)
        {
            FailedMenuUI.SetActive(true);
        }
    }

    public void HideMenu()
    {
        FailedMenuUI.SetActive(false);
        forceContinue = true;
    }
}
