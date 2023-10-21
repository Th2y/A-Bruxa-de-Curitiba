using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOver;
    public Text moedaTexto;
    public Text pontosTexto;

    void Start()
    {
        gameOver.SetActive(false);
    }

    public void UpdateCoins(int moedas)
    {
        moedaTexto.text = PlayerPrefs.GetInt("MoedasCorridaAtual").ToString();
    }

    public void UpdatePoints(int pontos)
    {
        pontosTexto.text = pontos.ToString();
    }
}
