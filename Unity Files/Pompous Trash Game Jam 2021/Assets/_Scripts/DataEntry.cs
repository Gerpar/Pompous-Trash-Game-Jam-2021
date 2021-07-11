using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEntry
{
    string userName;
    int score;
    int idNum = 1;

    public DataEntry(string userName, int score, int idNum)
    {
        userName = userName;
        score = score;
        idNum = idNum;
    }
}
