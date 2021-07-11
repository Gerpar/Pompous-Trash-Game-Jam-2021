using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataEntry
{
    string userName;
    int score;
    int idNum;

    public DataEntry(string userName, int score, int idNum)
    {
        userName = userName;
        score = score;
        idNum = idNum;
    }
}
