using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton Settings")]
    [SerializeField] protected bool dontDestroy = true;
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance) return instance;

            instance = FindFirstObjectByType<T>();
            
            if (instance) return instance;
            
            GameObject obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
            
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}