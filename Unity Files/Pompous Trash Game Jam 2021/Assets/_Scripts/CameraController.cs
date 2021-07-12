using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject trackingObj;
    [SerializeField] Vector2 xzOffset;
    [SerializeField] float trackingSpeed;
    [SerializeField] float camHeight;
    [SerializeField] float mouseSensitivity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraTracking());
    }

    void Update()
    {
        
    }

    void RotateCamera()
    {

    }

    /// <summary>
    /// Moves the camera towards it's new position it should be
    /// </summary>
    IEnumerator CameraTracking()
    {
        Transform trackTrans = trackingObj.transform;
        while(true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(trackTrans.position.x + xzOffset.x, camHeight, trackTrans.position.z + xzOffset.y), trackingSpeed * Time.deltaTime);
            transform.LookAt(trackTrans);
            yield return new WaitForEndOfFrame();
        }
    }
}
