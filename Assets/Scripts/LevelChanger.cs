using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public int level;

    public TMP_Text levelText;
    public TMP_Text checkpointBox;
    public GameObject checkpointPrefab;
    private List<GameObject> butterflies = new List<GameObject>();
    private float barWidth = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OpenScene);
        levelText.text = level.ToString();
        displayCheckpoints();
    }

    public void OpenScene()
    {
        LevelLoader.StartLevelLoadCoroutine("Level" + level.ToString());
        GameObject audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().PlayMusic();
    }

    public void displayCheckpoints()
    {
        string scoreRecord = ("Level" + level.ToString() + "score");
        if (PlayerPrefs.HasKey(scoreRecord))
        {
            int checkpointsNum = PlayerPrefs.GetInt(scoreRecord);
            for (int i = 0; i < checkpointsNum; i++)
            {
                barWidth = checkpointBox.GetComponent<RectTransform>().rect.width;
                GameObject cp = Instantiate(checkpointPrefab, new Vector3((i / 2f) * barWidth - barWidth / 2f, 0, 0), Quaternion.identity);
                cp.transform.localScale = new Vector3(cp.transform.localScale.x * 0.5f, cp.transform.localScale.y * 0.5f, 0); // change its local scale in x y z format
                cp.transform.SetParent(checkpointBox.transform, false);
                butterflies.Add(cp);
                cp.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
