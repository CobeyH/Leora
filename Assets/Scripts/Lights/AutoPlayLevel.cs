using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class AutoPlayLevel : MonoBehaviour
{
    public Light2D[] lights;

    void Start()
    {
        StartCoroutine(AutoPlaySequence());
    }

    void Update()
    {
        if (Input.anyKey)
        {
            LevelLoader.StartLevelLoadCoroutine("LevelSelector");
        }
    }

    IEnumerator AutoPlaySequence()
    {
        int lightIndex = 1;
        while (lightIndex < lights.Length)
        {
            yield return new WaitForSeconds(5);
            ToggleLight(lightIndex - 1);
            ToggleLight(lightIndex);
            lightIndex++;
        }
    }

    void ToggleLight(int index)
    {
        if (index >= lights.Length || index < 0) return;
        lights[index].enabled = !lights[index].enabled;
    }

}
