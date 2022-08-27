using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightRotation : MonoBehaviour
{
    public GameObject leftPivot, rightPivot;
    public Light2D controlledLight;
    public bool canRotate = false;
    private bool rotating = false;
    // Set the initial leaf position based on the light angle
    void Start()
    {
        UpdateLeafPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating && canRotate)
        {
            // Calculate the angle between the light and the mouse to determine how to angle the leaves;
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 dirToMouse = worldPosition - controlledLight.transform.position;
            float angleToMouse = Vector2.Angle(dirToMouse, controlledLight.transform.up);
            controlledLight.pointLightOuterAngle = angleToMouse * 2;
            UpdateLeafPositions();
        }
    }

    void OnMouseDown()
    {
        rotating = true;
    }
    void OnMouseUp()
    {
        rotating = false;
    }

    void UpdateLeafPositions()
    {
        float angle = controlledLight.pointLightOuterAngle;
        leftPivot.transform.localEulerAngles = new Vector3(0, 0, -1 * (angle / 2 - 40));
        rightPivot.transform.localEulerAngles = new Vector3(0, 0, (angle / 2 - 40));
    }
}
