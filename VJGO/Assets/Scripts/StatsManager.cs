using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatsManager : MonoBehaviour {

    public UnityEvent DiamondCollected;
    public uint DiamondsCollected = 0;

    Board m_board;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }

    public void CheckCollectibles()
    {
        List<Diamond> diamonds = m_board.AllDiamonds.FindAll(d => d.Coordinate == m_board.PlayerNode.Coordinate);
        foreach(Diamond diamond in diamonds) {
            diamond.Collect();
            ++DiamondsCollected;
            if (DiamondCollected != null)
            {
                DiamondCollected.Invoke();
            }
        }
    }

}
