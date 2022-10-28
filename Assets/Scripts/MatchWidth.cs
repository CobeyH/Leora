using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{
    // Set this to the in-world distance between the left & right edges of your scene.
    float sceneWidth = 22;

    Camera _camera;
    GlobalConfig config;

    void Start()
    {
        _camera = GetComponent<Camera>();
        config = GameObject.FindObjectOfType<GlobalConfig>();
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        sceneWidth = config.zoom;
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
    }
}
