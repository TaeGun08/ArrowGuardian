using System;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private ObjectPool<Enemy> enemyFire;

    private void Awake()
    {
 //       enemyFire = new ObjectPool<Enemy>();
    }

    public void DequeueEnemy()
    {
        
    }
}
