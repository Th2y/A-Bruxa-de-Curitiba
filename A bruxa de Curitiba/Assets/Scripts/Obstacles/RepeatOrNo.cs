using UnityEngine;
using UnityEngine.SceneManagement;

public class RepeatOrNo : MonoBehaviour
{
    [SerializeField] private GameObject[] moedas;
    [SerializeField] private GameObject fim;
    [SerializeField] private GameObject pausarNoFim;
    [SerializeField] private GameObject player;

    private int num = 1;

    public static RepeatOrNo Instance;

    private void Start()
    {
        Instance = this;
    }

    public void RepeatYes()
    {
        for(int i = 0; i < moedas.Length; i++)
        {
            moedas[i].transform.position = new Vector3(moedas[i].transform.position.x, moedas[i].transform.position.y, moedas[i].transform.position.z + 254*num);
        }
        fim.transform.position = new Vector3(fim.transform.position.x, fim.transform.position.y, fim.transform.position.z + 254*num);

        player.GetComponent<Player>().IncreaseSpeed();
    }

    public void RepeatNo()
    {
        pausarNoFim.SetActive(false);
        Invoke(nameof(PassStage), 2f);
    }

    private void PassStage()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                SceneManager.LoadScene(Constants.Level2Scene);
                break;
            case "Level2":
                SceneManager.LoadScene(Constants.Level3Scene);
                break;
        }            
    }
}
