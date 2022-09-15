using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public Texture2D hoverTexture;
    void OnMouseEnter()
    {
        // Change the cursor icon on hover.
        Cursor.SetCursor(hoverTexture, Vector2.zero, CursorMode.Auto);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
