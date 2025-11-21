using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : GeneratorBase
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

    public List<Enemy> GetEnemyList()
    {
        return enemyList;
    }

    public bool EnemyListCheck()
    {
        return enemyList.Count <= 0;
    } 
}
