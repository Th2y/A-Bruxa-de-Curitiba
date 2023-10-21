using UnityEngine;

public class Constants : MonoBehaviour
{
    #region PlayerPrefs
    public static readonly string EarnedCoinsPref = "EarnedCoins";
    public static readonly string ScorePref = "Score";
    public static readonly string CoinsCurrentRunPref = "CoinsCurrentRun";
    public static readonly string EffectsPref = "Effects";
    public static readonly string MusicPref = "Music";
    #endregion

    #region Scenes
    public static readonly string MenuScene = "MainMenu";
    public static readonly string Level1Scene = "Level1";
    public static readonly string Level2Scene = "Level2";
    public static readonly string Level3Scene = "Level3";
    #endregion

    #region Tags
    public static readonly string PlayerTag = "Player";
    public static readonly string CoinTag = "Moeda";
    public static readonly string ObstaclesTag = "Obstaculos";
    public static readonly string FinishTag = "Finish";
    public static readonly string EffectsTag = "Efeitos";
    public static readonly string MusicTag = "Musicas";
    #endregion
}
