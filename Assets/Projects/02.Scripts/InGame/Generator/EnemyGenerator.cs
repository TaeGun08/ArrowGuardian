using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    private const int INITIAL_COUNT = 10;
    private readonly Vector2[] generalPos = new Vector2[2];

    private EnemyPrefabSO enemyPrefabSo;

    private ObjectPool<Enemy> enemyNone;
    private ObjectPool<Enemy> enemyFlame;
    private ObjectPool<Enemy> enemyWater;
    private ObjectPool<Enemy> enemyWind;
    private ObjectPool<Enemy> enemyEarth;
    private ObjectPool<Enemy> enemyLightning;
    private ObjectPool<Enemy> enemyDark;
    private ObjectPool<Enemy> enemyLight;

    private readonly List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        enemyPrefabSo = Resources.Load<EnemyPrefabSO>("EnemyPrefabSO");
        
        generalPos[0] = new Vector2(-2.5f, 5);
        generalPos[1] = new Vector2(2.5f, 5);
    }

    private ObjectPool<Enemy> GetNewPoolPrefab(ElementType elementType, int initialCount, Transform transform)
    {
        return new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(elementType), initialCount, transform);
    }

    public Enemy CreateAndGetPool(ElementType elementType)
    {
        Enemy enemy = null;

        switch (elementType)
        {
            case ElementType.None:
                enemyNone ??= GetNewPoolPrefab(ElementType.None, INITIAL_COUNT, transform);
                enemy = enemyNone.Get();
                break;
            case ElementType.Flame:
                enemyFlame ??= GetNewPoolPrefab(ElementType.Flame, INITIAL_COUNT, transform);
                enemy = enemyFlame.Get();
                break;
            case ElementType.Water:
                enemyWater ??= GetNewPoolPrefab(ElementType.Water, INITIAL_COUNT, transform);
                enemy = enemyWater.Get();
                break;
            case ElementType.Wind:
                enemyWind ??= GetNewPoolPrefab(ElementType.Wind, INITIAL_COUNT, transform);
                enemy = enemyWind.Get();
                break;
            case ElementType.Earth:
                enemyEarth ??= GetNewPoolPrefab(ElementType.Earth, INITIAL_COUNT, transform);
                enemy = enemyEarth.Get();
                break;
            case ElementType.Lightning:
                enemyLightning ??= GetNewPoolPrefab(ElementType.Lightning, INITIAL_COUNT, transform);
                enemy = enemyLightning.Get();
                break;
            case ElementType.Dark:
                enemyDark ??= GetNewPoolPrefab(ElementType.Dark, INITIAL_COUNT, transform);
                enemy = enemyDark.Get();
                break;
            case ElementType.Light:
                enemyLight ??= GetNewPoolPrefab(ElementType.Light, INITIAL_COUNT, transform);
                enemy = enemyLight.Get();
                break;
        }

        if (enemy == null) return null;
        
        Vector2 randomPos = new Vector2(Random.Range(generalPos[0].x, generalPos[1].x),
            Random.Range(generalPos[0].y, generalPos[1].y));

        enemy.transform.position = randomPos;

        enemyList.Add(enemy);
        return enemy;
    }

    public void ReturnEnemy(ElementType elementType, Enemy returnEnemy)
    {
        switch (elementType)
        {
            case ElementType.None:
                enemyNone.Return(returnEnemy);
                break;
            case ElementType.Flame:
                enemyFlame.Return(returnEnemy);
                break;
            case ElementType.Water:
                enemyWater.Return(returnEnemy);
                break;
            case ElementType.Wind:
                enemyWind.Return(returnEnemy);
                break;
            case ElementType.Earth:
                enemyEarth.Return(returnEnemy);
                break;
            case ElementType.Lightning:
                enemyLightning.Return(returnEnemy);
                break;
            case ElementType.Dark:
                enemyDark.Return(returnEnemy);
                break;
            case ElementType.Light:
                enemyLight.Return(returnEnemy);
                break;
        }

        enemyList.Remove(returnEnemy);
    }

    public Enemy GetClosetEnemy(Vector3 position)
    {
        float distance = float.MaxValue;
        Enemy reValEnemy = enemyList.Count > 0 ? enemyList[0] : null;

        for (int i = 0; i < enemyList.Count; i++)
        {
            float currentDistance = Vector3.Distance(enemyList[i].transform.position, position);

            if (currentDistance >= distance) continue;
            reValEnemy = enemyList[i];
            distance = currentDistance;
        }

        return reValEnemy;
    }
}