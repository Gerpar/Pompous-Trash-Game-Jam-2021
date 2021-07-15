using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VtolControls : MonoBehaviour
{
    enum Thruster { Top_Left, Top_Right, Bot_Left, Bot_Right};

    [SerializeField] GameObject thrusterTL, thrusterTR, thrusterBL, thrusterBR, pitchUp, pitchDown, rollRight, rollLeft;
    [SerializeField] float thrusterForce;

    [Header("VTOL Stabilizers")]
    [SerializeField][Tooltip("How many degrees the VTOL can tip before stabilizers take effect")] float maxTilt = 45;
    [SerializeField] float minHeight = 1.0f;
    [SerializeField] float maxHeight = 5.0f;
    [SerializeField] ParticleSystem thrusterEnabledParticles, thrusterDisabledParticles;
    [SerializeField] MagnetController attachedMagnet;
    [SerializeField] GameObject bumpVFX;

    Rigidbody rb;

    // TTL = Thruster Top Left
    // TTR = Thruster Top Right
    // TBL = Thruster Bottom Left
    // TBR = Thruster Bottom Right
    //Particle variables
    ParticleSystem.MainModule TTLMain, TTRMain, TBLMain, TBRMain;
    ParticleSystem.ColorOverLifetimeModule TTLCOL, TTRCOL, TBLCOL, TBRCOL;

    // Thruster enabled states
    bool TTLEnabled, TTREnabled, TBLEnabled, TBREnabled;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        TTLMain = thrusterTL.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        TTRMain = thrusterTR.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        TBLMain = thrusterBL.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        TBRMain = thrusterBR.transform.GetChild(0).GetComponent<ParticleSystem>().main;

        TTLCOL = thrusterTL.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
        TTRCOL = thrusterTR.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
        TBLCOL = thrusterBL.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
        TBRCOL = thrusterBR.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            Thrusters();
            Stabilizer();
            HeightConstraint();
        }
    }

    /// <summary>
    /// Handles the ship's thrust when inputs are pressed
    /// </summary>
    void Thrusters()
    {
        // Apply thruster forces based on the key pressed in the correct position
        if (Input.GetButton("ThrusterTopLeft"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterTL.transform.forward * Time.deltaTime, thrusterTL.transform.position);
            if (!TTLEnabled) ToggleThruster(Thruster.Top_Left);
        }
        else
        {
            if (TTLEnabled) ToggleThruster(Thruster.Top_Left);
        }

        if (Input.GetButton("ThrusterTopRight"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterTR.transform.forward * Time.deltaTime, thrusterTR.transform.position);
            if (!TTREnabled) ToggleThruster(Thruster.Top_Right);
        }
        else
        {
            if (TTREnabled) ToggleThruster(Thruster.Top_Right);
        }

        if (Input.GetButton("ThrusterBotLeft"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterBL.transform.forward * Time.deltaTime, thrusterBL.transform.position);
            if (!TBLEnabled) ToggleThruster(Thruster.Bot_Left);
        }
        else
        {
            if (TBLEnabled) ToggleThruster(Thruster.Bot_Left);
        }

        if (Input.GetButton("ThrusterBotRight"))
        {
            rb.AddForceAtPosition(0.5f * thrusterForce * -thrusterBR.transform.forward * Time.deltaTime, thrusterBR.transform.position);
            if (!TBREnabled) ToggleThruster(Thruster.Bot_Right);
        }
        else
        {
            if (TBREnabled) ToggleThruster(Thruster.Bot_Right);
        }
    }

    /// <summary>
    /// Stabilizes the flight of the VTOL by adding force to counter tipping if the ship begins to tip over the degree limiter. Force increases the greater the difference over the limit
    /// </summary>
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
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * pitchUp.transform.forward * Time.deltaTime, pitchUp.transform.position);
        }
        // Tilting backwards detection
        else if (xA <= 360 - maxTilt && xA > 180)
        {
            diff = Mathf.Abs(360 - maxTilt - xA);
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * pitchDown.transform.forward * Time.deltaTime, pitchDown.transform.position);
        }

        ///Roll
        // Tilting Left Detection
        if (zA >= maxTilt && zA < 180)
        {
            diff = zA - maxTilt;
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * -rollLeft.transform.forward * Time.deltaTime, rollLeft.transform.position);
        }
        // Tilting Right Detection
        if (zA <= 360 - maxTilt && zA > 180)
        {
            diff = Mathf.Abs(360 - maxTilt - zA);
            rb.AddForceAtPosition((thrusterForce * 1.5f + (diff / 100)) * -rollRight.transform.forward * Time.deltaTime, rollRight.transform.position);
        }
    }

    /// <summary>
    /// Soft caps Y Axis between minHeight and maxHeight
    /// </summary>
    void HeightConstraint()
    {
        if(transform.position.y < minHeight)
        {
            rb.AddForce(Vector3.up * (thrusterForce * 2) * Time.deltaTime);
        }
        else if(transform.position.y > maxHeight)
        {
            rb.AddForce(-Vector3.up * (thrusterForce * 2) * Time.deltaTime);
        }
    }

    /// <summary>
    /// Updates particle effects attached to thruster
    /// </summary>
    /// <param name="thrusterToToggle"></param>
    void ToggleThruster(Thruster thrusterToToggle)
    {
        switch (thrusterToToggle)
        {
            case Thruster.Top_Left:
                TTLEnabled = !TTLEnabled;

                if(TTLEnabled)
                {
                    // Change particles to be on
                    TTLMain.startColor = thrusterEnabledParticles.main.startColor;
                    TTLCOL.color = thrusterEnabledParticles.colorOverLifetime.color;
                }
                else
                {
                    // Change particles to be off
                    TTLMain.startColor = thrusterDisabledParticles.main.startColor;
                    TTLCOL.color = thrusterDisabledParticles.colorOverLifetime.color;
                }
                break;
            case Thruster.Top_Right:
                TTREnabled = !TTREnabled;

                if (TTREnabled)
                {
                    // Change particles to be on
                    TTRMain.startColor = thrusterEnabledParticles.main.startColor;
                    TTRCOL.color = thrusterEnabledParticles.colorOverLifetime.color;
                }
                else
                {
                    // Change particles to be off
                    TTRMain.startColor = thrusterDisabledParticles.main.startColor;
                    TTRCOL.color = thrusterDisabledParticles.colorOverLifetime.color;
                }
                break;
            case Thruster.Bot_Left:
                TBLEnabled = !TBLEnabled;

                if (TBLEnabled)
                {
                    // Change particles to be on
                    TBLMain.startColor = thrusterEnabledParticles.main.startColor;
                    TBLCOL.color = thrusterEnabledParticles.colorOverLifetime.color;
                }
                else
                {
                    // Change particles to be off
                    TBLMain.startColor = thrusterDisabledParticles.main.startColor;
                    TBLCOL.color = thrusterDisabledParticles.colorOverLifetime.color;
                }
                break;
            case Thruster.Bot_Right:
                TBREnabled = !TBREnabled;

                if (TBREnabled)
                {
                    // Change particles to be on
                    TBRMain.startColor = thrusterEnabledParticles.main.startColor;
                    TBRCOL.color = thrusterEnabledParticles.colorOverLifetime.color;
                }
                else
                {
                    // Change particles to be off
                    TBRMain.startColor = thrusterDisabledParticles.main.startColor;
                    TBRCOL.color = thrusterDisabledParticles.colorOverLifetime.color;
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bumpable"))
        {
            attachedMagnet.DisableMagnet();
            GameObject vfx = Instantiate(bumpVFX, collision.GetContact(0).point, transform.rotation, null);
            Destroy(vfx, 10f);
        }
    }
}