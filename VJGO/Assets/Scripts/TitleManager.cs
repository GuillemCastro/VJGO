using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour {

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
