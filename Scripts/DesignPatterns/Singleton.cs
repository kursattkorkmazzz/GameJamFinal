using UnityEngine;
using System.Collections;

public class Singleton<T>: MonoBehaviour where T : Component
{
    static public T Instance { get; private set; }


    protected void InitializeSingletonAwake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
}
