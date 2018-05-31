using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour {

    public UnityEvent DiamondCollected;
    public UnityEvent StickCollected;

    public UnityEvent AllCollected;

    public uint SticksCollected { get { return m_totalSticks + SticksCollectedLevel; } }
    public uint DiamondsCollected { get { return m_totalDiamonds + DiamondsCollectedLevel; } }
    public uint EnemiesKilled = 0;

    uint m_totalSticks = 0;
    uint m_totalDiamonds = 0;

    public uint SticksCollectedLevel = 0;
    public uint DiamondsCollectedLevel = 0;

    Board m_board;
    PlayerMover m_player;
    List<EnemyManager> m_enemies;
    List<Diamond> m_diamonds;
    List<Stick> m_sticks;
    GameManager m_game;

    AllCollectedUI m_collectedUI;

    Scene m_scene;

    bool restart = false;

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
                    ++DiamondsCollectedLevel;
                    if (DiamondCollected != null)
                    {
                        DiamondCollected.Invoke();
                    }
                }
            }
        }
        if (m_sticks != null)
        {
            List<Stick> ss = m_sticks.FindAll(s => s.Coordinate == m_board.PlayerNode.Coordinate);
            foreach(Stick s in ss)
            {
                if (s.isActiveAndEnabled)
                {
                    s.Collect();
                    ++SticksCollectedLevel;
                    if (StickCollected != null)
                    {
                        StickCollected.Invoke();
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
            m_game.endLevelEvent.AddListener(this.OnLevelEnd);
        }
        m_scene = scene;
        
    }

    void OnLevelStart()
    {
        Debug.Log("onlevelstart");
        m_board = Object.FindObjectOfType<Board>();
        m_player = Object.FindObjectOfType<PlayerMover>();
        m_enemies = new List<EnemyManager>(Object.FindObjectsOfType<EnemyManager>());
        m_collectedUI = Object.FindObjectOfType<AllCollectedUI>();
        if (m_collectedUI != null)
        {
            m_collectedUI.gameObject.SetActive(false);
        }
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
            m_sticks = m_board.AllSticks;
        }
        if (m_enemies != null)
        {
            foreach (EnemyManager e in m_enemies)
            {
                e.deathEvent.AddListener(this.OnEnemyKilled);
            }
        }
    }

    void OnLevelEnd()
    {
        if (SticksCollected == 2 && DiamondsCollected == 10)
        {
            if (m_collectedUI != null)
            {
                m_collectedUI.gameObject.SetActive(true);
            }
            if (AllCollected != null)
            {
                AllCollected.Invoke();
            }
        }
    }

    void OnLevelUnload(Scene scene)
    {
        m_board = null;
        m_player = null;
        m_diamonds = null;
        m_sticks = null;
        EnemiesKilled = 0;
        m_game = null;
        if (!restart)
        {
            m_totalDiamonds += DiamondsCollectedLevel;
            m_totalSticks += SticksCollectedLevel;
        }
        SticksCollectedLevel = 0;
        DiamondsCollectedLevel = 0;
        restart = false;
    }

    public void OnEnemyKilled()
    {
        ++EnemiesKilled;
    }

    public void RestartLevelStats()
    {
        restart = true;
    }
}
