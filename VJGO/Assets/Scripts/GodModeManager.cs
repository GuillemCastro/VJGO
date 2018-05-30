using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodModeManager : MonoBehaviour {

    bool created = false;

    bool m_godMode = false;

    public bool IsGodModeActive { get { return m_godMode; } }

    PlayerMover playerMover;
    List<EnemyMover> enemyMovers;

    private void Awake()
    {
        Debug.Log("GM Awake");
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
        SceneManager.sceneLoaded += this.LevelStart;
    }

    private void Start()
    {
        Debug.Log("GM START");
    }

    private void LevelStart(Scene scene, LoadSceneMode sceneMode)
    {
        playerMover = Object.FindObjectOfType<PlayerMover>();
        enemyMovers = new List<EnemyMover>(Object.FindObjectsOfType<EnemyMover>());
        if (IsGodModeActive)
        {
            if (playerMover != null)
            {
                playerMover.moveSpeed = 5;
            }
            if (enemyMovers != null)
            {
                foreach (EnemyMover em in enemyMovers)
                {
                    em.moveSpeed = em.moveSpeed * 2.5f;
                }
            }
        }
    }

    public void ToggleGodMode()
    {
        if (created)
        {
            m_godMode = !m_godMode;
            Debug.Log("GodMode" + m_godMode);
        }
    }
}
