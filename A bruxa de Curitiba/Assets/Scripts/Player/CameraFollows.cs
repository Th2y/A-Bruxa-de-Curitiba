using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Color dark;
    [SerializeField] private Color clear;
    [SerializeField] private Color medium;
    [SerializeField] private Transform player;

    private Vector3 distance;
    private float time;

    private void Start()
    {
        distance = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, player.position.z + distance.z);
        transform.position = newPosition;

        time += Time.deltaTime;

        if (time <= 10f)
            cam.gameObject.GetComponent<Camera>().backgroundColor = clear;
        else if (time <= 20f)
            cam.gameObject.GetComponent<Camera>().backgroundColor = medium;
        else if (time < 30f)
            cam.gameObject.GetComponent<Camera>().backgroundColor = dark;
        else if (time >= 30f)
            time = 0f;
    }
}
