using UnityEngine;

public class Bats : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Vector3[] myPositions;

    private int posIndex = 0;
    private int size;
    private float t = 0f;

    private void Start()
    {
        size = myPositions.Length;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, myPositions[posIndex], time * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, time * Time.deltaTime);

        if (t > 0.9f)
        {
            t = 0f;
            posIndex++;

            posIndex = (posIndex >= size) ? 0 : posIndex;
        }
    }
}
