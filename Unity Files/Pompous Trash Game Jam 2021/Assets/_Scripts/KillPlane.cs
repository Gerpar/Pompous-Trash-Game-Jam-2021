using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [SerializeField] MagnetController magnetScript;
    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.layer == 6)
        {
            magnetScript.RemoveObject(collision.collider);
            collision.gameObject.transform.position = collision.gameObject.GetComponent<NeoTrash>().startPos;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
