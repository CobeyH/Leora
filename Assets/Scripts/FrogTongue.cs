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
        if (target.HasValue && !ObjectInRange(target.Value))
        {
            targetSelf = true;
            FindNewTarget();
        }
        else if (!target.HasValue)
        {
            if (!targetSelf && hunger < eatingPeriod)
            {
                return;
            }
            FindNewTarget();
        }
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
                new Vector3(targetPos.x + Random.Range(-1, 1),
                    targetPos.y + Random.Range(-1, 1),
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
            Vector3.Distance(tongue.transform.position, target) < 5;
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
}
