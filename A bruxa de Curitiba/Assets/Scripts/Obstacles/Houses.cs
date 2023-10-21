using UnityEngine;

public class Houses : MonoBehaviour
{
    [SerializeField] private Texture[] textures;
    [SerializeField] private Material objectMaterial;

    private static readonly string MainTex = "_MainTex";

    private void Start()
    {
        objectMaterial.SetTexture(MainTex, textures[0]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.PlayerTag))
        {
            objectMaterial.SetTexture(MainTex, textures[1]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PlayerTag))
        {
            objectMaterial.SetTexture(MainTex, textures[0]);
        }
    }
}
