using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {
    public Animator playerAnimController;

    public string playerDeathTrigger = "IsDead";

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Die()
    {
        if (playerAnimController != null)
        {
            playerAnimController.SetTrigger(playerDeathTrigger);
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
