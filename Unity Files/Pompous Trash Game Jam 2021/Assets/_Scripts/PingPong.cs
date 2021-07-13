using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    // Get a reference to the two points the platform will lerp between
    [SerializeField]
    private GameObject pointA;

    [SerializeField]
    private GameObject pointB;

    [SerializeField]
    private float delay;

    [SerializeField]
    private LineRenderer line;

    public float BounceSpeed;

    // Grab the positions of the 2 points
    private Vector3 A;
    private Vector3 B;

    // Start is called before the first frame update
    void Start()
    {
        A = pointA.transform.position;
        B = pointB.transform.position;
        StartCoroutine(Move());
    }

    private void OnValidate()
    {
        A = pointA.transform.position;
        B = pointB.transform.position;
    }

    private IEnumerator Move()
    {
        bool destination = true;
        while (true)
        {
            if (destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, A, BounceSpeed * Time.deltaTime);
                if (transform.position.x == A.x && transform.position.y == A.y)
                {
                    destination = false;
                    yield return new WaitForSeconds(delay);
                }
            }

            if (!destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, B, BounceSpeed * Time.deltaTime);
                if (transform.position.x == B.x && transform.position.y == B.y)
                {
                    destination = true;
                    yield return new WaitForSeconds(delay);
                }
            }

            line.SetPosition(0, pointA.transform.position);
            line.SetPosition(1, pointB.transform.position);
            

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null)
        {
            Gizmos.DrawSphere(pointA.transform.position, 0.5f);
        }

        if (pointB != null)
        {
            Gizmos.DrawSphere(pointB.transform.position, 0.5f);
        }

        if (pointA != null && pointB != null)
        {
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
}
