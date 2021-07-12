using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataEntry
{
    public string userName;
    public int score;
    public int idNum;

    public DataEntry(string userName, int score, int idNum)
    {
        userName = userName;
        score = score;
        idNum = idNum;
    }
}
