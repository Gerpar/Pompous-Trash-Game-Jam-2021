using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneController : MonoBehaviour
{
    public Leaderboard board;

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Trash A")
        {
            Destroy(collision.gameObject);
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 128, 0);
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
            board.SaveScore();
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Trash B")
        {
            Destroy(collision.gameObject);
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
}
