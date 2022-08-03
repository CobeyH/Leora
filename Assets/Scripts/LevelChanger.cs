using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public int level;

    public TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener (OpenScene);
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Level" + level.ToString());
        GameObject audioManager = GameObject.Find("AudioManager");
        audioManager.GetComponent<AudioManager>().PlayMusic();
    }
}
