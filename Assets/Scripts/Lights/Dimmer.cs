using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Dimmer : MonoBehaviour
{
    public bool canDim = false;
    public CapsuleCollider2D hitbox;
    public Light2D controlledLight;
    private bool dimming = false;
    private bool inArea = true;
    private float intensityMultiplier = 0;

    void Start()
    {
        intensityMultiplier = controlledLight.intensity;
    }

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
            Vector3 mouseLocalCoords = hitbox.transform.InverseTransformPoint(worldPosition);

            float heightClicked = (mouseLocalCoords.y + hitbox.size.y / 2) / hitbox.size.y;
            controlledLight.intensity = heightClicked * intensityMultiplier * 2;
        }
    }
}

