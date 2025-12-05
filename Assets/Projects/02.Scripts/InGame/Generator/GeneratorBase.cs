using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneratorBase : MonoBehaviour
{
    protected const int INITIAL_COUNT = 10;

    protected readonly Dictionary<int, object> pools = new Dictionary<int, object>();
    
    protected PrefabSoBase prefabSoBase;
    protected Transform parentTransform;
    
    protected virtual ObjectPool<T> GetOrCreatePool<T>(int index) where T : MonoBehaviour
    {
        if (pools.TryGetValue(index, out var poolObj) && poolObj is ObjectPool<T> pool)
            return pool;

        var prefab = prefabSoBase.GetPrefab<T>(index);
        if (prefab == null)
            return null;

        var newPool = new ObjectPool<T>(prefab.GetComponent<T>(), INITIAL_COUNT, parentTransform);
        pools.Add(index, newPool);
        return newPool;
    }
    
    public virtual T CreateAndGetPool<T>(int index) where T : MonoBehaviour
    {
        var pool = GetOrCreatePool<T>(index);
        return pool?.Get();
    }
    
    public virtual void ReturnPool<T>(int index, T element) where T : MonoBehaviour
    {
        if (pools.TryGetValue(index, out var poolObj) && poolObj is ObjectPool<T> pool)
        {
            pool.Return(element);
        }
        else
        {
            Destroy(element.gameObject);
        }
    }
}