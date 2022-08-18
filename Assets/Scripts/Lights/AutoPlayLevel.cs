using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class AutoPlayLevel : MonoBehaviour
{
    public Light2D[] lights;

    private float timeOn = 0;

    private int lightIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("LevelSelector");
        }

        // Turn off and on lights to auto complete the level
        timeOn += Time.deltaTime;
        if (timeOn > 5)
        {
            timeOn = 0;
            lights[lightIndex].enabled = false;
            if (lightIndex < lights.Length - 1)
            {
                lightIndex++;
                lights[lightIndex].enabled = true;
            }
        }
    }
}
