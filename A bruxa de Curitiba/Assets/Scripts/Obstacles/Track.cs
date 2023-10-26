using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject coinPrefab;

    private bool isInfinityMode;
    private DifficultySO difficulty;

    private readonly List<GameObject> _newCoins = new();

    public void Start()
    {
        isInfinityMode = ActualMode.Instance.IsInfinityMode;

        difficulty = playerSO.DifficultiesDict[playerSO.ActualDifficulty];

        int newNumberOfCoins = (int)Random.Range(difficulty.NumberOfCoinsToShow.x, difficulty.NumberOfCoinsToShow.y);

        for (int i = 0; i < newNumberOfCoins; i++)
        {
            _newCoins.Add(Instantiate(coinPrefab, transform));
            _newCoins[i].SetActive(false);
        }

        PlaceObstacles();
        PlaceCoins();
    }

    private void PlaceObstacles()
    {
        float percent = playerSO.DifficultiesDict[playerSO.ActualDifficulty].PercentOfObstaclesToShow;

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(Random.Range(0f, 1f) <= percent);
        }
    }

    private void PlaceCoins()
    {
        float minZPos = 10f;

        for (int i = 0; i < _newCoins.Count; i++)
        {
            float randomZPos = Random.Range(minZPos, minZPos + 5f);
            minZPos = randomZPos + 1;

            _newCoins[i].SetActive(true);
            _newCoins[i].transform.localPosition = new Vector3(transform.position.x, _newCoins[i].transform.position.y, randomZPos);
            _newCoins[i].GetComponent<ChangeLane>().PositionLane();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.PlayerTag))
        {
            Player player = other.GetComponent<Player>();

            if (isInfinityMode || player.Coins < difficulty.NumberOfCoinsToWin)
            {
                player.IncreaseSpeed();
                transform.position = new Vector3(0, 0, transform.position.z + 297 * 2);
                PlaceObstacles();
                PlaceCoins();
            }
            else 
            {
                RepeatOrNo.Instance.RepeatNo();
            }
        }
    }
}
