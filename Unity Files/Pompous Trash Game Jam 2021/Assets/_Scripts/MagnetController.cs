using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [SerializeField] float magnetStrength, magnetRadius;
    [SerializeField] LayerMask affectedLayer;

    List<Collider> attachedObjs;
    bool magnetized;

    // Start is called before the first frame update
    void Start()
    {
        attachedObjs = new List<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Magnetize"))
        {
            ToggleMagnet();
        }
    }

    private void FixedUpdate()
    {
        if (magnetized)
        {
            PullObjects();
        }
    }

    void ToggleMagnet()
    {
        magnetized = !magnetized;

        if(!magnetized)
        {
            int objCount = attachedObjs.Count;
            // Remove spring joints when magnet is deactivated
            for(int i = 0; i < objCount; i++)
            {
                Destroy(attachedObjs[0].GetComponent<SpringJoint>());
                attachedObjs.RemoveAt(0);
            }
        }
    }

    void PullObjects()
    {
        // When activating magnet, create new spring joints to attach the objects
        Collider[] hitCols = Physics.OverlapSphere(transform.position, magnetRadius, affectedLayer);
        foreach (Collider col in hitCols)
        {
            if(!attachedObjs.Contains(col))
            {
                GameObject colObj = col.gameObject;
                SpringJoint magSpring;

                // Try getting spring joint, if there isn't a spring joint, make one
                if(!(magSpring = colObj.GetComponent<SpringJoint>()))
                {
                    magSpring = colObj.AddComponent<SpringJoint>();
                }
                
                magSpring.connectedBody = gameObject.GetComponent<Rigidbody>();
                magSpring.autoConfigureConnectedAnchor = false;
                magSpring.connectedAnchor = Vector3.zero;
                magSpring.spring = magnetStrength;
                attachedObjs.Add(col);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
