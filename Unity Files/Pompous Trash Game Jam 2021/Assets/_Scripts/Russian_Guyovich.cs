using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Russian_Guyovich : MonoBehaviour
{
    public static Russian_Guyovich instance;

    public AudioSource source;
    public AudioClip roundStart;
    public List<AudioClip> penalty;
    public List<AudioClip> score;
    public List<AudioClip> lose;
    public List<AudioClip> win;

    private void Start()
    {
        instance = this;
    }

    public void StartRound()
    {
        source.clip = roundStart;
        source.Play();
    }

    public void LoseRound()
    {
        // Select Random shot in array
        int audioID = UnityEngine.Random.Range(0, lose.Count);

        // If audio does not exist, throw an exception just in case.
        if (audioID < 0 || audioID > lose.Count)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            // Play the audio
            source.clip = lose[audioID];
            source.Play();
        }
    }

    public void WinRound()
    {
        // Select Random shot in array
        int audioID = UnityEngine.Random.Range(0, win.Count);

        // If audio does not exist, throw an exception just in case.
        if (audioID < 0 || audioID > win.Count)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            // Play the audio
            source.clip = win[audioID];
            source.Play();
        }
    }

    public void Penalty()
    {
        // Select Random shot in array
        int audioID = UnityEngine.Random.Range(0, penalty.Count);

        // If audio does not exist, throw an exception just in case.
        if (audioID < 0 || audioID > penalty.Count)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            // Play the audio
            source.clip = penalty[audioID];
            source.Play();
        }
    }

    public void Score()
    {
        // Select Random shot in array
        int audioID = UnityEngine.Random.Range(0, score.Count);

        // If audio does not exist, throw an exception just in case.
        if (audioID < 0 || audioID > score.Count)
        {
            throw new IndexOutOfRangeException();
        }
        else
        {
            // Play the audio
            source.clip = score[audioID];
            source.Play();
        }
    }
}
