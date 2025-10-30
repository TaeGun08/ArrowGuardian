using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (!instance) return instance;

            instance = FindFirstObjectByType<T>();
            if (!instance) return instance;
            GameObject obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(instance);
            
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}