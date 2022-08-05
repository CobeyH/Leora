using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Dimmer : MonoBehaviour
{
    public bool canDim = false;
    public CapsuleCollider2D hitbox;

    void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mouseLocalCoords = hitbox.transform.InverseTransformPoint(worldPosition);

        float heightClicked = (mouseLocalCoords.y + hitbox.size.y / 2) / hitbox.size.y;

    }
}
