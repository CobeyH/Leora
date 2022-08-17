using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Dimmer : MonoBehaviour
{
    public bool canDim = false;

    public int maxValue = 2;

    public CapsuleCollider2D hitbox;

    public Light2D controlledLight;

    private bool dimming = false;

    private bool inArea = true;

    void OnMouseDown()
    {
        dimming = true;
        inArea = true;
    }

    void OnMouseUp()
    {
        dimming = false;
    }

    void OnMouseExit()
    {
        inArea = false;
    }

    void OnMouseEnter()
    {
        inArea = true;
    }

    void Update()
    {
        if (dimming && canDim && inArea)
        {
            Vector3 mousePos = Input.mousePosition;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 mouseLocalCoords =
                hitbox.transform.InverseTransformPoint(worldPosition);

            // Scale the value between 0 and 1
            float heightClicked =
                (mouseLocalCoords.y + hitbox.size.y / 2) / hitbox.size.y;

            // Scale the value between
            float newIntensity = heightClicked * maxValue;
            newIntensity = Mathf.Max(1, newIntensity);
            controlledLight.intensity = Mathf.Round(newIntensity);
        }
    }
}
