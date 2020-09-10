using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudarLane : MonoBehaviour
{
    public void PosicionarLane()
    {
        int randomLane = Random.Range(-1, 2);
        transform.position = new Vector3(randomLane, transform.position.y, transform.position.z);
    }
}
