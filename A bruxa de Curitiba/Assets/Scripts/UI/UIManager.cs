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

    public void AtualizarMoedas(int moedas)
    {
        moedaTexto.text = moedas.ToString();
    }

    public void AtualizarPontos(int pontos)
    {
        pontosTexto.text = pontos.ToString();
    }
}
