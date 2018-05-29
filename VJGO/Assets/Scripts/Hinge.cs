using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge : MonoBehaviour {

    public float delay = 1f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    public float time = 2f;

    AudioSource audioSource;

    bool m_open = false;

    public bool IsOpen { get { return m_open; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //StartCoroutine(TestRoutine());
    }

    public void Open()
    {
        StartCoroutine(OpenRoutine());
    }

    IEnumerator OpenRoutine()
    {
        iTween.RotateAdd(gameObject, iTween.Hash(
            "y", -90,
            "delay", delay,
            "easetype", easeType,
            "time", time
            ));
        if (audioSource != null)
        {
            Debug.Log("PLAY SOUND");
            audioSource.PlayDelayed(delay);
        }
        yield return new WaitForSeconds(delay);
        m_open = true;
    }

    public void Close()
    {
        m_open = false;
        iTween.RotateAdd(gameObject, iTween.Hash(
            "y", 90,
            "delay", delay,
            "easetype", easeType,
            "time", time
            ));

    }

    IEnumerator TestRoutine()
    {
        yield return new WaitForSeconds(5f);
        Open();
        yield return new WaitForSeconds(2f);
        //Close();
    }
}
