using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurntable : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject;
    }
    private void Update()
    {
        cam.transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
    }
}
