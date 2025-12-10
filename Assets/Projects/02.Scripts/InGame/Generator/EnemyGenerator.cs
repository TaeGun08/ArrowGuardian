using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class EnemyGenerator : GeneratorBase
{
    private readonly List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        prefabSoBase = Resources.Load<EnemyPrefabSO>("EnemyPrefabSO");
        parentTransform = transform;
    }

    public void AddEnemyList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemyList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }
    
    public Enemy GetClosetEnemy(Vector3 position)
    {
        float distance = float.MaxValue;
        Enemy result = null;

        for (int i = 0; i < enemyList.Count; i++)
        {
            float curDistance = Vector3.Distance(enemyList[i].transform.position, position);

            if (curDistance < distance)
            {
                distance = curDistance;
                result = enemyList[i];
            }
        }

        return result;
    }
    
    public Enemy GetClosetEnemy(Vector3 position, List<Enemy> excludeEnemies)
    {
        Enemy result = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < enemyList.Count; i++)
        {
            Enemy enemy = enemyList[i];
            if (!IsValidTarget(enemy, excludeEnemies)) continue;

            float distance = Vector3.Distance(enemy.transform.position, position);

            if (distance < minDistance)
            {
                minDistance = distance;
                result = enemy;
            }
        }

        return result;
    }

    private bool IsValidTarget(Enemy enemy, List<Enemy> excludeEnemies)
    {
        if (enemy == null) return false;
        return excludeEnemies == null || !excludeEnemies.Contains(enemy);
    }
    
    public List<Enemy> GetEnemyList()
    {
        return enemyList;
    }

    public bool EnemyListCheck()
    {
        return enemyList.Count <= 0;
    } 
}
