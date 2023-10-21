using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    [SerializeField] private float transitionDelay;
    [SerializeField] private Animator fadeAnimator;

    public void LoadScene(string scene)
    {
        if(scene == Constants.MenuScene) SceneManager.LoadScene(scene);
        else StartCoroutine(LoadLevel(scene));
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
