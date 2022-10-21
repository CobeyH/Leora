using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightRotation : MonoBehaviour
{
    public GameObject leftPivot, rightPivot;
    private Light2D controlledLight;
    private bool canRotate = false;
    private bool rotating = false;
    private Camera mainCam;

    void Awake()
    {
        // TODO: Fix this object location, it's brittle
        GameObject lightEmitter = gameObject.transform.parent.parent.parent.gameObject;
        GameObject lightBase = lightEmitter.transform.parent.gameObject;

        controlledLight = lightEmitter.GetComponent<Light2D>();
        canRotate = lightBase.GetComponent<LightBuilder>().lightData.canRotate;
    }
    // Set the initial leaf position based on the light angle
    void Start()
    {
        mainCam = Camera.main;
        UpdateLeafPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating && canRotate)
        {
            // Calculate the angle between the light and the mouse to determine how to angle the leaves;
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = mainCam.ScreenToWorldPoint(mousePos);
            Vector2 dirToMouse = worldPosition - controlledLight.transform.position;
            float angleToMouse = Vector2.Angle(dirToMouse, controlledLight.transform.up);
            angleToMouse = Mathf.Clamp(angleToMouse, 10, 90);
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
        leftPivot.transform.localEulerAngles = new Vector3(0, 0, -1 * (angle / 2));
        rightPivot.transform.localEulerAngles = new Vector3(0, 0, (angle / 2));
    }
}
