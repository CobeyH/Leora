using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelButtons : MonoBehaviour
{
    [SerializeField]
    GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // substracting start screen and level selctor scene
        int levelCount = SceneManager.sceneCountInBuildSettings - 2;
        int furthestLevel = PlayerPrefs.GetInt("furthestUnlock", 0);

        for (int levelIndex = 1; levelIndex < levelCount; levelIndex++)
        {
            // Debug.Log("score for scene " + levelIndex.ToString() + ": " + PlayerPrefs.GetInt("Level" + levelIndex.ToString() + "score"));
            GameObject buttonObject = Instantiate(buttonPrefab, gameObject.transform);
            buttonObject.GetComponent<LevelChanger>().level = levelIndex;
            if (levelIndex == 1 || levelIndex < furthestLevel)
            {
                buttonObject.GetComponent<MothLightLevelSelection>().button.interactable = true;
            } else
            {
                buttonObject.GetComponent<MothLightLevelSelection>().button.interactable = false;
            }

        }
    }
}
