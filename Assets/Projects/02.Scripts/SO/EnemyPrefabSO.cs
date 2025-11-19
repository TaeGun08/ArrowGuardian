using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPrefabSO", menuName = "Scriptable Objects/EnemyPrefabSO")]
public class EnemyPrefabSO : PrefabSoBase
{
    [Header("EnemyPrefab Settings")] 
    [SerializeField] private Enemy[] enemyPrefabs;
    
    public override T GetPrefab<T>(int index)
    {
        if (index < 0 || index >= enemyPrefabs.Length)
            return null;
        
        return enemyPrefabs[index] as T;
    }
}
