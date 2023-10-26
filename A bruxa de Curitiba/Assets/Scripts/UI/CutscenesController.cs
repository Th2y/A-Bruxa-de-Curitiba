using UnityEngine;
using UnityEngine.Video;

public class CutscenesController : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private GameObject[] rawImages;
    [SerializeField] private VideoPlayer[] videoPlayers;

    private void Awake()
    {
        for (int i = 0; i < rawImages.Length; i++)
        {
            if (i == ActualMode.Instance.ActualLevel)
            {
                rawImages[i].SetActive(true);
                videoPlayers[i].gameObject.SetActive(true);
                videoPlayers[i].loopPointReached += OnVideoFinished;
                videoPlayers[i].Play();
            }
            else
            {
                rawImages[i].SetActive(false);
                videoPlayers[i].gameObject.SetActive(false);
            }
        }

        ActualMode.Instance.ActualLevel++;

        string missionName = "Watch" + ActualMode.Instance.ActualLevel + "Cutscene";

        foreach (MissionSO missionSO in playerSO.Missions)
        {
            if (missionSO.Name == missionName)
            {
                missionSO.Completed = true;
                break;
            }
        }
    }

    public void LoadLevel()
    {
        MenuController menuController = FindObjectOfType<MenuController>();
        if (menuController != null) menuController.LoadScene("Level");
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        LoadLevel();
    }
}
