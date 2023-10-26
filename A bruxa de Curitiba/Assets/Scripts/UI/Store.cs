using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private TextMeshProUGUI actualCoinsText;

    [Header("Skins")]
    [SerializeField] private GameObject aneOldSkin;
    [SerializeField] private TextMeshProUGUI aneOldSkinValue;

    [Header("Difficulties")]
    [SerializeField] private GameObject normalDifficulty;
    [SerializeField] private GameObject hardDifficulty;
    [SerializeField] private TextMeshProUGUI normalDifficultyValue;
    [SerializeField] private TextMeshProUGUI hardDifficultyValue;

    private void Start()
    {
        UpdateCoinsValue();
        ShowSkin();
        ShowDifficulty();
        ShowDifficulty();
    }

    #region Skin
    private void ShowSkin()
    {
        aneOldSkin.SetActive(!playerSO.Skins["Old"].Purchased);
        aneOldSkinValue.text = playerSO.Skins["Old"].Value.ToString();
    }

    public void BuySkin(string skinName)
    {
        if(playerSO.TotalNumberOfCoins >= playerSO.Skins[skinName].Value)
        {
            playerSO.TotalNumberOfCoins -= playerSO.Skins[skinName].Value;
            playerSO.Skins[skinName].Purchased = true;
            UpdateCoinsValue();
            ShowSkin();
        }
    }
    #endregion

    #region Difficulty
    private void ShowDifficulty()
    {
        normalDifficulty.SetActive(!playerSO.DifficultiesDict["Normal"].Purchased);
        hardDifficulty.SetActive(!playerSO.DifficultiesDict["Hard"].Purchased);

        normalDifficultyValue.text = playerSO.DifficultiesDict["Normal"].Value.ToString();
        hardDifficultyValue.text = playerSO.DifficultiesDict["Hard"].Value.ToString();
    }

    public void BuyDifficulty(string difficultyName)
    {
        if (playerSO.TotalNumberOfCoins >= playerSO.DifficultiesDict[difficultyName].Value)
        {
            playerSO.TotalNumberOfCoins -= playerSO.DifficultiesDict[difficultyName].Value;
            playerSO.DifficultiesDict[difficultyName].Purchased = true;
            UpdateCoinsValue();
            ShowSkin();
        }
    }
    #endregion

    private void UpdateCoinsValue()
    {
        actualCoinsText.text = playerSO.TotalNumberOfCoins.ToString();
    }
}
