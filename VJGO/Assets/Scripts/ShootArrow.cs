using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {
    public GameObject arrow;
    public bool isMoving = false;
    public float iTweenDelay = 0.2f;
    public float iTweenTime = 0.5f;
    public iTween.EaseType iTweenEaseType = iTween.EaseType.easeInOutExpo;

    AudioSource audioSource;

    void Awake()
    {
        arrow.GetComponentInChildren<Renderer>().enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void ShootArrowAt (Vector3 destination)
    {
        StartCoroutine(ShootArrowAtRoutine(destination));

    }

    IEnumerator ShootArrowAtRoutine(Vector3 destination)
    {
        arrow.GetComponentInChildren<Renderer>().enabled = true;
        isMoving = true;
        iTween.MoveTo(arrow, iTween.Hash(
            "x", destination.x,
            "y", arrow.transform.position.y,
            "z", destination.z,
            "delay", iTweenDelay,
            "easetype", iTweenEaseType,
            "time", iTweenTime
            ));
        if (audioSource != null)
        {
            audioSource.Play();
        }

        yield return new WaitForSeconds(iTweenTime);

        arrow.GetComponentInChildren<Renderer>().enabled = false;
        isMoving = false;

    }
}
