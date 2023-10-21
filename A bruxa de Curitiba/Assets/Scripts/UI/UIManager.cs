using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOver;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Start()
    {
        gameOver.SetActive(false);
    }

    public void UpdateCoins()
    {
        coinsText.text = PlayerPrefs.GetInt(Constants.CoinsCurrentRunPref).ToString();
    }

    public void UpdatePoints(int pontos)
    {
        pointsText.text = pontos.ToString();
    }
}
