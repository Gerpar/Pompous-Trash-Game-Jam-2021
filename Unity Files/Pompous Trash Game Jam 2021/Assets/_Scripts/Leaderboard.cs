using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Leaderboard : MonoBehaviour
{
    int idNum = 0;
    private DataEntry data;
    public void SaveScore(string name, int score)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            data = new DataEntry(name, score, idNum);
            idNum += 1;
            Debug.Log(data.userName);
            Debug.Log(data.score);
            Debug.Log(data.idNum);
            bf.Serialize(stream, data);

            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            data = new DataEntry(name, score, idNum);
            idNum += 1;
            Debug.Log(data.userName);
            Debug.Log(data.score);
            Debug.Log(data.idNum);
            bf.Serialize(stream, data);
            stream.Close();
        }
    }

    public GameObject score_row;

    public void GetLeaderBoard()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
       
            do
            {
                data = (DataEntry)bf.Deserialize(stream);
                Debug.Log(data.userName);
                Debug.Log(data.score);
                Debug.Log(data.idNum);
                //instantiate
                Instantiate(score_row, transform.position, transform.rotation);
            } while (data != null);  

            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
