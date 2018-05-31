using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    Button next;
    Button restart;
    Text labelStats;
    Text label;

    StatsManager statsManager;

    private void Awake()
    {
        statsManager = Object.FindObjectOfType<StatsManager>();
        next = transform.Find("RestartButton/NextButton").GetComponent<Button>();
        restart = transform.Find("RestartButton").GetComponent<Button>();
        labelStats = transform.Find("MissionCard/Border/Background/LabelStats").GetComponent<Text>();
        label = transform.Find("MissionCard/Border/Background/Label").GetComponent<Text>();
    }

    public void Win()
    {
        this.enabled = true;
        string text = string.Format("• {0} enemies killed\n\tCOLLECTED:\n• {1} diamonds ({2}/10)\n• {3} sticks ({4}/2)", statsManager.EnemiesKilled, statsManager.DiamondsCollectedLevel, statsManager.DiamondsCollected, statsManager.SticksCollectedLevel, statsManager.SticksCollected);
        labelStats.text = text;
    }

    public void Lose()
    {
        this.enabled = true;
        next.interactable = false;
        labelStats.enabled = false;
        label.text = "YOU\nLOST\n:c";
    }

}
