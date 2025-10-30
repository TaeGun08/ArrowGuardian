using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPrefabSO", menuName = "Scriptable Objects/EnemyPrefabSO")]
public class EnemyPrefabSO : ScriptableObject
{
    [Header("EnemyPrefab Settings")]
    [field: SerializeField] public Enemy[] EnemyPrefabs { get; private set; }
}
