using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondUI : MonoBehaviour {

    uint m_DiamondCount = 0;
    uint m_StickCount = 0;
    Text m_DiamondText;
    Text m_StickText;
    StatsManager statsManager;

    private void Awake()
    {
        m_DiamondText = transform.Find("DiamondText").GetComponent<Text>();
        m_StickText = transform.Find("StickText").GetComponent<Text>();
        statsManager = Object.FindObjectOfType<StatsManager>();
        if (statsManager != null)
        {
            statsManager.DiamondCollected.AddListener(this.AddDiamondCount);
            statsManager.StickCollected.AddListener(this.AddStickCount);
        }
    }

    private void Start()
    {
        if (statsManager != null)
        {
            m_DiamondCount = statsManager.DiamondsCollected;
            m_StickCount = statsManager.SticksCollected;
        }
        string text = string.Format("{0}/10", m_DiamondCount);
        m_DiamondText.text = text;

        text = string.Format("{0}/2", m_StickCount);
        m_StickText.text = text;
    }

    public void AddDiamondCount()
    {
        m_DiamondCount = statsManager.DiamondsCollected;
        string text = string.Format("{0}/10", m_DiamondCount);
        m_DiamondText.text = text;
    }

    public void AddStickCount()
    {
        m_StickCount = statsManager.SticksCollected;
        string text = string.Format("{0}/2", m_StickCount);
        m_StickText.text = text;
    }
}
