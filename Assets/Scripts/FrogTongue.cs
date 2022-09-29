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

    private Transform target;

    public bool targetSelf;

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
        if (targetSelf)
        {
            return;
        }

        // If you have a target but it's become invalid then return to self.
        if (target != null && !TargetIsValid(target))
        {
            targetSelf = true;
        } // Find a new target if there isn't one.
        else if (target == null)
        {
            FindNewTarget();
        }
    }

    void FixedUpdate()
    {
        // Move towards target.
        float distanceToTarget;
        if (targetSelf)
        {
            distanceToTarget = MoveTowardsObject(tongue.transform);
        }
        else if (target != null && hunger > eatingPeriod)
        {
            distanceToTarget = MoveTowardsObject(target);
        }
        else
        {
            return;
        }
        SetEdgeCollider();
        CheckTargetStatus (distanceToTarget);
    }

    void CheckTargetStatus(float distanceToTarget)
    {
        if (distanceToTarget < 0.1)
        {
            if (!targetSelf)
            {
                hunger = 0;
            }
            targetSelf = !targetSelf;
        }
    }

    void FindNewTarget()
    {
        timeElapsed = 0;
        foreach (GameObject flock in mothGroups)
        {
            if (flock == null)
            {
                return;
            }
            if (!TargetIsValid(flock.transform))
            {
                continue;
            }
            target = flock.transform;
            if (audioManager != null)
            {
                audioManager.Play("Frog");
            }
            hunger = 0;
            return;
        }
        target = null;
    }

    bool TargetIsValid(Transform target)
    {
        return ObjectInRange(target) && LineOfSight(target);
    }

    bool ObjectInRange(Transform target)
    {
        bool isInRange =
            Vector3.Distance(tongue.transform.position, target.position) <
            maxTargetDistance;
        Vector3 dirToTarget = target.position - tongue.transform.position;
        bool isInFOV =
            Vector3.Angle(dirToTarget, tongue.transform.up) < fieldOfView / 2f;
        return isInRange && isInFOV;
    }

    bool LineOfSight(Transform target)
    {
        int layer_mask = LayerMask.GetMask("Terrain");

        // Linecast from the tongue origin to the target. If it doesn't hit anything then a clear path exists.
        bool test =
            !Physics2D
                .Linecast(tongue.transform.position,
                target.position,
                layer_mask);
        return test;
    }

    float MoveTowardsObject(Transform target)
    {
        // Calculate direction to move tongue
        Vector3 oldPosition = lineRenderer.GetPosition(1);
        Vector3 vecToTarget = target.position - oldPosition;
        Vector3 dirToTarget = vecToTarget.normalized;
        float distanceToMove =
            speed * Time.deltaTime * accelerationCurve.Evaluate(timeElapsed);

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

        edgeCollider.SetPoints (edges);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 position = tongue.transform.position;
        Gizmos.DrawWireSphere (position, maxTargetDistance);
        float angle = fieldOfView;
        float halfFOV = angle / 2.0f;

        // Get length of hypotenuse
        float rayRange =
            maxTargetDistance / Mathf.Cos(halfFOV * Mathf.PI / 180f);
        float coneDirection = 180;

        Quaternion upRayRotation =
            Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.forward);
        Quaternion downRayRotation =
            Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.forward);

        Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
        Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

        Gizmos.DrawRay (position, upRayDirection);
        Gizmos.DrawRay (position, downRayDirection);
        Gizmos.DrawLine(position + downRayDirection, position + upRayDirection);
    }
}
