using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtolControls : MonoBehaviour
{
    [SerializeField] GameObject thrusterTL, thrusterTR, thrusterBL, thrusterBR, pitchUp, pitchDown, rollRight, rollLeft;
    [SerializeField] float thrusterForce;
    [SerializeField] float maxTilt = 45;
    [SerializeField] float minHeight = 1.0f;
    [SerializeField] float maxHeight = 5.0f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrusters();
        Stabilizer();
        HeightConstraint();
    }

    // Handles the thrust when buttons are pressed
    void Thrusters()
    {
        // Apply thruster forces based on the key pressed in the correct position
        if (Input.GetButton("ThrusterTopLeft"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterTL.transform.forward, thrusterTL.transform.position);
        }
        if (Input.GetButton("ThrusterTopRight"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterTR.transform.forward, thrusterTR.transform.position);
        }
        if (Input.GetButton("ThrusterBotLeft"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterBL.transform.forward, thrusterBL.transform.position);
        }
        if (Input.GetButton("ThrusterBotRight"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterBR.transform.forward, thrusterBR.transform.position);
        }
    }

    // "Stabilizes" the flight of the VTOL
    void Stabilizer()
    {
        Quaternion currentRot = transform.rotation;

        float xA = currentRot.eulerAngles.x;
        float zA = currentRot.eulerAngles.z;

        float diff = 0;

        /// Pitch
        // Tilting forward detection
        if (xA >= maxTilt && xA < 180)
        {
            diff = xA - maxTilt;
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * pitchUp.transform.forward, pitchUp.transform.position);
        }
        // Tilting backwards detection
        else if (xA <= 360 - maxTilt && xA > 180)
        {
            diff = Mathf.Abs(360 - maxTilt - xA);
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * pitchDown.transform.forward, pitchDown.transform.position);
        }

        ///Roll
        // Tilting Left Detection
        if (zA >= maxTilt && zA < 180)
        {
            diff = zA - maxTilt;
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * -rollLeft.transform.forward, rollLeft.transform.position);
        }
        // Tilting Right Detection
        if (zA <= 360 - maxTilt && zA > 180)
        {
            diff = Mathf.Abs(360 - maxTilt - zA);
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * -rollRight.transform.forward, rollRight.transform.position);
        }
    }

    void HeightConstraint()
    {
        if(transform.position.y < minHeight)
        {
            rb.AddForce(Vector3.up * (thrusterForce * 2));
        }
        else if(transform.position.y > maxHeight)
        {
            rb.AddForce(-Vector3.up * (thrusterForce * 2));
        }
    }
}