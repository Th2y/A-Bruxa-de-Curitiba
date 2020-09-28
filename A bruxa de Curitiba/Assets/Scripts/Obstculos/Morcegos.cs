using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcegos : MonoBehaviour
{
    public float tempo;
    public Vector3[] minhasPosicoes;
    int posIndex = 0;
    int tamanho;
    float t = 0f;

    private void Start()
    {
        tamanho = minhasPosicoes.Length;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, minhasPosicoes[posIndex], tempo * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, tempo * Time.deltaTime);

        if (t > 0.9f)
        {
            t = 0f;
            posIndex++;

            posIndex = (posIndex >= tamanho) ? 0 : posIndex;
        }
    }
}
