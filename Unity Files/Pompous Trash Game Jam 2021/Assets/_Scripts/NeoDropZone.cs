using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jared Roberge - 7/17/2021
// New dropzone script. We're dropping the whole sorting thing. it works, but we cant expect players to know what the hell
// to do, and sorting with a magnet is damn near impossible.
public class NeoDropZone : MonoBehaviour
{
    //public eTrash ZoneType;
    public Russian_Guyovich announcer;
    public int pointValue;

    private void OnCollisionEnter(Collision collision)
    {
        // If the two objects match, destroy and add points.
        if (collision.gameObject.layer == 6)
        {
            // points += collisioncollision.gameObject.GetComponent<NeoTrash>().pointValue
            announcer.Score();
            Destroy(collision.gameObject);
        }
    }
}

public enum eTrash
{
    MISC, A, B, C, D
}
