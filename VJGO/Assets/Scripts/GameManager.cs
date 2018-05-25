﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    Board m_board;
    PlayerManager m_player;

    bool m_hasLevelStarted = false;
    bool m_isGamePlaying = false;
    bool m_isGameOver = false;
    bool m_hasLevelFinished = false;

    public bool HasLevelStarted
    {
        get
        {
            return m_hasLevelStarted;
        }

        set
        {
            m_hasLevelStarted = value;
        }
    }

    public bool IsGamePlaying
    {
        get
        {
            return m_isGamePlaying;
        }

        set
        {
            m_isGamePlaying = value;
        }
    }

    public bool IsGameOver
    {
        get
        {
            return m_isGameOver;
        }

        set
        {
            m_isGameOver = value;
        }
    }

    public bool HasLevelFinished
    {
        get
        {
            return m_hasLevelFinished;
        }

        set
        {
            m_hasLevelFinished = value;
        }
    }

    public float delay = 1f;

    public UnityEvent setupEvent;
    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();

    }

    // Use this for initialization
    void Start () {
		if (m_player != null && m_board != null)
        {
            StartCoroutine("RunGameLoop");
        }
        else
        {
            Debug.LogWarning("GameManager ERROR: No player or board found!");
        }
	}

    IEnumerator RunGameLoop()
    {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        yield return StartCoroutine("EndLevelRoutine");
    }

    IEnumerator StartLevelRoutine()
    {
        Debug.Log("Setup Level");
        if (setupEvent != null)
        {
            setupEvent.Invoke();
        }
        Debug.Log("Start Level");
        m_player.playerInput.InputEnabled = false;
        while (!m_hasLevelStarted)
        {
            yield return null;
        }
        if (startLevelEvent != null)
        {
            startLevelEvent.Invoke();
        }
    }

    IEnumerator PlayLevelRoutine()
    {
        Debug.Log("Play Level");
        m_isGamePlaying = true;
        yield return new WaitForSeconds(delay);
        m_player.playerInput.InputEnabled = true;

        if (playLevelEvent != null)
        {
            playLevelEvent.Invoke();
        }
        while (!m_isGameOver)
        {
            yield return null;
            m_isGameOver = IsWinner();
        }

        Debug.Log("WIIIIIN! =====");
    }

    IEnumerator EndLevelRoutine()
    {
        Debug.Log("End Level");
        m_player.playerInput.InputEnabled = false;

        if (endLevelEvent != null)
        {
            endLevelEvent.Invoke();
        }
        //show end screen
        while (!m_hasLevelFinished)
        {
            yield return null;
        }
        RestartLevel();
    }

    void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PlayLevel()
    {
        m_hasLevelStarted = true;
    }

    bool IsWinner()
    {
        if (m_board.PlayerNode != null)
        {
            return m_board.PlayerNode == m_board.GoalNode;
        }
        return false;
    }
}
