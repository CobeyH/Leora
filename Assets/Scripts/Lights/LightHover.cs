using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHover : MonoBehaviour
{
    public Texture2D lightOffTexture, lightOnTexture;
    Light2D hoveredLight;

    void Awake()
    {
        hoveredLight = gameObject.GetComponent<Light2D>();
    }

    void OnMouseEnter()
    {
        // Set the cursor to an icon depending on if the light is on or off
        Texture2D cursorTexture = hoveredLight.enabled ? lightOnTexture : lightOffTexture;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseDown()
    {
        // Reset the icon when the light is toggled
        OnMouseEnter();
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
