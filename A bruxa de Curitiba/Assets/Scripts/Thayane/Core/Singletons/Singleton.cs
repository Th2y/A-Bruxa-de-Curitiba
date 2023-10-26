using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    protected bool DontDestroy = false;

    protected virtual void Awake()
    {
        if (Instance == null) Instance = GetComponent<T>();
        else Destroy(gameObject);

        if(DontDestroy) DontDestroyOnLoad(Instance);
    }
}