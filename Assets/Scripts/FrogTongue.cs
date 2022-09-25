using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public EdgeCollider2D edgeCollider;
    public AnimationCurve accelerationCurve;

    public int eatingPeriod = 1;

    public float speed = 1;

    [Range(10, 90)]
    public int fieldOfView = 30;

    [Range(1f, 0f)]
    public float accuracy = 0.5f;

    public float maxTargetDistance = 5f;

    public GameObject tongue;

    private AudioManager audioManager;

    private GameObject[] mothGroups;

    private Vector3? target;
    private bool targetSelf;

    private float hunger = 0;
    private float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Find moth groups to eat
        mothGroups = GameObject.FindGameObjectsWithTag("MothForces");
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, tongue.transform.position);
        lineRenderer.SetPosition(1, tongue.transform.position);
        audioManager =
            GameObject
                .FindGameObjectWithTag("AudioManager")
                .GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hunger += Time.deltaTime;
        timeElapsed += Time.deltaTime;
        // Return tongue to self when target goes out of range.
        if (target.HasValue && !ObjectInRange(target.Value))
        {
            targetSelf = true;
            FindNewTarget();

        }
        // Find a new target if there isn't one.
        else if (!target.HasValue)
        {
            if (!targetSelf && hunger < eatingPeriod)
            {
                return;
            }
            FindNewTarget();
        }
    }

    void FixedUpdate()
    {
        // Move towards target.
        if (target.HasValue)
        {
            float distanceToTarget = MoveTowardsObject(target.Value);
            SetEdgeCollider();
            if (distanceToTarget < 0.1)
            {
                targetSelf = !targetSelf;
                target = null;
            }
        }
    }

    void FindNewTarget()
    {
        timeElapsed = 0;
        if (targetSelf)
        {
            target = tongue.transform.position;
            return;
        }
        foreach (GameObject flock in mothGroups)
        {
            if (!ObjectInRange(flock.transform.position))
            {
                continue;
            }
            Vector3 targetPos = flock.transform.position;
            target =
                new Vector3(targetPos.x + Random.Range(-accuracy, accuracy),
                    targetPos.y + Random.Range(-accuracy, accuracy),
                    0);
            if (audioManager != null)
            {
                audioManager.Play("Frog");
            }
            hunger = 0;
            return;
        }
        target = null;
    }

    bool ObjectInRange(Vector3 target)
    {
        bool isInRange =
            Vector3.Distance(tongue.transform.position, target) < maxTargetDistance;
        Vector3 dirToTarget = target - tongue.transform.position;
        bool isInFOV =
            Vector3.Angle(dirToTarget, tongue.transform.up) < fieldOfView / 2f;
        if (isInRange && isInFOV)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float MoveTowardsObject(Vector3 target)
    {
        // Calculate direction to move tongue
        Vector3 oldPosition = lineRenderer.GetPosition(1);
        Vector3 vecToTarget = target - oldPosition;
        Vector3 dirToTarget = vecToTarget.normalized;
        float distanceToMove = speed * Time.deltaTime * accelerationCurve.Evaluate(timeElapsed);

        // Update tongue end position
        Vector3 newPosition = oldPosition + dirToTarget * distanceToMove;
        lineRenderer.SetPosition(1, newPosition);

        return vecToTarget.magnitude - distanceToMove;
    }

    void SetEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();
        for (int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 linePoint =
                transform
                    .InverseTransformPoint(lineRenderer.GetPosition(point));
            edges.Add(new Vector2(linePoint.x, linePoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 position = tongue.transform.position;
        Gizmos.DrawWireSphere(position, maxTargetDistance);
        float angle = fieldOfView;
        float halfFOV = angle / 2.0f;
        // Get length of hypotenuse
        float rayRange = maxTargetDistance / Mathf.Cos(halfFOV * Mathf.PI / 180f);
        float coneDirection = 180;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.forward);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.forward);

        Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
        Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

        Gizmos.DrawRay(position, upRayDirection);
        Gizmos.DrawRay(position, downRayDirection);
        Gizmos.DrawLine(position + downRayDirection, position + upRayDirection);

    }
}
