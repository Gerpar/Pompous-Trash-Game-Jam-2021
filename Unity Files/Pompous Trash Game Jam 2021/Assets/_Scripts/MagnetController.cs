using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Gerad Paris
// Script controls the magnet, and related actions for the player's VTOL
public class MagnetController : MonoBehaviour
{
    [SerializeField] float magnetStrength, magnetRadius;
    [SerializeField] LayerMask affectedLayer;
    [SerializeField] ParticleSystem attachedParticles;
    [SerializeField] float bumpMagnetDelay;

    List<Collider> attachedObjs;
    bool magnetized;
    bool canUseMagnet;

    float emissRate;
    ParticleSystem.EmissionModule magPartEmiss;

    // Start is called before the first frame update
    void Start()
    {
        attachedObjs = new List<Collider>();
        magPartEmiss = attachedParticles.emission;
        emissRate = magPartEmiss.rateOverTime.constant;
        magPartEmiss.rateOverTime = 0;
        canUseMagnet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Magnetize") && canUseMagnet)
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
            magPartEmiss.rateOverTime = 0;
            int objCount = attachedObjs.Count;
            // Remove spring joints when magnet is deactivated
            for(int i = 0; i < objCount; i++)
            {
                if(attachedObjs[0])
                {
                    Destroy(attachedObjs[0].GetComponent<SpringJoint>());
                    attachedObjs.RemoveAt(0);
                }
                else
                {
                    attachedObjs.RemoveAt(0);
                }
            }
        }
        else
        {
            magPartEmiss.rateOverTime = emissRate;
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

    public void DisableMagnet()
    {
        magnetized = false;
        StartCoroutine(BumpDelay());
        magPartEmiss.rateOverTime = 0;
        int objCount = attachedObjs.Count;
        // Remove spring joints when magnet is deactivated
        for (int i = 0; i < objCount; i++)
        {
            Destroy(attachedObjs[0].GetComponent<SpringJoint>());
            attachedObjs.RemoveAt(0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }

    public void RemoveObject(Collider objCollider)
    {
        Destroy(objCollider.GetComponent<SpringJoint>());
        attachedObjs.Remove(objCollider);
    }

    IEnumerator BumpDelay()
    {
        canUseMagnet = false;
        yield return new WaitForSeconds(bumpMagnetDelay);
        canUseMagnet = true;
    }
}