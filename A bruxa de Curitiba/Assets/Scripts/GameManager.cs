using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
            gm = this;
        else if (gm != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
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
