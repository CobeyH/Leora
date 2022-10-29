using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MenuEventChannelSO ToggleMenuChannel;
    public GameObject dofVolume;

    private LevelProgressTracker tracker;

    AudioManager audioManager;
    bool _time_slowed = false;

    void Awake()
    {
        tracker =
            GameObject
                .FindGameObjectWithTag("ProgressManager")
                .GetComponent<LevelProgressTracker>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        dofVolume.SetActive(false);
        GameObject audioObject =
            GameObject.FindGameObjectWithTag("AudioManager");
        audioManager = audioObject.GetComponent<AudioManager>();
        StartCoroutine(CheckLevelFailed());
        StartCoroutine(CheckLevelWon());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SendPauseEvent();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = _time_slowed ? 1f : 0.15f;
                _time_slowed = !_time_slowed;
            }
        }
    }

    IEnumerator CheckLevelFailed()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (tracker.IsLevelFailed())
            {
                if (ToggleMenuChannel != null)
                {
                    ToggleMenuChannel.RaiseEvent(MenuType.LevelFailedMenu);
                    PauseGame();
                }
                yield break;
            }
        }
    }

    IEnumerator CheckLevelWon()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            if (tracker.IsLevelComplete())
            {
                if (ToggleMenuChannel != null)
                {
                    ToggleMenuChannel.RaiseEvent(MenuType.LevelWonMenu);
                    PauseGame();
                }

                yield break;
            }
        }
    }

    private void OnEnable()
    {
        ToggleMenuChannel.OnEventRaised += TogglePause;
    }

    private void OnDisable()
    {
        ToggleMenuChannel.OnEventRaised -= TogglePause;
    }

    private void TogglePause(MenuType menu)
    {
        switch (menu)
        {
            case MenuType.PauseMenu:
                if (Time.timeScale == 0)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
                break;
            default:
                break;
        }
    }

    public void SendPauseEvent()
    {
        if (ToggleMenuChannel != null)
        {
            ToggleMenuChannel.RaiseEvent(MenuType.PauseMenu);
        }
    }

    public void PauseGame()
    {
        dofVolume.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        dofVolume.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        LevelLoader.RestartLevel();
    }

    public void LoadMenu()
    {
        LevelLoader.LoadLevel("LevelSelector");
    }
}
