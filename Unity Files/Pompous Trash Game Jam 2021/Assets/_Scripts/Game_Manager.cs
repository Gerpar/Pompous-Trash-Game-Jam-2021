using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    //---------------------------------------------
    // This is my mediocre game manager - Jared R.
    // Somebody had to do it okay.
    [Header("Managers")]
    [SerializeField] private Russian_Guyovich announcer;
    [SerializeField] private Game_UI_Manager ui;
    
    [Header("Round time in seconds")]
    public float timer;

    [Header("Next level index")]
    public int sceneIndex;

    [Header("Game objects required for level completion")]
    [SerializeField] List<GameObject> collectableObjects;

    // If this becomes true, the timer stops.
    private bool isWon;

    CurrentScore currentScoreScript;

    // Start is called before the first frame update
    void Start()
    {
        isWon = false;
        InitializeRound();
    }

    void Awake()
    {
        currentScoreScript = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<CurrentScore>();
    }

    void Update()
    {
        // This removes null or missing objects from the list.
        collectableObjects.RemoveAll(GameObject => GameObject == null);

        // Check to see if all objects are gone from scene; if they are, end the round
        if(collectableObjects.Count == 0 || collectableObjects == null)
        {
            StartCoroutine(RoundWon());
        }
    }

    //---------------------------------------------------------------------
    // Announcer speaks, cursor is locked, countdown timer begins.
    // TODO: keep track of all objects that can be placed in drop zones.
    public void InitializeRound()
    {
        // A weird sequence thing where the game is briefly paused while the announcer does shit.
        announcer.StartRound();

        // Cursor stuff
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start the game timer
        StartCoroutine(CountDown(timer));
    }

    //---------------------------------------------------------------------
    // This is the countdown timer for the game.
    // TODO: if the player wins while the timer is running, add the remaining time to the score  (maybe)
    public IEnumerator CountDown(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                float timeText = Mathf.RoundToInt(time);
                ui.timerText.text = timeText.ToString();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //---------------------------------------------------------------------
    // Called when timer runs out. Just calls the announcer, and then waits
    // before going back to the main menu.
    // TODO: Maybe include the scoring system?
    public IEnumerator GameOver()
    {
        currentScoreScript.ResetScore();
        announcer.LoseRound();
        ui.timerText.text = "You Lose";

        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(0);
    }

    public IEnumerator RoundWon()
    {
        // while true?
        // if all objects are cleared
        //{
        announcer.WinRound();
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(sceneIndex);
        //}
    }
}
