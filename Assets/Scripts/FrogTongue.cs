using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public float speed = 1;

    private GameObject[] mothGroups;

    private Vector3? target;

    private bool targetSelf;

    private ParticleSystem.Particle[]
        mothBuffer = new ParticleSystem.Particle[100];

    // Start is called before the first frame update
    void Start()
    {
        // Find moth groups to eat
        mothGroups = GameObject.FindGameObjectsWithTag("Moths");
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (target.HasValue && !ObjectInRange(target.Value))
        {
            targetSelf = true;
            FindNewTarget();
        }
        else if (!target.HasValue)
        {
            FindNewTarget();
        }
        if (target.HasValue)
        {
            float distanceToTarget = MoveTowardsObject(target.Value);
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
            target = gameObject.transform.position;
            return;
        }
        foreach (GameObject flock in mothGroups)
        {
            if (ObjectInRange(flock.transform.position))
            {
                ParticleSystem partSys = flock.GetComponent<ParticleSystem>();
                int numMoths = partSys.GetParticles(mothBuffer);
                Debug.Log (numMoths);
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
        if (Vector3.Distance(this.transform.position, target) < 5)
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
}
