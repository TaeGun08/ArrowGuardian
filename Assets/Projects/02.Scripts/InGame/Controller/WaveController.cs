using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    private GameManager gameManager;
    private GeneratorManager generatorManager;
    private WaveDataSO waveDataSo;

    private readonly Vector2[] generalPos = new Vector2[2];

    private int currentWaveIndex;
    private int totalWaves;
    private int enemiesAlive;

    private bool isSpawningWave;

    private void Awake()
    {
        generalPos[0] = new Vector2(-2.5f, 5);
        generalPos[1] = new Vector2(2.5f, 5);
    }

    public void StartWave()
    {
        gameManager = GameManager.Instance;
        generatorManager = GeneratorManager.Instance;
        waveDataSo = Resources.Load<WaveDataSO>("WaveDataSO");

        totalWaves = waveDataSo.GetWaveCount();
        currentWaveIndex = 0;

        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        while (gameObject.activeInHierarchy)
        {
            var waveData = waveDataSo.GetWaveData(currentWaveIndex);

            WaitForSeconds wait = new WaitForSeconds(waveData.Delay);

            int randomElementIndex = currentWaveIndex == 0 ? 0 : Random.Range(0, (int)ElementType.Light);
            for (int i = 0; i < waveData.Count; i++)
            {
                SpawnEnemy(waveData.GetElementType(randomElementIndex));
                yield return wait;
            }

            yield return new WaitUntil(() => enemiesAlive <= 0);

            currentWaveIndex++;
            
            if (currentWaveIndex < totalWaves)
            {
                gameManager.UnitStatsUI.SetWaveText($"Wave {currentWaveIndex + 1}");
            }
            else
            {
                gameManager.UnitStatsUI.SetWaveText($"Game Clear");
                GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
                yield break;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnEnemy(ElementType elementType)
    {
        var enemy = generatorManager.EnemyGenerator.CreateAndGetPool<Enemy>((int)elementType);

        if (enemy == null) return;

        Vector2 randomPos = new Vector2(
            Random.Range(generalPos[0].x, generalPos[1].x),
            Random.Range(generalPos[0].y, generalPos[1].y));
        enemy.transform.position = randomPos;

        generatorManager.EnemyGenerator.AddEnemyList(enemy);
        enemiesAlive++;

        enemy.OnDeath += () =>
        {
            enemiesAlive--;

            gameManager.SetExp(1);
            generatorManager.EnemyGenerator.ReturnPool<Enemy>((int)enemy.ElementType, enemy);
            enemy.OnDeath = null;
            
            generatorManager.EnemyGenerator.RemoveEnemyList(enemy);
        };
    }
}