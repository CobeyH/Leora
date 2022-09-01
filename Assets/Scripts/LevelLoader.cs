using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;
    static public LevelLoader instance;
    void Awake()
    {
        instance = this;
    }

    public static void StartNextLevelCoroutine()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartLevelLoadCoroutine(nextSceneIndex);
    }

    public static void StartLevelLoadCoroutine(int index)
    {
        object[] parms = new object[1] { index };
        instance.StartCoroutine("LoadLevel", parms);
    }
    public static void StartLevelLoadCoroutine(string index)
    {
        object[] parms = new object[1] { index };
        instance.StartCoroutine("LoadLevel", parms);
    }

    // Start a coroutine to play a scene transition animation and load the scene.
    IEnumerator LoadLevel(object[] parms)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        // This part could probably be improved.
        // The coroutine must accept both integers and strings because either
        // can be used as an index for LoadScene
        if (parms[0] is string)
        {
            SceneManager.LoadScene((string)parms[0]);
        }
        else
        {
            SceneManager.LoadScene((int)parms[0]);
        }
    }
}
