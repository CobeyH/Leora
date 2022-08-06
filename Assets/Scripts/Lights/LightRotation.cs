using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightRotation : MonoBehaviour
{
    public GameObject pivot;
    public Light2D controlledLight;
    public bool clockwiseRotation;
    private bool rotating = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLeafPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {

            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 dirToMouse = worldPosition - controlledLight.transform.position;
            float angleToMouse = Vector2.Angle(dirToMouse, controlledLight.transform.up);
            controlledLight.pointLightOuterAngle = angleToMouse / 2;
            UpdateLeafPosition();
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

    void UpdateLeafPosition()
    {
        float angle = controlledLight.pointLightOuterAngle;
        int rotationDir = clockwiseRotation ? -1 : 1;
        pivot.transform.eulerAngles = new Vector3(0, 0, rotationDir * (angle / 2 - 40));
    }
}
