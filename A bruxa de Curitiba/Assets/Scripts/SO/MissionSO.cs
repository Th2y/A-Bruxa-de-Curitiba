using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Mission")]
public class MissionSO : ScriptableObject
{
    public string Name;
    public string Description;
    public bool Completed;
}
