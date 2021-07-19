using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string input;
    public GameObject btn;
    public void Save()
    {
        btn.GetComponent<Leaderboard>().SaveScore(input, GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<CurrentScore>().currentScore);
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
        if(input == "")
        {
            btn.GetComponent<Button>().interactable = false;
        }
        else
        {
            btn.GetComponent<Button>().interactable = true;
        }
    }
}
