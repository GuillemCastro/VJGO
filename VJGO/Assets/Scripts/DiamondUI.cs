using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondUI : MonoBehaviour {

    uint m_DiamondCount = 0;
    Text m_DiamondText;
    StatsManager statsManager;

    private void Awake()
    {
        m_DiamondText = GetComponentInChildren<Text>();
        statsManager = Object.FindObjectOfType<StatsManager>();
        if (statsManager != null)
        {
            statsManager.DiamondCollected.AddListener(this.AddDiamondCount);
        }
    }

    private void Start()
    {
        if (statsManager != null)
        {
            m_DiamondCount = statsManager.DiamondsCollected;
        }
        string text = string.Format("{0}/10", m_DiamondCount);
        m_DiamondText.text = text;
    }

    public void AddDiamondCount()
    {
        m_DiamondCount = statsManager.DiamondsCollected;
        string text = string.Format("{0}/10", m_DiamondCount);
        m_DiamondText.text = text;
    }
}
