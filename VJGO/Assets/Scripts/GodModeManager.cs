using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeManager : MonoBehaviour {

    bool created = false;

    bool m_godMode = false;

    public bool IsGodModeActive { get { return m_godMode; } }

    private void Awake()
    {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ToggleGodMode()
    {
        if (created)
        {
            m_godMode = !m_godMode;
            Debug.Log("GodMode" + m_godMode);
        }
    }
}
