using UnityEngine;

public class ChangeLane : MonoBehaviour
{
    public void PositionLane()
    {
        transform.position = new Vector3(Random.Range(-1, 2), transform.position.y, transform.position.z);
    }
}
