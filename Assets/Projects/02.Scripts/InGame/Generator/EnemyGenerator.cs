using System;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private ObjectPool<Enemy> enemyNone;

    private void Awake()
    {
        //enemyNone = new ObjectPool<Enemy>();
    }

    public void DequeueEnemy()
    {
        
    }
}
