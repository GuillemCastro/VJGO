using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour {

    public UnityEvent DiamondCollected;
    public uint DiamondsCollected = 0;
    public uint EnemiesKilled = 0;

    Board m_board;
    PlayerMover m_player;
    List<EnemyManager> m_enemies;
    List<Diamond> m_diamonds;
    GameManager m_game;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += this.OnLevelLoad;
        SceneManager.sceneUnloaded += this.OnLevelUnload;
    }

    public void CheckCollectibles()
    {
        Debug.Log("checkcollectibles");
        if (m_diamonds != null)
        {
            List<Diamond> ds = m_diamonds.FindAll(d => d.Coordinate == m_board.PlayerNode.Coordinate);
            foreach (Diamond diamond in ds)
            {
                if (diamond.isActiveAndEnabled)
                {
                    diamond.Collect();
                    ++DiamondsCollected;
                    if (DiamondCollected != null)
                    {
                        DiamondCollected.Invoke();
                    }
                }
            }
        }
    }

    void OnLevelLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Onlevelload");
        m_game = Object.FindObjectOfType<GameManager>();
        if (m_game != null)
        {
            m_game.setupEvent.AddListener(this.OnLevelStart);
        }
    }

    void OnLevelStart()
    {
        Debug.Log("onlevelstart");
        m_board = Object.FindObjectOfType<Board>();
        m_player = Object.FindObjectOfType<PlayerMover>();
        m_enemies = new List<EnemyManager>(Object.FindObjectsOfType<EnemyManager>());
        if (m_player != null)
        {
            Debug.Log("add finish movement");
            m_player.finishMovementEvent.AddListener(this.CheckCollectibles);
        }
        if (m_board != null)
        {
            Debug.Log("find diamonds");
            m_board = m_board.GetComponent<Board>();
            m_diamonds = m_board.AllDiamonds;
        }
        if (m_enemies != null)
        {
            foreach (EnemyManager e in m_enemies)
            {
                e.deathEvent.AddListener(this.OnEnemyKilled);
            }
        }
    }

    void OnLevelUnload(Scene scene)
    {
        m_board = null;
        m_player = null;
        m_diamonds = null;
        EnemiesKilled = 0;
        m_game = null;
    }

    public void OnEnemyKilled()
    {
        ++EnemiesKilled;
    }

}
