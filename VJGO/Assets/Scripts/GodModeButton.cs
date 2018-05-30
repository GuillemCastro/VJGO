using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodModeButton : MonoBehaviour {

    Text m_godModeText;
    GodModeManager godMode;

    bool m_status = false;
     
    private void Awake()
    {
        m_godModeText = GetComponentInChildren<Text>();
        godMode = Object.FindObjectOfType<GodModeManager>().GetComponent<GodModeManager>();
    }

    private void Start()
    {
        if (godMode != null)
        {
            m_status = godMode.IsGodModeActive;
        }
        UpdateText();
    }

    public void Toggle()
    {
        m_status = !m_status;
        if (godMode != null && godMode.IsGodModeActive != m_status)
        {
            m_status = godMode.IsGodModeActive;
        }
        UpdateText();
    }

    void UpdateText()
    {
        if (m_godModeText != null)
        {
            string on_off = m_status ? "ON" : "OFF";
            m_godModeText.text = string.Format("God Mode: {0}", on_off);
        }
    }
}
