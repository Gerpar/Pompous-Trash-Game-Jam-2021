using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreResetter : MonoBehaviour
{
    CurrentScore scoreScript;

    void Awake()
    {
        scoreScript = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<CurrentScore>();
        scoreScript.ResetScore();
    }
}