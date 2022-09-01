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
        object[] parms = new object[1] { nextSceneIndex };
        instance.StartCoroutine("LoadLevel", parms);
    }

    IEnumerator LoadLevel(object[] parms)
    {
        int nextSceneIndex = (int)parms[0];
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextSceneIndex);
    }
}
