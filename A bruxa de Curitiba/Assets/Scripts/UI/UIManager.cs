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

    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdatePoints(int pontos)
    {
        pointsText.text = pontos.ToString();
    }
}
