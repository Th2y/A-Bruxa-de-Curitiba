using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private bool isPaused;
    [SerializeField] private float transitionDelay;
    [SerializeField] private Animator fadeAnimator;

    private static readonly string HistoryMission = "PlayHistoryMode";
    private static readonly string InfinityMission = "PlayInfinityMode";

    public void ChooseLevelModeAndPlay(bool isInfinityMode)
    {
        ActualMode.Instance.IsInfinityMode = isInfinityMode;

        if (isInfinityMode )
        {
            foreach(MissionSO missionSO in playerSO.Missions)
            {
                if(missionSO.Name == InfinityMission)
                {
                    missionSO.Completed = true;
                    break;
                }
            }
        }
        else 
        {
            foreach (MissionSO missionSO in playerSO.Missions)
            {
                if (missionSO.Name == HistoryMission)
                {
                    missionSO.Completed = true;
                    break;
                }
            }
        }

        StartCoroutine(LoadLevel(isInfinityMode ? "Level" : "Cutscenes"));
    }

    public void LoadScene(string scene)
    {
        if (scene == "MainMenu") 
        {
            GiveRewards();
            ActualMode.Instance.ActualLevel = 0;
            SceneManager.LoadScene(scene); 
        }
        else if(scene == "Cutscenes")
        {
            StartCoroutine(LoadLevel(scene));
        }
        else
        {
            GiveRewards();
            StartCoroutine(LoadLevel(scene));
        }
    }

    private void GiveRewards()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null && !player.RewardsReceived)
        {
            player.RewardsReceived = true;
            playerSO.TotalNumberOfCoins += player.Coins;
            if (player.BestScore > playerSO.BestScore) playerSO.BestScore = (int)player.BestScore;
        }
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
