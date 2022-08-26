using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public Button[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonInteractions();
    }

    private void UpdateButtonInteractions()
    {
        int numUnlocked = PlayerPrefs.GetInt("furthestUnlock", 0);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i > numUnlocked)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        UpdateButtonInteractions();
    }


}
