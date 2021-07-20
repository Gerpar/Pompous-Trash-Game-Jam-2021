using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject trackingObj;
    [SerializeField] float trackingSpeed;
    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float zoomSensitivity = 10.0f;
    [SerializeField] float minZoomDistance, maxZoomDistance;
    [SerializeField] LayerMask clippingLayers;

    Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CameraTracking());
        camOffset = transform.position - trackingObj.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        if(Time.timeScale == 0)
            return;

        // Apply rotation to the offset
        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * mouseSensitivity, Vector3.up);

        camOffset = camTurnAngle * camOffset;

        float scrollVal = Input.GetAxis("Mouse ScrollWheel");

        Vector3 newPos = trackingObj.transform.position + camOffset;

        if (scrollVal != 0)
        {
            Vector3 newCamOffset = camOffset + transform.forward * scrollVal * zoomSensitivity;
            float camDistance = Vector3.Distance(newPos, trackingObj.transform.position);

            if (scrollVal < 0)
            {
                // Scrolling out
                if (camDistance < maxZoomDistance)
                {
                    camOffset = newCamOffset;
                    transform.position = Vector3.Slerp(transform.position, newPos, trackingSpeed);
                }
            }
            if (scrollVal > 0)
            {
                // Scrolling in
                if (camDistance > minZoomDistance)
                {
                    camOffset = newCamOffset;
                    transform.position = Vector3.Slerp(transform.position, newPos, trackingSpeed);
                }
            }
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, newPos, trackingSpeed);
        }

        RaycastHit hit;
        // Move the camera closer if it clips into any objects
        if(Physics.Linecast(trackingObj.transform.position, transform.position, out hit, clippingLayers))
        {
            transform.position = hit.point;
        }

        transform.LookAt(trackingObj.transform);
    }
}
