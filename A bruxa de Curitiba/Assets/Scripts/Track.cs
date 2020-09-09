using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Vector2 numeroDeObstaculos;

    public List<GameObject> novosObstaculos;

    void Start()
    {
        int novoNumeroDeObstaculos = (int)Random.Range(numeroDeObstaculos.x, numeroDeObstaculos.y);

        for (int i = 0; i < novoNumeroDeObstaculos; i++)
        {
            novosObstaculos.Add(Instantiate(obstaculos[Random.Range(0, obstaculos.Length)], transform));
            novosObstaculos[i].SetActive(false);
        }

        PosicionarObstaculos();
    }

    void PosicionarObstaculos()
    {
        for (int i = 0; i < novosObstaculos.Count; i++)
        {
            float posZMin = (297f / novosObstaculos.Count) + (297f / novosObstaculos.Count) * i;
            float posZMax = (297f / novosObstaculos.Count) + (297f / novosObstaculos.Count) * i + 1;
            novosObstaculos[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
            novosObstaculos[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.position = new Vector3(0, 0, transform.position.z + 297 * 2);
            Invoke("PosicionarObstaculos", 5f);
        }
    }
}
