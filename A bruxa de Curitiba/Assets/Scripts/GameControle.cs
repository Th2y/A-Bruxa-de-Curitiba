using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControle : MonoBehaviour
{
    public GameObject painelFimDeJogo;

    public Text pontucaoTexto;
    public float pontuacao;
    public int moedasPegas;
    public Text moedasTexto;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (!player.estaMorto)
        {
            pontuacao += Time.deltaTime * 10;
            pontucaoTexto.text = Mathf.Round(pontuacao).ToString();
        }
    }

    public void MostrarFimDeJogo()
    {
        painelFimDeJogo.SetActive(true);
    }

    public void AddMoedas()
    {
        moedasPegas++;
        moedasTexto.text = moedasPegas.ToString();
    }
}
