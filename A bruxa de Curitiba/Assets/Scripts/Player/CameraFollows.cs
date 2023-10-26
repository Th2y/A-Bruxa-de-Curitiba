using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    private Transform player;
    private Vector3 distance;

    private void Start()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Transform>();
        distance = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = new(transform.position.x, transform.position.y, player.position.z + distance.z);
        transform.position = newPosition;
    }
}
