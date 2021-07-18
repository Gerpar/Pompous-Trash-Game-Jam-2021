using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardParent : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraTransform;
    Quaternion originalRot;
    void Start()
    {
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cameraTransform.rotation * originalRot;
    }
}
