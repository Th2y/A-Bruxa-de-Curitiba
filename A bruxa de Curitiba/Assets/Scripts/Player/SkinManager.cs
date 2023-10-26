using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        ChangeSkin();
    }

    public void ChangeSkin()
    {
        meshRenderer.material = playerSO.Skins[playerSO.ActualSkin].Material;
    }
}
