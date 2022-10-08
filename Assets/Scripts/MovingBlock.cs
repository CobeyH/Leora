using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector2 endPoint;

    Vector2 startPoint;

    public float speed;

    private bool blockMoving = false;

    public void Trigger()
    {
        StartCoroutine(MoveBlock());
    }

    public void Start()
    {
        startPoint = transform.position;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Trigger();
        }
    }

    IEnumerator MoveBlock()
    {
        if (blockMoving)
        {
            yield break;
        }
        blockMoving = true;

        Vector3 moveDir = (endPoint - startPoint).normalized;

        while (Vector3.Distance(transform.position, endPoint) > 0.1)
        {
            transform.position += moveDir * speed / 60f;
            yield return new WaitForFixedUpdate();
        }
        Vector2 temp = startPoint;
        startPoint = endPoint;
        endPoint = temp;
        blockMoving = false;
        yield break;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(endPoint, 0.25f);
    }
}
