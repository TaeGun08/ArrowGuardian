using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPrefabSO", menuName = "Scriptable Objects/EnemyPrefabSO")]
public class EnemyPrefabSO : ScriptableObject
{
    [Header("EnemyPrefab Settings")] 
    [SerializeField] private Enemy[] enemyPrefabs;

    public Enemy GetEnemyPrefab(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.None:
                return enemyPrefabs[0];
            case ElementType.Flame:
                return enemyPrefabs[1];
            case ElementType.Water:
                return enemyPrefabs[2];
            case ElementType.Wind:
                return enemyPrefabs[3];
            case ElementType.Earth:
                return enemyPrefabs[4];
            case ElementType.Lightning:
                return enemyPrefabs[5];
            case ElementType.Dark:
                return enemyPrefabs[6];
            case ElementType.Light:
                return enemyPrefabs[7];
        }
        
        return null;
    }
}
