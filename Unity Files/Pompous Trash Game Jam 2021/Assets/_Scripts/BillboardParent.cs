using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardParent : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraTransform);
    }
}
