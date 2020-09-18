using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    private void Awake()
    {
        if (instancia == null)
            instancia = this;
        else if (instancia != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        instancia = this;
    }

    void Update()
    {
        
    }

    public void IniciarCorrida()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
