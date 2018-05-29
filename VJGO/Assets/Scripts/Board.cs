using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // uniform distance between nodes
    public static float spacing = 2f;

    // four compass directions
    public static readonly Vector2[] directions =
    {
        new Vector2(spacing, 0f),
        new Vector2(-spacing, 0f),
        new Vector2(0f, spacing),
        new Vector2(0f, -spacing)
    };

    List<Node> m_allNodes = new List<Node>();
    public List<Node> AllNodes { get { return m_allNodes; } }

    List<Door> m_allDoors = new List<Door>();
    public List<Door> AllDoors { get { return m_allDoors; } }

    List<Diamond> m_allDiamonds = new List<Diamond>();
    public List<Diamond> AllDiamonds { get { return m_allDiamonds; } }

    List<Piston> m_allPistons = new List<Piston>();
    public List<Piston> AllPistons { get { return m_allPistons; } }

    Node m_playerNode;
    public Node PlayerNode { get { return m_playerNode; } }

    Node m_goalNode;
    public Node GoalNode { get { return m_goalNode; } }

    public GameObject goalPrefab;
    public float drawGoalTime = 2f;
    public float drawGoalDelay = 2f;
    public iTween.EaseType drawGaolEaseType = iTween.EaseType.easeOutExpo;

    PlayerMover m_player;

    public List<Transform> capturePositions;
    int m_currentCapturePosition = 0;

    public int CurrentCapturePosition { get { return m_currentCapturePosition;}
        set { m_currentCapturePosition = value; } }

    public float capturePositionIconSize = 0.4f;
    public Color capturePositionIconColor = Color.green;

    void Awake()
    {
        m_player = Object.FindObjectOfType<PlayerMover>().GetComponent<PlayerMover>();
        GetNodeList();
        GetDoorList();
        GetDiamondList();
        m_goalNode = FindGoalNode();
    }

    public void GetNodeList()
    {
        Node[] nList = GameObject.FindObjectsOfType<Node>();
        m_allNodes = new List<Node>(nList);
    }

    public void GetDoorList()
    {
        Door[] nList = GameObject.FindObjectsOfType<Door>();
        m_allDoors = new List<Door>(nList);
    }

    public void GetDiamondList()
    {
        Diamond[] nList = GameObject.FindObjectsOfType<Diamond>();
        m_allDiamonds = new List<Diamond>(nList);
    }

    public void GetPistonList()
    {
        Piston[] nList = GameObject.FindObjectsOfType<Piston>();
        m_allPistons = new List<Piston>(nList);
    }

    public Node FindNodeAt(Vector3 pos)
    {

        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return m_allNodes.Find(n => n.Coordinate == boardCoord);
    }

    public Door FindDoorAt(Vector3 pos)
    {
        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return m_allDoors.Find(n => n.Coordinate == boardCoord);
    }

    public Diamond FindDiamondAt(Vector3 pos)
    {
        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return m_allDiamonds.Find(n => n.Coordinate == boardCoord);
    }

    public Piston FindPistonAt(Vector3 pos)
    {
        Vector2 boardCoord = Utility.Vector2Round(new Vector2(pos.x, pos.z));
        return m_allPistons.Find(p => p.Coordinate == boardCoord);
    }

    public Node FindGoalNode()
    {
        return m_allNodes.Find(n => n.isLevelGoal);
    }

    Node FindPlayerNode()
    {
        if (m_player != null && !m_player.isMoving)
        {
            return FindNodeAt(m_player.transform.position);
        }
        return null;
    }

    public List<EnemyManager> FindEnemiesAt(Node node)
    {
        List<EnemyManager> foundEnemies = new List<EnemyManager>();
        EnemyManager[] enemies = Object.FindObjectsOfType<EnemyManager>() as EnemyManager[];

        foreach (EnemyManager enemy in enemies)
        {
            EnemyMover mover = enemy.GetComponent<EnemyMover>();

            if (mover.CurrentNode == node)
            {
                foundEnemies.Add(enemy);
            }
        }
        return foundEnemies;
    }

    public void UpdatePlayerNode()
    {
        m_playerNode = FindPlayerNode();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
        if (m_playerNode != null)
        {
            Gizmos.DrawSphere(m_playerNode.transform.position, .2f);
        }

        Gizmos.color = capturePositionIconColor;

        foreach (Transform capturePos in capturePositions)
        {
            Gizmos.DrawCube(capturePos.position, Vector3.one * capturePositionIconSize);
        }
    }

    public void DrawGoal()
    {
        if (goalPrefab != null && m_goalNode != null)
        {
            GameObject goalInstance = Instantiate(goalPrefab, m_goalNode.transform.position, Quaternion.identity);
            iTween.ScaleFrom(goalInstance, iTween.Hash(
                "scale", Vector3.zero,
                "time", drawGoalTime,
                "delay", drawGoalDelay,
                "easetype", drawGaolEaseType
                ));
        }
    }

    public void InitBoard()
    {
        if (PlayerNode != null)
        {
           PlayerNode.InitNode();
        }
    }
}
