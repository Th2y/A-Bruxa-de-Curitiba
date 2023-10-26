using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Difficulty")]
public class DifficultySO : ScriptableObject
{
    public string DifficultyName;
    public bool Purchased;
    public int Value;
    public int NumberOfCoinsToWin = 50;
    public Vector2 NumberOfCoinsToShow;
    public int PercentOfObstaclesToShow;
}
