using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugTools : MonoBehaviour
{
    Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && scene.buildIndex > 0 )
        {
            LoadSceneByIndex(scene.buildIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && scene.buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneByIndex(scene.buildIndex + 1);
        }
    }
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
