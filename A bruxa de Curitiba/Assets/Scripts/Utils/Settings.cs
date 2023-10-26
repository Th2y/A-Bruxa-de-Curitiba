using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;

    [Header("Difficulties")]
    [SerializeField] private Button easyDifficultyButton;
    [SerializeField] private Button normalDifficultyButton;
    [SerializeField] private Button hardDifficultyButton;

    [Header("Skins")]
    [SerializeField] private Button youngSkinButton;
    [SerializeField] private Button oldSkinButton;

    private Dictionary<string, Button> difficulties => new()
    {
        { "Easy", easyDifficultyButton },
        { "Normal", normalDifficultyButton },
        { "Hard", hardDifficultyButton }
    };

    private Dictionary<string, Button> skins => new()
    {
        { "Young", youngSkinButton },
        { "Old", oldSkinButton }
    };

    private void Start()
    {
        GetActualDifficulty();
        GetActualSkin();
    }

    private void GetActualDifficulty()
    {
        foreach(DifficultySO difficulty in playerSO.Difficulties)
        {
            difficulties[difficulty.DifficultyName].interactable = difficulty.Purchased;
        }
    }

    private void GetActualSkin()
    {
        foreach (SkinSO skin in playerSO.Skins.Values)
        {
            skins[skin.Name].interactable = skin.Purchased;
        }
    }

    public void SetDifficulty(string difficulty)
    {
        playerSO.ActualDifficulty = difficulty;
    }

    public void SetSkin(string skin)
    {
        playerSO.ActualSkin = skin;
    }
}
