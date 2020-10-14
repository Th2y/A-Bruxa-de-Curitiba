using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool isPaused;
    public float transitionDelay;
    public Animator fadeAnimator;

    public void LoadScene(string scene)
    {
        //SceneManager.LoadScene(scene);
        StartCoroutine(LoadLevel(scene));
    }

    public void PauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }

    }

    IEnumerator LoadLevel(string scene)
    {
        fadeAnimator.SetTrigger("StartFade");
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(scene);
    }
}
