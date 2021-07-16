using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_UI_Manager : MonoBehaviour
{
    //------------------------------------------
    // UI_Manager - Jared J Roberge
    //
    // This script is a singleton item meant to be placed in every scene. Store a reference to every single canvasGroup component
    // in the groups list to use functions that affect all canvas groups.

    public static Game_UI_Manager instance;
    private bool paused;
    public CanvasGroup pauseMenu;

    public List<CanvasGroup> groups;
    public Text timerText;

    private void Start()
    {
        instance = this;
        paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                ResumeGame();
                HideAllGroups();
            }
            else
            {
                paused = true;
                PauseGame();
                ShowGroup(pauseMenu);
            }
        }
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
        foreach (CanvasGroup g in groups)
        {
            g.alpha = 0;
            g.interactable = false;
            g.blocksRaycasts = false;
        }

        pauseMenu.alpha = 0;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
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

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

