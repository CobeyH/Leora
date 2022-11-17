using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class AutoPlayLevel : MonoBehaviour
{
    public Light2D[] lights;
    [SerializeField]
    private IntEventChannelSO lightChannel;

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
            lightChannel.RaiseEvent(lightIndex - 1);
            lightChannel.RaiseEvent(lightIndex);
            lightIndex++;
        }
    }
}
