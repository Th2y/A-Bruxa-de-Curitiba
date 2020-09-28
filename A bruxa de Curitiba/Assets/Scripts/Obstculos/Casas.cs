using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casas : MonoBehaviour
{
    public Texture[] texturas;
    public Material materialObjeto;

    void Start()
    {
        materialObjeto.SetTexture("_MainTex",texturas[0]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            materialObjeto.SetTexture("_MainTex", texturas[1]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            materialObjeto.SetTexture("_MainTex", texturas[0]);
        }
    }
}
