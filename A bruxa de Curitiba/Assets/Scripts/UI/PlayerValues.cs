using UnityEngine;
using TMPro;

public class PlayerValues : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Start()
    {
        GetCoins();
        GetScore();
    }

    private void GetScore()
    {
        pointsText.text = playerSO.BestScore.ToString();
    }

    private void GetCoins()
    {
        coinsText.text = playerSO.TotalNumberOfCoins.ToString();
    }
}
