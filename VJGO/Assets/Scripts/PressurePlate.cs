using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

    public UnityEvent activated;
    Board m_board;
    Node m_node;

    bool m_activated = false;
    public bool IsActivated { get { return m_activated; } }

    public float delay = 1f;
    public float time = 0.5f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    AudioSource m_audioSource;

    private void Awake()
    {
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        m_audioSource = GetComponent<AudioSource>();
    }

    void Start () {
        m_node = m_board.FindNodeAt(Utility.Vector3Round(transform.position));
	}
	
	void Update () {
		if (m_node != null && m_board != null)
        {

            if (!m_activated && m_node == m_board.PlayerNode)
            {

                iTween.ScaleTo(gameObject, iTween.Hash(
                    "y", 0.01,
                    "time", time,
                    "delay", delay,
                    "easetype", easeType
                    ));

                if (m_audioSource != null)
                {
                    m_audioSource.PlayDelayed(delay);
                }

                m_activated = true;
                if (activated != null)
                {
                    activated.Invoke();
                }
            }

        }
	}
}
