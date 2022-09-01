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
    // Update is called once per frame
    void Update()
    {

    }

    public static void StartNextLevelCoroutine()
    {
        instance.StartCoroutine("LoadNextLevel");
    }

    IEnumerator LoadNextLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
