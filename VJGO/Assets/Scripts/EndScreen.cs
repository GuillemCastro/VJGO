using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    Button next;
    Button restart;
    Text label;

    StatsManager statsManager;

    private void Awake()
    {
        statsManager = Object.FindObjectOfType<StatsManager>().GetComponent<StatsManager>();
        next = transform.Find("RestartButton/NextButton").GetComponent<Button>();
        restart = transform.Find("RestartButton").GetComponent<Button>();
        label = transform.Find("MissionCard/Border/Background/LabelStats").GetComponent<Text>();
    }

    public void Win()
    {
        this.enabled = true;
        string text = string.Format("{0} enemies killed\n{1} diamonds collected", statsManager.EnemiesKilled, statsManager.DiamondsCollected);
        label.text = text;
    }

    public void Lose()
    {
        this.enabled = true;
        next.interactable = false;
        label.enabled = false;
        label.text = "YOU\nLOST\n:c";
    }

}
