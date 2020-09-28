﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text moedas;
    public Text pontuacao;
    int pontos;

    void Start()
    {
        PegarMoedas();
        PegarPontuacao();
    }

    void Update()
    {
        
    }

    public void IniciarHistoria()
    {
        GameManager.instancia.IrFase1();
    }

    public void IniciarInfinito()
    {
        GameManager.instancia.IrFase3();
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