using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScore : MonoBehaviour 
{
    public int currentScore;

    void Start()
    {
        // Make this object persistent
        Object.DontDestroyOnLoad(this);
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
