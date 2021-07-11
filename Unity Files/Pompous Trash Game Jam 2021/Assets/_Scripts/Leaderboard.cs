using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Leaderboard : MonoBehaviour
{
    int idNum = 0;
    void SaveScore()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.bin";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            DataEntry data = new DataEntry("ajON", 1, idNum);
            idNum += 1;

            formatter.Serialize(stream, data);

            stream.Close();

        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            DataEntry data = new DataEntry("jon", 1, idNum);
            idNum += 1;

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    void GetLeaderBoard()
    {
        string path = Application.persistentDataPath + "/data.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataEntry[] data = (DataEntry[])formatter.Deserialize(stream);

            Debug.Log(data);

            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
