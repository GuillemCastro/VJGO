using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Stationary,
    Patrol,
    Spinner,
    Ranged
}

public class EnemySensor : MonoBehaviour {
    public Vector3 directionToSearch = new Vector3(0f, 0f, 2f);

    Node m_nodeToSearch;
    Node m_currentNode;
    Board m_board;

    public EnemyType enemyType = EnemyType.Stationary;

    bool m_foundPlayer = false;
    public bool FoundPlayer {  get { return m_foundPlayer; } }

	void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }
	
	// Update is called once per frame
	public void UpdateSensor() {
        Vector3 worldSpacePositionToSearch = transform.InverseTransformVector(directionToSearch) + transform.position;

        if (m_board != null)
        {
           if (enemyType == EnemyType.Ranged)
            {
                for (int i = 0; i < 3; ++i)
                {
                    m_nodeToSearch = m_board.FindNodeAt(worldSpacePositionToSearch);
                    m_currentNode = m_board.FindNodeAt(transform.position);
                    if (m_nodeToSearch == m_board.PlayerNode && m_currentNode.LinkedNodes.Contains(m_nodeToSearch))
                    {
                        m_foundPlayer = true;
                    }
                    worldSpacePositionToSearch += transform.InverseTransformVector(directionToSearch);
                }
            }
            else
            {
                m_nodeToSearch = m_board.FindNodeAt(worldSpacePositionToSearch);
                m_currentNode = m_board.FindNodeAt(transform.position);
                if (m_nodeToSearch == m_board.PlayerNode && m_currentNode.LinkedNodes.Contains(m_nodeToSearch))
                {
                    m_foundPlayer = true;
                }
            }
        }
	}
}
