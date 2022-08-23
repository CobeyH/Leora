using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject CompletionMenuUI;
    public GameObject LevelSkipButton;
    public TMP_Text timeSpentDisplay;
    public TMP_Text butterflyCountDisplay;
    public GameObject checkpointPrefab;
    private List<GameObject> butterflies = new List<GameObject>();
    private LevelProgressTracker tracker;
    private float barWidth = 0;

    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelProgressTracker>();
    }

    // Update is called once per frame
    void Update()
    {

        if (tracker.IsLevelComplete() && CompletionMenuUI.activeSelf == false)
        {
            updateScore();
            timeSpentDisplay.SetText(ConvertTime(Time.timeSinceLevelLoad));

            // Adding butterfly counts on the game complete menu
            for (int i = 0; i < tracker.GetCheckpointsCompleted(); i++)
            {
                barWidth = butterflyCountDisplay.GetComponent<RectTransform>().rect.width;
                GameObject cp = Instantiate(checkpointPrefab, new Vector3((i / 2f) * barWidth - barWidth / 2f, 0, 0), Quaternion.identity);
                cp.transform.localScale = new Vector3(cp.transform.localScale.x * 2, cp.transform.localScale.y * 2, 0); // change its local scale in x y z format
                cp.transform.SetParent(butterflyCountDisplay.transform, false);
                butterflies.Add(cp);
                cp.transform.GetChild(0).gameObject.SetActive(true);
            }

            CompletionMenuUI.SetActive(true);
            // Unlock the next level
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex > PlayerPrefs.GetInt("furthestUnlock"))
            {
                PlayerPrefs.SetInt("furthestUnlock", nextSceneIndex);
            }
        }
        else if (tracker.IsLevelSkippable())
        {
            LevelSkipButton.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        CompletionMenuUI.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void HideSkipButton()
    {
        LevelSkipButton.SetActive(false);
    }

    public string ConvertTime(float deltaTime)
    {
        float minutes = Mathf.Floor(deltaTime / 60);
        float seconds = Mathf.Floor(deltaTime % 60);

        string secondsString;
        string minutesString;

        if (seconds < 10)
        {
            secondsString = "0" + seconds.ToString();
        }
        else
        {
            secondsString = seconds.ToString();
        }

        if (minutes < 10)
        {
            minutesString = "0" + minutes.ToString();
        }
        else
        {
            minutesString = minutes.ToString();
        }

        return minutesString + ":" + secondsString;

    }

    public void updateScore()
    {
        Scene scene = SceneManager.GetActiveScene();
        int level = int.Parse(scene.name.Substring(5));
        int currentScore = tracker.GetCheckpointsCompleted();
        string scoreString = "Level" + level + "score";

        if (PlayerPrefs.HasKey(scoreString))
        {
            int prevScore = PlayerPrefs.GetInt(scoreString);
            
            // if player got higher score this round, overwrite that value
            if (tracker.GetCheckpointsCompleted() > prevScore)
            {
                PlayerPrefs.SetInt(scoreString, currentScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt(scoreString, currentScore);
        }
    }
}
