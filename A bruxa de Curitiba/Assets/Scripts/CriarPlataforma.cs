using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarPlataforma : MonoBehaviour
{
    public List<GameObject> plataformas = new List<GameObject>();
    public List<Transform> plataformaAtual = new List<Transform>();

    public int distanciaPlataformas;

    private Transform player;
    private Transform atualPlataformaPonto;
    private int plataformaIndex;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for(int i = 0; i < plataformas.Count; i++)
        {
            Transform p = Instantiate(plataformas[i], new Vector3(0, 0, i * 86), transform.rotation).transform;
            plataformaAtual.Add(p);
            distanciaPlataformas += 86;
        }

        atualPlataformaPonto = plataformaAtual[plataformaIndex].GetComponent<Plataforma>().ponto;
    }

    void Update()
    {
        float distancia = player.position.z - atualPlataformaPonto.position.z;

        if(distancia >= 5)
        {
            Reciclar(plataformaAtual[plataformaIndex].gameObject);
            plataformaIndex++;

            if (plataformaIndex > plataformaAtual.Count - 1)
                plataformaIndex = 0;


            atualPlataformaPonto = plataformaAtual[plataformaIndex].GetComponent<Plataforma>().ponto;
        }
    }

    public void Reciclar(GameObject plataforma)
    {
        plataforma.transform.position = new Vector3(0, 0, distanciaPlataformas);
        distanciaPlataformas += 86;
    }
}
