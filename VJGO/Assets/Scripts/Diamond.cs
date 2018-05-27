using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    public float delay = 0f;
    public float time = 0.5f;
    public float movement = -0.3f;

    public iTween.EaseType easeType = iTween.EaseType.linear;
    public iTween.LoopType loopType = iTween.LoopType.pingPong;

    public Vector2 Coordinate;

    AudioSource audio;

    private void Awake()
    {
        Coordinate = new Vector2(transform.position.x, transform.position.z);
        audio = GetComponent<AudioSource>();
    }

    void Start () {

        iTween.MoveBy(gameObject, iTween.Hash(
            "y", movement,
            "delay", delay,
            "easetype", easeType,
            "looptype", loopType,
            "time", time
            ));

	}

    public void Collect()
    {
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        StartCoroutine(CollectRoutine());
    }

    IEnumerator CollectRoutine()
    {
        PlaySound();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void PlaySound()
    {
        if (audio != null)
        {
            audio.Play();
        }
    }

}
