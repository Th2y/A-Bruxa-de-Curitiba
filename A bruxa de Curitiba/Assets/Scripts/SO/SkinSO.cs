using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skin")]
public class SkinSO : ScriptableObject
{
    public string Name;
    public Material Material;
    public Sprite Image;
    public bool Purchased;
    public int Value;
}
