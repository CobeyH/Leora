using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightRotation : MonoBehaviour
{
    public GameObject leftPivot, rightPivot;
    public Light2D lightBeams;
    private Light2D controlledLight;
    private bool canRotate = false;
    private bool rotating = false;
    private Camera mainCam;

    // Set the initial leaf position based on the light angle
    void Start()
    {
        GameObject lightEmitter = gameObject.transform.parent.parent.parent.gameObject;
        GameObject lightBase = lightEmitter.transform.parent.gameObject;
        controlledLight = lightEmitter.GetComponent<Light2D>();
        canRotate = lightBase.GetComponent<LightBuilder>().lightData.canRotate;

        mainCam = Camera.main;
        UpdateLeafPositions();
        MatchLightAngles();
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
        }
        MatchLightAngles();
        UpdateLeafPositions();
    }

    // Make the LightEmitter inner angle's max value to be the outer angle's value.
    // Additionally the LightEmitter angle and Beam angle match.
    void MatchLightAngles()
    {
        if (controlledLight.pointLightInnerAngle > controlledLight.pointLightOuterAngle)
        {
            controlledLight.pointLightInnerAngle = controlledLight.pointLightOuterAngle;
        }
        lightBeams.pointLightOuterAngle = controlledLight.pointLightOuterAngle;
        lightBeams.pointLightInnerAngle = controlledLight.pointLightInnerAngle;
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
