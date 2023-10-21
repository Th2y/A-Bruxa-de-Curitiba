using UnityEngine;
using TMPro;

public class PlayerValues : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI pointsText;

    private int points;

    private void Start()
    {
        GetCoins();
        GetScore();
    }

    private void GetScore()
    {
        points = (int)PlayerPrefs.GetFloat(Constants.ScorePref);
        pointsText.text = points.ToString();
    }

    private void GetCoins()
    {
        coinsText.text = PlayerPrefs.GetInt(Constants.EarnedCoinsPref).ToString();
    }
}
