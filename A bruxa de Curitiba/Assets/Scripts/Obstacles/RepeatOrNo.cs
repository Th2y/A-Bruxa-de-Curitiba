using UnityEngine;

public class RepeatOrNo : MonoBehaviour
{
    [SerializeField] private GameObject endGameDetector;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Player player;

    private int num = 1;

    public static RepeatOrNo Instance;

    private void Start()
    {
        Instance = this;
    }

    public void RepeatYes()
    {
        endGameDetector.transform.position = 
            new Vector3(endGameDetector.transform.position.x, endGameDetector.transform.position.y, endGameDetector.transform.position.z + 254 * num);

        player.IncreaseSpeed();
    }

    public void RepeatNo()
    {
        endGamePanel.SetActive(true);
    }
}
