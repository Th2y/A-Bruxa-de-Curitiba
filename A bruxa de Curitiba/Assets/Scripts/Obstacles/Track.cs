using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Vector2 numeroDeObstaculos;
    public GameObject moeda;
    public Vector2 numeroDeMoedas;

    public List<GameObject> novosObstaculos;
    public List<GameObject> novasMoedas;

    void Start()
    {
        int novoNumeroDeObstaculos = (int)Random.Range(numeroDeObstaculos.x, numeroDeObstaculos.y);
        int novoNumeroDeMoedas = (int)Random.Range(numeroDeMoedas.x, numeroDeMoedas.y);

        for (int i = 0; i < novoNumeroDeObstaculos; i++)
        {
            novosObstaculos.Add(Instantiate(obstaculos[Random.Range(0, obstaculos.Length)], transform));
            novosObstaculos[i].SetActive(false);
        }
        for (int i = 0; i < novoNumeroDeMoedas; i++)
        {
            novasMoedas.Add(Instantiate(moeda, transform));
            novasMoedas[i].SetActive(false);
        }

        PosicionarObstaculos();
        PosicionarMoedas();
    }

    void PosicionarObstaculos()
    {
        for (int i = 0; i < novosObstaculos.Count; i++)
        {
            float posZMin = (297f / novosObstaculos.Count) + (297f / novosObstaculos.Count) * i;
            float posZMax = (297f / novosObstaculos.Count) + (297f / novosObstaculos.Count) * i + 1;
            novosObstaculos[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
            novosObstaculos[i].SetActive(true);
            if (novosObstaculos[i].GetComponent<ChangeLane>() != null)
                novosObstaculos[i].GetComponent<ChangeLane>().PositionLane();
        }
    }

    void PosicionarMoedas()
    {
        float minZPos = 10f;

        for (int i = 0; i < novasMoedas.Count; i++)
        {
            float maxZPos = minZPos + 5f;
            float randomZPos = Random.Range(minZPos, maxZPos);
            novasMoedas[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZPos);
            novasMoedas[i].SetActive(true);
            novasMoedas[i].GetComponent<ChangeLane>().PositionLane();
            minZPos = randomZPos + 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().IncreaseSpeed();
            transform.position = new Vector3(0, 0, transform.position.z + 297 * 2);
            PosicionarObstaculos();
            PosicionarMoedas();
        }
    }
}
