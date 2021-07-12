using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataEntry
{
    public string userName;
    public int score;
    public int idNum;

    public DataEntry(string newUserName, int newScore, int newIdNum)
    {
        userName = newUserName;
        score = newScore;
        idNum = newIdNum;
    }
}
