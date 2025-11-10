using UnityEngine;

public static class ComponentExtensions
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
            component = gameObject.AddComponent<T>();
        return component;
    }
    
    public static T FindOrAddComponent<T>() where T : Component
    {
        T component = Object.FindFirstObjectByType<T>();
        if (component) return component;
        GameObject obj = new GameObject(typeof(T).Name);
        component = obj.AddComponent<T>();
        return component;
    }
}
