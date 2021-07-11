using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    //------------------------------------------
    // UI_Manager - Jared J Roberge
    //
    // This script is a singleton item meant to be placed in every scene. Store a reference to every single canvasGroup component
    // in the groups list to use functions that affect all canvas groups.

    public static UI_Manager instance;

    public List<CanvasGroup> groups;

    private void Start()
    {
        instance = this;
    }

    //-------------------------------------------
    // These functions can be called anywhere by using UI_Manager.instance.yourFunction.
    // Or, you can hook some of them up to buttons and other UI elements.

    public void ShowGroup(CanvasGroup group)
    {
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    public void HideGroup(CanvasGroup group)
    {
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    public void HideAllGroups()
    {
        foreach(CanvasGroup g in groups)
        {
            g.alpha = 0;
            g.interactable = false;
            g.blocksRaycasts = false;
        }
    }

    public void ShowAllGroups()
    {
        // Not sure why you'd ever want to use this one tbh.
        foreach (CanvasGroup g in groups)
        {
            g.alpha = 1;
            g.interactable = true;
            g.blocksRaycasts = true;
        }
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
