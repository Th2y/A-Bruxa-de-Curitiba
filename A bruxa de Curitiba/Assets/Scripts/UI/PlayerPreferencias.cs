using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPreferencias : MonoBehaviour
{
    public TextMeshProUGUI moedas;
    public TextMeshProUGUI pontuacao;
    int pontos;

    void Start()
    {
        PegarMoedas();
        PegarPontuacao();
    }

    public void PegarPontuacao()
    {
        pontos = (int)PlayerPrefs.GetFloat("Pontuacao");
        pontuacao.text = pontos.ToString();
    }

    public void PegarMoedas()
    {
        moedas.text = PlayerPrefs.GetInt("MoedasGanhas").ToString();
    }
}
