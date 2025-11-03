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
    }

    private ObjectPool<Enemy> GetNewPoolPrefab(ElementType elementType, int initialCount, Transform transform)
    {
        return new ObjectPool<Enemy>(enemyPrefabSo.GetEnemyPrefab(elementType), initialCount, transform);
    }
    
    public void CreateAndGetPool(ElementType elementType)
    {
        Enemy enemy = null;
        
        switch (elementType)
        {
            case ElementType.None:
                enemyNone ??= GetNewPoolPrefab(ElementType.None, initialCount, transform);
                enemy = enemyNone.Get();
                break;
            case ElementType.Flame:
                enemyFlame ??= GetNewPoolPrefab(ElementType.Flame, initialCount, transform);
                enemy = enemyFlame.Get();
                break;
            case ElementType.Water:
                enemyWater ??= GetNewPoolPrefab(ElementType.Water, initialCount, transform);
                enemy = enemyWater.Get();
                break;
            case ElementType.Wind:
                enemyWind ??= GetNewPoolPrefab(ElementType.Wind, initialCount, transform);
                enemy = enemyWind.Get();
                break;
            case ElementType.Earth:
                enemyEarth ??= GetNewPoolPrefab(ElementType.Earth, initialCount, transform);
                enemy = enemyEarth.Get();
                break;
            case ElementType.Lightning:
                enemyLightning ??= GetNewPoolPrefab(ElementType.Lightning, initialCount, transform);
                enemy = enemyLightning.Get();
                break;
            case ElementType.Dark:
                enemyDark ??= GetNewPoolPrefab(ElementType.Dark, initialCount, transform);
                enemy = enemyDark.Get();
                break;
            case ElementType.Light:
                enemyLight ??= GetNewPoolPrefab(ElementType.Light, initialCount, transform);
                enemy = enemyLight.Get();
                break;
        }
        
        if (enemy == null) return;
        
        Vector2 randomPos = new Vector2(Random.Range(generalPos[0].x, generalPos[1].x),  
            Random.Range(generalPos[0].y, generalPos[1].y));
        
        enemy.transform.position = randomPos;
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
    }

    private IEnumerator GenerateEnemyCoroutine(ElementType elementType, int count, float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        
        while (count > 0)
        {
            CreateAndGetPool(elementType);
            count--;
            yield return wait;
        }
    }

    public void GenerateEnemy(ElementType elementType, int count, float delay)
    {
        StartCoroutine(GenerateEnemyCoroutine(elementType, count, delay));
    }
}
