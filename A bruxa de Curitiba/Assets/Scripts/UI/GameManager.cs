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

    public void IrFase1()
    {
        SceneManager.LoadScene("Fase1");
    }

    public void IrFase2()
    {
        SceneManager.LoadScene("Fase2");
    }

    public void IrFase3()
    {
        SceneManager.LoadScene("Fase3");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
