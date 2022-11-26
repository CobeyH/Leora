using UnityEngine;

public class SkipCredits : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            LevelLoader.StartLevelLoadCoroutine("LevelSelector");
        }
    }
}
