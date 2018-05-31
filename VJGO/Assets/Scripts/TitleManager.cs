using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class TitleManager : MonoBehaviour {

    public UnityEvent startEvent;

    private void Start()
    {
        if (startEvent != null)
        {
            startEvent.Invoke();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutine(1));
    }

    IEnumerator LoadGameRoutine(int index)
    {
        Debug.Log("Index" + index);
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene(index);
    }

    public void LoadLevel1()
    {
        StartCoroutine(LoadGameRoutine(1));
    }

    public void LoadLevel2()
    {
        StartCoroutine(LoadGameRoutine(2));
    }

    public void LoadLevel3()
    {
        StartCoroutine(LoadGameRoutine(3));
    }

    public void LoadLevel4()
    {
        StartCoroutine(LoadGameRoutine(4));
    }

    public void LoadLevel5()
    {
        StartCoroutine(LoadGameRoutine(5));
    }
}
