using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public EdgeCollider2D edgeCollider;

    public int eatingPeriod = 1;

    public float speed = 1;

    public GameObject tongue;

    private GameObject[] mothGroups;

    private Vector3? target;

    private bool targetSelf;

    private ParticleSystem.Particle[]
        mothBuffer = new ParticleSystem.Particle[100];

    private float hunger = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Find moth groups to eat
        mothGroups = GameObject.FindGameObjectsWithTag("Moths");
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, tongue.transform.position);
        lineRenderer.SetPosition(1, tongue.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        hunger += Time.deltaTime;
        if (target.HasValue && !ObjectInRange(target.Value))
        {
            targetSelf = true;
            FindNewTarget();
        }
        else if (!target.HasValue)
        {
            if (!targetSelf && hunger < (eatingPeriod))
            {
                return;
            }
            hunger = 0;
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
        if (targetSelf)
        {
            target = tongue.transform.position;
            return;
        }
        foreach (GameObject flock in mothGroups)
        {
            if (ObjectInRange(flock.transform.position))
            {
                ParticleSystem partSys = flock.GetComponent<ParticleSystem>();
                int numMoths = partSys.GetParticles(mothBuffer);
                if (numMoths > 0)
                {
                    target =
                        flock.transform.TransformPoint(mothBuffer[0].position);
                    return;
                }
            }
        }
        target = null;
    }

    bool ObjectInRange(Vector3 target)
    {
        if (Vector3.Distance(tongue.transform.position, target) < 5)
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
        float distanceToMove = speed * Time.deltaTime;

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
}
