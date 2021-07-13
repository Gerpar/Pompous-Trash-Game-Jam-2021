using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Leaderboard : MonoBehaviour
{
    int idNum = 0;
    public DataEntry data;
    public void SaveScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            data = new DataEntry("ajON", 999, idNum);
            idNum += 1;
            Debug.Log(data.score);
            bf.Serialize(stream, data);

            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            data = new DataEntry("jon", 999, idNum);
            idNum += 1;
            Debug.Log(data.score);
            bf.Serialize(stream, data);
            stream.Close();
        }
    }

    public void GetLeaderBoard()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            DataEntry data = (DataEntry)bf.Deserialize(stream);

            Debug.Log(data.score);

            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
