using System;
using UnityEngine;
using Object = System.Object;

public class ArrowGenerator : MonoBehaviour
{
    private const int INITIAL_COUNT = 10;
    
    private ObjectPool<Arrow> arrowPool;

    private ArrowPrefabSO arrowPrefabSo;

    private void Awake()
    {
        arrowPrefabSo = Resources.Load<ArrowPrefabSO>("ArrowPrefabSO");
    }

    private ObjectPool<Arrow> GetNewPoolPrefab(int arrowIndex, int initialCount)
    {
        return new ObjectPool<Arrow>(arrowPrefabSo.GetArrow(arrowIndex), initialCount, transform);
    }

    public Arrow CreateAndGetArrow(int arrowIndex)
    {
        arrowPool ??= GetNewPoolPrefab(arrowIndex, INITIAL_COUNT);
        return arrowPool.Get();
    }

    public void ReturnArrow(Arrow arrow)
    {
        arrowPool.Return(arrow);
    }
}
