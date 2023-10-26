using UnityEngine;

public class RepeatOrNo : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private GameObject endGameDetector;
    [SerializeField] private GameObject finishedGamePanel;
    [SerializeField] private Player player;

    public static RepeatOrNo Instance;

    private void Start()
    {
        Instance = this;
    }

    public void RepeatYes()
    {
        endGameDetector.transform.position = 
            new Vector3(endGameDetector.transform.position.x, endGameDetector.transform.position.y, endGameDetector.transform.position.z + 254);

        player.IncreaseSpeed();
    }

    public void RepeatNo()
    {
        ActualMode.Instance.ActualCoins = player.Coins;
        ActualMode.Instance.ActualPoints = (int)player.BestScore;

        if (ActualMode.Instance.ActualLevel < 4)
        {
            MenuController menu = FindObjectOfType<MenuController>();
            menu.LoadScene("Cutscenes");
        }
        else
        {
            finishedGamePanel.SetActive(true);
        }
    }
}
