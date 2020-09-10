using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    private Transform player;
    private Vector3 distancia;

    //Apenas para "brincar" com a cor de fundo
    private float tempo;
    public Camera cam;
    public Color escuro;
    public Color claro;
    public Color medio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        distancia = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 novaPosicao = new Vector3(transform.position.x, transform.position.y, player.position.z + distancia.z);
        transform.position = novaPosicao;

        //Apenas para "brincar" com a cor de fundo
        tempo += Time.deltaTime;
        if (tempo <= 10f)
            cam.gameObject.GetComponent<Camera>().backgroundColor = claro;
        else if (tempo <= 20f)
            cam.gameObject.GetComponent<Camera>().backgroundColor = medio;
        else if(tempo < 30f)
            cam.gameObject.GetComponent<Camera>().backgroundColor = escuro;   
        else if (tempo >= 30f)
            tempo = 0f;
    }
}
