using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUIElement
{
    public Action OnUIImpact { get; }
    public void Show();
    public void Hide();
    public void UpdateUI(); 
}

public class UIGenerator : MonoBehaviour
{
    private const int INITIAL_COUNT = 10;

    private UIPrefabSO uiPrefabSo;
    private Canvas canvas;
    
    private readonly Dictionary<string, object> pools = new Dictionary<string, object>();

    private void Awake()
    {
        uiPrefabSo = Resources.Load<UIPrefabSO>("UIPrefabSO");
        canvas = ComponentExtensions.FindOrAddComponent<Canvas>();
    }

    private ObjectPool<T> GetOrCreatePool<T>(string name) where T : MonoBehaviour, IUIElement
    {
        if (pools.TryGetValue(name, out var poolObj) && poolObj is ObjectPool<T> pool)
        {
            return pool;
        }

        var prefab = uiPrefabSo.GetUIPrefab(name);
        if (prefab == null) return null;

        var newPool = new ObjectPool<T>(prefab.GetComponent<T>(), INITIAL_COUNT, canvas.transform);
        pools.Add(name, newPool);
        return newPool;
    }

    public T CreateAndGetUI<T>(string name) where T : MonoBehaviour, IUIElement
    {
        var pool = GetOrCreatePool<T>(name);
        if (pool == null) return null;

        var uiElement = pool.Get();
        return uiElement;
    }

    public void ReturnUI<T>(string name, T uiElement) where T : MonoBehaviour, IUIElement
    {
        if (pools.TryGetValue(name, out var poolObj) && poolObj is ObjectPool<T> pool)
        {
            pool.Return(uiElement);
        }
        else
        {
            Destroy(uiElement.gameObject);
        }
    }
}
