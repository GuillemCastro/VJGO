using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondUI : MonoBehaviour {

    uint m_DiamondCount = 0;
    Text m_DiamondText;
    

    private void Awake()
    {
        m_DiamondText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        string text = string.Format("x {0}", m_DiamondCount);
        m_DiamondText.text = text;
    }

    public void AddDiamondCount()
    {
        ++m_DiamondCount;
        string text = string.Format("x {0}", m_DiamondCount);
        m_DiamondText.text = text;
    }
}
