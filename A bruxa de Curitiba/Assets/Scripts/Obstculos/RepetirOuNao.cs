using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepetirOuNao : MonoBehaviour
{
    public GameObject[] moedas;
    public GameObject fim;
    int num = 1;

    public GameObject pausarNoFim;

    public static RepetirOuNao instancia;
    public GameObject player;

    private void Start()
    {
        instancia = this;
    }

    public void RepetirSim()
    {
        for(int i = 0; i < moedas.Length; i++)
        {
            moedas[i].transform.position = new Vector3(moedas[i].transform.position.x, moedas[i].transform.position.y, moedas[i].transform.position.z + 254*num);
        }
        fim.transform.position = new Vector3(fim.transform.position.x, fim.transform.position.y, fim.transform.position.z + 254*num);

        player.GetComponent<Player>().AumentarVelocidade();
    }

    public void RepetirNao()
    {
        //Provisorio, depois quando ele acabar, deverá acionar uma animação e pausar todo o jogo
        pausarNoFim.SetActive(false);
        Invoke("PassarFase", 2f);
    }

    void PassarFase()
    {
        if(SceneManager.GetActiveScene().name == "Fase1")
            GameManager.instancia.IrFase2();
        else if(SceneManager.GetActiveScene().name == "Fase2")
            GameManager.instancia.IrFase3();
    }
}
