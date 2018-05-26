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
        StartCoroutine(LoadGameRoutine());
    }

    IEnumerator LoadGameRoutine()
    {
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadScene(1);
    }
}
