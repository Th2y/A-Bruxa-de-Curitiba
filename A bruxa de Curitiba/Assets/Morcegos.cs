using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcegos : MonoBehaviour
{
    private float tempo = 0;

    void Update()
    {
        tempo += Time.deltaTime;

        if(tempo <= 0.5f)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        else
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            Invoke("ZerarTempo", 0.5f);
        }
    }

    void ZerarTempo()
    {
        tempo = 0;
    }
}
