using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    Vector2 m_coordinate;
    public Vector2 Coordinate { get { return Utility.Vector2Round(m_coordinate); } }

    Board m_board;

    Hinge m_hinge;

    public bool IsOpen {
        get
        {
            if (m_hinge != null)
            {
                return m_hinge.IsOpen;
            }
            return false;
        }
    }

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>();
        m_coordinate = new Vector2(transform.position.x, transform.position.z);
        m_hinge = gameObject.GetComponentInChildren<Hinge>();
    }

    public void Open()
    {
        if (m_hinge != null)
        {
            m_hinge.Open();
        }
    }

    public void Close()
    {
        if (m_hinge != null)
        {
            m_hinge.Close();
        }
    }

}
