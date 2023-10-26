using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    [Header("Skin")]
    public SkinSO AneYoung;
    public SkinSO AneOld;
    public string ActualSkin = "Young";
    public Dictionary<string, SkinSO> Skins => new()
    {
        { "Young", AneYoung },
        { "Old", AneOld }
    };

    [Space(10), Header("Coins")]
    public int TotalNumberOfCoins = 0;

    [Space(10), Header("Score")]
    public int BestScore = 0;

    [Space(10), Header("Difficulty")]
    public string ActualDifficulty = "Easy";
    public DifficultySO[] Difficulties;
    public Dictionary<string, DifficultySO> DifficultiesDict => new()
    {
        { "Easy", Difficulties[0] },
        { "Normal", Difficulties[1] },
        { "Hard", Difficulties[2] }
    };
}
