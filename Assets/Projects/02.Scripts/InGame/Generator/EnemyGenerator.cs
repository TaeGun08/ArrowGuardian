using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Vector2[] generalPos;

    [SerializeField] private int initialCount = 10;
    private EnemyPrefabSO enemyPrefabSo;
    
    private ObjectPool<Enemy> enemyNone;
    private ObjectPool<Enemy> enemyFlame;
    private ObjectPool<Enemy> enemyWater;
    private ObjectPool<Enemy> enemyWind;
    private ObjectPool<Enemy> enemyEarth;
    private ObjectPool<Enemy> enemyLightning;
    private ObjectPool<Enemy> enemyDark;
    private ObjectPool<Enemy> enemyLight;

    private void Awake()
    {
        enemyPrefabSo = Resources.Load<EnemyPrefabSO>("EnemyPrefabSO");
        EnemyConstructor();
    }

    private void EnemyConstructor()
    {
        enemyNone = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.None), initialCount, transform);
        enemyFlame = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Flame), initialCount, transform);
        enemyWater = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Water), initialCount, transform);
        enemyWind = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Wind), initialCount, transform);
        enemyEarth = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Earth), initialCount, transform);
        enemyLightning = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Lightning), initialCount, transform);
        enemyDark = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Dark), initialCount, transform);
        enemyLight = new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(ElementType.Light), initialCount, transform);
    }

    public void CreateOrGetPool(ElementType elementType)
    {
        Enemy enemy = null;
        
        switch (elementType)
        {
            case ElementType.None:
                enemy = enemyNone.Get();
                break;
            case ElementType.Flame:
                enemy = enemyFlame.Get();
                break;
            case ElementType.Water:
                enemy = enemyWater.Get();
                break;
            case ElementType.Wind:
                enemy = enemyWind.Get();
                break;
            case ElementType.Earth:
                enemy = enemyEarth.Get();
                break;
            case ElementType.Lightning:
                enemy = enemyLightning.Get();
                break;
            case ElementType.Dark:
                enemy = enemyDark.Get();
                break;
            case ElementType.Light:
                enemy = enemyLight.Get();
                break;
        }
        
        if (enemy == null) return;
        
        Vector2 randomPos = new Vector2(Random.Range(generalPos[0].x, generalPos[1].x),  
            Random.Range(generalPos[0].y, generalPos[1].y));
        
        enemy.transform.position = randomPos;
    }

    public void Return(ElementType elementType, Enemy returnEnemy)
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
    }

    private IEnumerator GenerateCoroutine(ElementType elementType, int count, float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        while (count > 0)
        {
            CreateOrGetPool(elementType);
            count--;
            yield return wait;
        }
    }

    public void Generate(ElementType elementType, int count, float delay)
    {
        StartCoroutine(GenerateCoroutine(elementType, count, delay));
    }
}
