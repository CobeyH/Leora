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
    private float barWidth = 0;

    // Update is called once per frame
    void Update()
    {

        if (ProgressBar.LevelComplete)
        {
            updateScore();
            timeSpentDisplay.SetText(ConvertTime(Time.timeSinceLevelLoad));

            // Adding butterfly counts on the game complete menu
            for (int i = 0; i < ProgressBar.butterflyCount; i++)
            {
                barWidth = butterflyCountDisplay.GetComponent<RectTransform>().rect.width;
                GameObject cp = Instantiate(checkpointPrefab, new Vector3((i / 2f) * barWidth - barWidth / 2f, 0, 0), Quaternion.identity);
                cp.transform.localScale = new Vector3(cp.transform.localScale.x * 2, cp.transform.localScale.y * 2, 0); // change its local scale in x y z format
                cp.transform.SetParent(butterflyCountDisplay.transform, false);
                butterflies.Add(cp);
                cp.transform.GetChild(0).gameObject.SetActive(true);
            }

            ProgressBar.LevelComplete = false;
            CompletionMenuUI.SetActive(true);
            // Unlock the next level
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex > PlayerPrefs.GetInt("furthestUnlock"))
            {
                PlayerPrefs.SetInt("furthestUnlock", nextSceneIndex);
            }
        }
        else if (ProgressBar.LevelSkippable)
        {
            LevelSkipButton.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        CompletionMenuUI.SetActive(false);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        ProgressBar.LevelComplete = false;
    }

    public void HideSkipButton()
    {
        ProgressBar.LevelSkippable = false;
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
        } else
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

    public void updateScore() {
        Scene scene = SceneManager.GetActiveScene();
        int level = int.Parse(scene.name.Substring(5));
        
        if (PlayerPrefs.HasKey("Level" + level + "score"))
        {
            Debug.Log("level " + level);
            Debug.Log("current score" + ProgressBar.butterflyCount);
            Debug.Log("stored playerpref" + PlayerPrefs.GetInt("Level" + level + "score"));


            Debug.Log("player has playerpref");
            // if player got higher score this round, overwrite that value
            if (ProgressBar.butterflyCount > PlayerPrefs.GetInt("Level" + level + "score"))
            {
                PlayerPrefs.SetInt("Level" + level + "score", ProgressBar.butterflyCount);
                Debug.Log("higher score" + ProgressBar.butterflyCount);
            }

        } else
        {
            PlayerPrefs.SetInt("Level" + level + "score", ProgressBar.butterflyCount);
            Debug.Log("player doesnt have pref" + ProgressBar.butterflyCount);

        }
    }
}
