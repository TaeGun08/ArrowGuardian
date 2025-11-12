using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private GameManager gameManager;
    private GeneratorManager generatorManager;
    private WaveDataSO waveDataSo;

    private int currentWaveIndex;
    private int totalWaves;
    private int enemiesAlive;

    private bool isSpawningWave;

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
        while (currentWaveIndex < totalWaves)
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
            gameManager.UnitStatsUI.SetWaveText(currentWaveIndex.ToString());

            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnEnemy(ElementType elementType)
    {
        var enemy = generatorManager.EnemyGenerator.CreateAndGetPool(elementType);

        if (enemy == null) return;
        
        enemiesAlive++;

        enemy.OnDeath += () =>
        {
            enemiesAlive--;
          
            gameManager.SetExp(1);
            generatorManager.EnemyGenerator.ReturnEnemy(enemy.ElementType, enemy);

            enemy.OnDeath = null;
        };
    }
}