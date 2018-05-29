using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : RedstoneActivable {

    protected bool m_isDown;
    public bool IsDown { get { return m_isDown; } }

    public float delay = 1f;
    public float time = 0.1f;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;

    protected AudioSource audioSource;

    public Vector2 Coordinate;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    private void Start()
    {
        //StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(2f);
        Activate();
    }

    public override void Activate()
    {
        if (m_isDown)
        {
            return;
        }
        StartCoroutine(ActivateRoutine());
    }

    IEnumerator ActivateRoutine()
    {
        yield return new WaitForSeconds(delay);
        iTween.ScaleTo(gameObject, iTween.Hash(
            "y", 0.01f,
            "time", time,
            "delay", 0,
            "easetype", easeType
            ));

        if (audioSource != null)
        {
            audioSource.Play();
        }
        m_isDown = true;
    }


}
