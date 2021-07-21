using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Leaderboard : MonoBehaviour
{
    int idNum;
    private DataEntry data;
    private List<DataEntry> dataList = new List<DataEntry>();
    public void SaveScore(string name, int score)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        if (File.Exists(path))
        {
            // get file
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length>0)
            {
                dataList = (List<DataEntry>)bf.Deserialize(stream);
            }

            idNum = dataList.Count;
            // add new entry at correct location
            data = new DataEntry(name, score, idNum);
            if (stream.Length > 0)
            {
                int indexCounter = 0;
                foreach (var entry in dataList)
                {
                    // check if new entry score is more than current entries score
                    if (entry.score <= data.score)
                    {
                        dataList.Insert(indexCounter, data);
                        break;
                    }
                    indexCounter++;
                }
            }
            else
            {
                dataList.Add(data);
            }

            // close old stream and create new stream
            stream.Close();
            stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, dataList);

            stream.Close();
        }
        else
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            idNum = dataList.Count;

            data = new DataEntry(name, score, idNum);

            dataList.Add(data);

            bf.Serialize(stream, dataList);

            stream.Close();
        }
    }

    public Transform content;
    public GameObject score_row;

    public void GetLeaderBoard()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.data";
        if (File.Exists(path))
        {
            // get file
            FileStream stream = new FileStream(path, FileMode.Open);

            dataList = (List<DataEntry>)bf.Deserialize(stream);

            GameObject tempTextBox1 = Instantiate(score_row, transform.position, transform.rotation) as GameObject;
            tempTextBox1.GetComponent<Text>().text = "Id|UserName|Score";

            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }

            tempTextBox1.transform.SetParent(gameObject.transform, false);

            foreach (var entry in dataList)
            {
                //instantiate
                GameObject tempTextBox = Instantiate(score_row, transform.position, transform.rotation) as GameObject;
                tempTextBox.GetComponent<Text>().text = entry.idNum + " | " + entry.userName + " | " + entry.score;
                tempTextBox.transform.SetParent(gameObject.transform, false);
            } 

            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    void Start()
    {
        GetLeaderBoard();
    }
}
